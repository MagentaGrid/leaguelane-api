using Leaguelane.Persistence.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public interface ITeamService
    {
        Task FetchAndStoreTeamsAndVenuesAsync(int leagueId, int seasonId, int sportId, CancellationToken cancellationToken);
        Task ImportAllTeams(CancellationToken cancellationToken);
    }
}
