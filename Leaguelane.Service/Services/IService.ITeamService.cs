using Leaguelane.Persistence.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public interface ITeamService
    {
        Task FetchAndStoreTeamsAndVenuesAsync(int leagueId, int seasonId, int sportId, CancellationToken cancellationToken);
        Task ImportAllTeams(CancellationToken cancellationToken);
        Task<Dictionary<int, Team>> GetAllTeamsById(IEnumerable<int> teamIds, CancellationToken cancellationToken);
        Task<List<Team>> GetAllTeamsByIds(List<int> teamIds, CancellationToken cancellationToken);
    }
}
