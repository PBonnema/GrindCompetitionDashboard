using DataAccess.Models;
using Ingestion.Dtos;

namespace Ingestion.Mappers
{
    public static class GrindCompetitionPlayerMapper
    {
        public static GrindCompetitionPlayer MapToGrindCompetitionPlayer(this PlayerOnClanPageDto playerOnClanPageDto) => new()
        {
            Name = playerOnClanPageDto.Name,
            InitialScore = playerOnClanPageDto.MapToGrindCompetitionScore(),
        };
    }
}
