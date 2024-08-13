using Quest.Core.Dto;
using Quest.Core.Dto.Requests;
using Quest.Core.Dto.Responses;
using Quest.Core.Entities;

namespace Quest.Core.Interfaces
{
    public interface IQuestService
    {
        Task<PlayerState> GetPlayerState(string playerId);
        Task<ProgressResponse> AddOrUpdatePlayerQuestState(ProgressRequest request);
        int CalculateQuestPointAccumulation(int chipAmtBet, int playerLevel);
        float CalculateQuestPercentCompleted(int pointsEarned);
        List<Milestone> GetCompletedMilestones(int pointsEarned, int lastIndexCompleted);

    }
}
