using System;

namespace DataAccess.Models
{
    public class GrindCompetitionScore
    {
        public DateTime Timestamp { get; set; }
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public double Xp { get; set; }
    }
}