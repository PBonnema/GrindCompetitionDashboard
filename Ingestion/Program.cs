using DataAccess;
using DataAccess.Models;
using DataAccess.Repository;
using Ingestion.Services;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Timers;

namespace Ingestion
{
    class Program
    {
        private static readonly bool IsDevelopment = Environment.GetEnvironmentVariable("ENVIRONMENT") == "Development";
        private static IConfiguration Configuration;
        private static ILogger Logger;
        private static BlockTanksStatsDatabaseSettings BlockTanksStatsDatabaseSettings;
        private static GrindCompetitionConfig GrindCompetitionConfig;

        static async Task Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console(LogEventLevel.Verbose);

            if (!IsDevelopment)
            {
                loggerConfiguration = loggerConfiguration
                    .WriteTo.ApplicationInsights(Configuration["APPLICATION_INSIGHTS_INSTRUMENTATION_KEY"], TelemetryConverter.Traces, LogEventLevel.Warning);
            }

            using var logger = loggerConfiguration.CreateLogger();
            Logger = logger;

            try
            {
                var dateTimeProvider = new DateTimeProvider();
                BlockTanksStatsDatabaseSettings = new BlockTanksStatsDatabaseSettings
                {
                    ConnectionString = Configuration["DATABASE_CONNECTION_STRING"],
                    DatabaseName = Configuration["DATABASE_NAME"],
                    DatabasePassword = Configuration["DATABASE_PASSWORD"],
                    GrindCompetitionPlayerCollectionName = "grindCompetitionPlayers",
                    GrindCompetitionConfigCollectionName = "grindCompetitionConfig",
                };
                var grindCompetitionConfigService = new GrindCompetitionConfigService(new GrindCompetitionConfigRepository(BlockTanksStatsDatabaseSettings, dateTimeProvider));
                GrindCompetitionConfig = await grindCompetitionConfigService.GetConfig();

                Logger.Information($"Ingesting every {GrindCompetitionConfig.IngestionIntervalHours} hours.");
                using var timer = new Timer(GrindCompetitionConfig.IngestionIntervalHours * 60 * 60 * 1000);

                async void onElapsed(object sender, ElapsedEventArgs args)
                {
                    var ingestionStart = DateTimeOffset.UtcNow;
                    await Ingest();
                    Logger.Information($"Next ingestion at {ingestionStart.AddMilliseconds(timer.Interval)}");
                }
                timer.Elapsed += onElapsed;

                // Delay the start of ingestion until either the start of the competition, or if it already has started, until the next scheduled update.
                var delayHours = (GrindCompetitionConfig.CompetitionStart - dateTimeProvider.Now).TotalHours;
                if (delayHours < 0)
                {
                    delayHours *= -1;
                    delayHours %= GrindCompetitionConfig.IngestionIntervalHours;
                    delayHours = GrindCompetitionConfig.IngestionIntervalHours - delayHours;
                }

                Logger.Information($"Delaying ingestion for {delayHours} hours until {dateTimeProvider.Now.AddHours(delayHours)} (competition start: {GrindCompetitionConfig.CompetitionStart})");
                using var delayTimer = new Timer(delayHours * 60 * 60 * 1000);
                delayTimer.AutoReset = false;
                delayTimer.Elapsed += (sender, args) => { timer.Start(); onElapsed(null, null); };
                delayTimer.Start();

                // Run indefinitely while not consuming CPU and still being able to react to signals
                await Task.Delay(-1);
            }
            catch (Exception e)
            {
                Logger.Fatal($"...An exception occurred: ${e}");
                throw;
            }
        }

        static async Task Ingest()
        {
            var dateTimeProvider = new DateTimeProvider();

            Logger.Information($"Updating at {dateTimeProvider.Now}...");

            try
            {
                var sw = Stopwatch.StartNew();

                var grindCompetitionPlayerRepository = new GrindCompetitionPlayerRepository(BlockTanksStatsDatabaseSettings, dateTimeProvider);

                using var scrapeBTPageService = new ScrapeBTPageService(new ScrapeBTPageService.SeleniumConfig(
                    UseRemoteSeleniumChrome: !IsDevelopment,
                    SeleniumChromeUrl: Configuration["SELENIUM_CONNECTION_STRING"],
                    SeleniumConnectionRetries: 5,
                    SeleniumConnectionRetryPeriodSec: 2
                ), "https://blocktanks.io");

                var grindCompetitionService = new GrindCompetitionService(grindCompetitionPlayerRepository, scrapeBTPageService, Logger.ForContext<GrindCompetitionService>());

                foreach (var clanTag in Configuration["CLANS"].Split(','))
                {
                    await grindCompetitionService.UpdateScoresForPlayersOfClanAsync(clanTag);
                }

                sw.Stop();
                Logger.Information($"...Done updating in {sw.Elapsed}");
            }
            catch (Exception e)
            {
                Logger.Fatal($"...An exception occurred: ${e}");
            }
        }
    }
}
