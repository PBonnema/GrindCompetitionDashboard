using DataAccess.Models;
using DataAccess.Repository;
using Serilog;
using System.Threading;
using System.Threading.Tasks;

namespace Ingestion.Services
{
    public class GrindCompetitionService : IGrindCompetitionService
    {
        private readonly IGrindCompetitionPlayerRepository _grindCompetitionPlayerRepository;
        private readonly IScrapeBTPageService _scrapeBTPageService;
        private readonly ILogger _logger;

        public GrindCompetitionService(IGrindCompetitionPlayerRepository grindCompetitionPlayerRepository, IScrapeBTPageService scrapeBTPageService, ILogger logger)
        {
            _grindCompetitionPlayerRepository = grindCompetitionPlayerRepository;
            _scrapeBTPageService = scrapeBTPageService;
            _logger = logger;
        }

        public async Task UpdateScoreForPlayerAsync(GrindCompetitionPlayer grindCompetitionPlayer, CancellationToken cancellation = default)
        {
            var storedPlayer = await _grindCompetitionPlayerRepository.GetByNameAsync(grindCompetitionPlayer.Name, cancellation);
            if (storedPlayer == null)
            {
                _logger.Debug($"Adding the initial score for player {grindCompetitionPlayer.Name}");
                grindCompetitionPlayer.CurrentScore = grindCompetitionPlayer.InitialScore;
                await _grindCompetitionPlayerRepository.CreateAsync(grindCompetitionPlayer, cancellation);
            }
            else
            {
                _logger.Debug($"Updating the score for player {grindCompetitionPlayer.Name}");
                storedPlayer.CurrentScore = grindCompetitionPlayer.InitialScore;
                await _grindCompetitionPlayerRepository.UpdateAsync(storedPlayer.Id, storedPlayer, cancellation);
            }
        }

        public async Task UpdateScoresForPlayersOfClanAsync(string clanTag, CancellationToken cancellation = default)
        {
            _logger.Information($"Updating the scores for {clanTag}...");
            var grindCompetitionPlayers = await _scrapeBTPageService.ScrapePlayersFromClanpage(clanTag, cancellation);
            foreach(var grindCompetitionPlayer in grindCompetitionPlayers)
            {
                await UpdateScoreForPlayerAsync(grindCompetitionPlayer, cancellation);
            }
        }
    }
}
