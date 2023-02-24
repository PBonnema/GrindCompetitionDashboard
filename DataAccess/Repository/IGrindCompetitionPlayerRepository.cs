using DataAccess.Models;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IGrindCompetitionPlayerRepository : IRepository<GrindCompetitionPlayer>
    {
        Task<GrindCompetitionPlayer> GetByNameAsync(string playerName, CancellationToken cancellation = default);
    }
}