using DataAccess.Models;
using DataAccess.Repository;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ingestion.Services
{
    public class GrindCompetitionConfigService : IGrindCompetitionConfigService
    {
        private readonly IGrindCompetitionConfigRepository _grindCompetitionConfigRepository;

        public GrindCompetitionConfigService(IGrindCompetitionConfigRepository grindCompetitionConfigRepository)
        {
            _grindCompetitionConfigRepository = grindCompetitionConfigRepository;
        }

        public async Task<GrindCompetitionConfig> GetConfig(CancellationToken cancellation = default)
        {
            return (await _grindCompetitionConfigRepository.GetAsync(cancellation)).Single();
        }
    }
}
