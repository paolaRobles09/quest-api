using AutoMapper;
using Quest.Core.Dto;
using Quest.Core.Entities;
using Quest.Core.Interfaces;
using Quest.Infrastructure.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Quest.Core.Dto.Responses;
using Quest.Infrastructure.Data;
using Quest.Core.Dto.Requests;

namespace Quest.Core.Services
{
    public class QuestService : IQuestService
    {
        private readonly IMapper _mapper;
        private readonly QuestConfig _config;
        private readonly QuestDbContext _context;

        public QuestService(QuestDbContext context, IMapper mapper) { 

            _config = QuestConfig.GetConfig();
            _context = context;
            _mapper = mapper;
        }
        public async Task<PlayerState> GetPlayerState(string playerId)
        {
            // AsNoTracking: quicker to execute as there is no need to setup the change tracking information
            var playerQuestState = _mapper.Map<PlayerState>(await _context.PlayerQuestState.AsNoTracking().FirstOrDefaultAsync(x => x.PlayerId == playerId));

            return playerQuestState;
        }

        private PlayerQuestState GetPlayerQuestState(PlayerQuestState playerState, ProgressRequest request, out ProgressResponse response) 
        {
            var pointsEarned = CalculateQuestPointAccumulation(request.ChipAmountBet, request.PlayerLevel);

            playerState.QuestPointsEarned += pointsEarned;
            var completedMilestones = GetCompletedMilestones(playerState.QuestPointsEarned, playerState.LastMilestoneIndexCompleted);

            playerState.LastMilestoneIndexCompleted = completedMilestones.Count > 0 ? completedMilestones.LastOrDefault()?.Index ?? 0 : playerState.LastMilestoneIndexCompleted;
            playerState.TotalQuestPercentCompleted = CalculateQuestPercentCompleted(playerState.QuestPointsEarned);

            response = new ProgressResponse(pointsEarned, playerState.TotalQuestPercentCompleted, completedMilestones.Select(x => new MilestoneCompleted(x.Index, x.ChipsAwarded)).ToList() ?? []);
           
            return playerState;
        }

        public async Task<ProgressResponse> AddOrUpdatePlayerQuestState(ProgressRequest request) 
        {
            var playerState = await _context.PlayerQuestState.FirstOrDefaultAsync(x => x.PlayerId == request.PlayerId);
            var response = new ProgressResponse(0, 0, []);
            
            if(playerState == null) 
            {
                _context.PlayerQuestState.Add(GetPlayerQuestState(new PlayerQuestState { PlayerId = request.PlayerId }, request, out response));
            }
            else 
            {
                _context.Entry(playerState).CurrentValues.SetValues(GetPlayerQuestState(playerState, request, out response));
            }

            await _context.SaveChangesAsync();

            return response;

        }

        public float CalculateQuestPercentCompleted(int pointsEarned)
        {
            return pointsEarned * 100 / _config.TotalQuestPointsNeeded;
        }

        public int CalculateQuestPointAccumulation(int chipAmtBet, int playerLevel)
        {
            return (int)(chipAmtBet * _config.RateFromBet) + (int)(playerLevel * _config.LevelBonusRate);
        }

        public List<Milestone> GetCompletedMilestones(int pointsEarned, int lastIndexCompleted)
        {
            return _config.Milestones.Where(x => x.QuestPointsRequired <= pointsEarned && x.Index > lastIndexCompleted).ToList();
        }
    }
}
