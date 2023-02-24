using DataAccess.Models;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class GrindCompetitionPlayerRepository : Repository<GrindCompetitionPlayer>, IGrindCompetitionPlayerRepository
    {
        public GrindCompetitionPlayerRepository(BlockTanksStatsDatabaseSettings settings, IDateTimeProvider dateTimeProvider)
            : base(settings, settings.GrindCompetitionPlayerCollectionName, dateTimeProvider)
        {
        }

        public async Task<GrindCompetitionPlayer> GetByNameAsync(string playerName, CancellationToken cancellation = default) =>
            await (await _models.FindAsync(player => playerName == player.Name, cancellationToken: cancellation)).FirstOrDefaultAsync(cancellation);


        public override async Task<GrindCompetitionPlayer> CreateAsync(GrindCompetitionPlayer model, CancellationToken cancellation = default)
        {
            if (model.InitialScore != null && model.InitialScore.Timestamp == default)
            {
                model.InitialScore.Timestamp = _now;
            }

            if (model.CurrentScore != null && model.CurrentScore.Timestamp == default)
            {
                model.CurrentScore.Timestamp = _now;
            }

            return await base.CreateAsync(model, cancellation);
        }

        public override async Task UpdateAsync(string id, GrindCompetitionPlayer modelIn, CancellationToken cancellation = default)
        {
            if (modelIn.CurrentScore != null && modelIn.CurrentScore.Timestamp == default)
            {
                modelIn.CurrentScore.Timestamp = _now;
            }

            await base.UpdateAsync(id, modelIn, cancellation);
        }
    }
}
