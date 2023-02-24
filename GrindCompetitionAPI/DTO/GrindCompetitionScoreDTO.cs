using System;

namespace GrindCompetitionAPI.DTO
{
    public class GrindCompetitionScoreDTO
    {
        public DateTime Timestamp { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public double Xp { get; set; }
    }
}
