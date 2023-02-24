namespace DataAccess.Repository
{
    public record BlockTanksStatsDatabaseSettings
    {
        public string ConnectionString { get; init; }
        public string DatabasePassword { get; init; }
        public string DatabaseName { get; init; }
        public string GrindCompetitionPlayerCollectionName { get; init; }
        public string GrindCompetitionConfigCollectionName { get; init; }
    }
}
