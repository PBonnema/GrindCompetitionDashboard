namespace GrindCompetitionAPI.DTO
{
    public class GrindCompetitionPlayerDTO
    {
        public string Name { get; set; }
        public GrindCompetitionScoreDTO InitialScore { get; set; }
        public GrindCompetitionScoreDTO CurrentScore { get; set; }
    }
}
