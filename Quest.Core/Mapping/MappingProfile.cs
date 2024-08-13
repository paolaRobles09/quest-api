using AutoMapper;
using Quest.Core.Entities;
using Quest.Infrastructure.Data;

namespace Quest.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<PlayerQuestState, PlayerState>();
            CreateMap<PlayerState, PlayerQuestState>();
        }
    }
}
