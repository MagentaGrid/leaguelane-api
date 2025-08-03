using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public interface ITeamStatService
    {
        Task FetchAndStoreTeamStatsAsync(int leagueId, int teamId, int seasonId, int sportId, CancellationToken cancellationToken);
    }
}
