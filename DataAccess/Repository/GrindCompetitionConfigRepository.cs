using DataAccess.Models;

namespace DataAccess.Repository
{
    public class GrindCompetitionConfigRepository : Repository<GrindCompetitionConfig>, IGrindCompetitionConfigRepository
    {
        public GrindCompetitionConfigRepository(BlockTanksStatsDatabaseSettings settings, IDateTimeProvider dateTimeProvider)
            : base(settings, settings.GrindCompetitionConfigCollectionName, dateTimeProvider)
        {
        }
    }
}
