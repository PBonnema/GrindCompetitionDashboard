using System;

namespace DataAccess.Models
{
    public class GrindCompetitionConfig : Model
    {
        public double IngestionIntervalHours { get; set; }
        public DateTime CompetitionStart { get; set; }
        public DateTime CompetitionEnd { get; set; }
    }
}