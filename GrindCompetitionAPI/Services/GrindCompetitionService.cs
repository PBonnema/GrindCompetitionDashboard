using DataAccess.Models;
using DataAccess.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrindCompetitionAPI.Services
{
    public class GrindCompetitionService : IGrindCompetitionService
    {
        private readonly IGrindCompetitionPlayerRepository _grindCompetitionPlayerRepository;

        public GrindCompetitionService(IGrindCompetitionPlayerRepository grindCompetitionPlayerRepository)
        {
            _grindCompetitionPlayerRepository = grindCompetitionPlayerRepository;
        }

        public async Task<IEnumerable<GrindCompetitionPlayer>> GetGrindCompetitionPlayers()
        {
            return (await _grindCompetitionPlayerRepository.GetAsync()).OrderByDescending(p => p.CurrentScore.Xp - p.InitialScore.Xp);
        }
    }
}
