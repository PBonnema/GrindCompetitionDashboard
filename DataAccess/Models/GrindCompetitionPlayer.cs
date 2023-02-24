namespace DataAccess.Models
{
    public class GrindCompetitionPlayer : Model
    {
        public string Name { get; set; }
        public GrindCompetitionScore InitialScore { get; set; }
        public GrindCompetitionScore CurrentScore { get; set; }
    }
}
