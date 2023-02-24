using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ingestion.Services
{
    public interface IScrapeBTPageService : IDisposable
    {
        Task<IEnumerable<GrindCompetitionPlayer>> ScrapePlayersFromClanpage(string clanTag, CancellationToken cancellation = default);
    }
}