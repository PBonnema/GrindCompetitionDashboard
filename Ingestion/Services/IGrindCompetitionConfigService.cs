using DataAccess.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Ingestion.Services
{
    public interface IGrindCompetitionConfigService
    {
        Task<GrindCompetitionConfig> GetConfig(CancellationToken cancellation);
    }
}