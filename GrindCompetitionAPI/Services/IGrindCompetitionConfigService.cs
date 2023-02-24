using DataAccess.Models;
using System.Threading;
using System.Threading.Tasks;

namespace GrindCompetitionAPI.Services
{
    public interface IGrindCompetitionConfigService
    {
        Task<GrindCompetitionConfig> GetConfig(CancellationToken cancellation = default);
    }
}