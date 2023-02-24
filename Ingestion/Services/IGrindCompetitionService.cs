using DataAccess.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Ingestion.Services
{
    public interface IGrindCompetitionService
    {
        Task UpdateScoreForPlayerAsync(GrindCompetitionPlayer grindCompetitionPlayer, CancellationToken cancellation = default);
        Task UpdateScoresForPlayersOfClanAsync(string clanTag, CancellationToken cancellation = default);
    }
}