using System;

namespace GrindCompetitionAPI.DTO
{
    public class GrindCompetitionConfigDTO
    {
        public double IngestionIntervalHours { get; set; }
        public DateTime CompetitionStart { get; set; }
        public DateTime CompetitionEnd { get; set; }
    }
}
