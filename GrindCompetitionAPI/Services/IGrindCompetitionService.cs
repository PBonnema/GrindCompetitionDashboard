using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrindCompetitionAPI.Services
{
    public interface IGrindCompetitionService
    {
        Task<IEnumerable<GrindCompetitionPlayer>> GetGrindCompetitionPlayers();
    }
}