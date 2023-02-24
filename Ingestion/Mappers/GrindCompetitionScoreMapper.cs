using DataAccess.Models;
using Ingestion.Dtos;

namespace Ingestion.Mappers
{
    public static class GrindCompetitionScoreMapper
    {
        public static GrindCompetitionScore MapToGrindCompetitionScore(this PlayerOnClanPageDto playerOnClanPageDto) => new()
        {
            Xp = playerOnClanPageDto.XP,
            Kills = playerOnClanPageDto.Kills,
            Deaths = playerOnClanPageDto.Deaths
        };
    }
}
