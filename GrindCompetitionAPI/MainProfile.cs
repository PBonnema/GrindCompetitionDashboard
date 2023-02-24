using AutoMapper;
using DataAccess.Models;
using GrindCompetitionAPI.DTO;

namespace GrindCompetitionAPI
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            CreateMap<GrindCompetitionPlayer, GrindCompetitionPlayerDTO>();
            CreateMap<GrindCompetitionScore, GrindCompetitionScoreDTO>();
            CreateMap<GrindCompetitionConfig, GrindCompetitionConfigDTO>();
        }
    }
}