using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public interface IOddsService
    {
        Task FetchAndStoreOddsAsync(int leagueId, int seasonId, int sportId, CancellationToken cancellationToken);
    }
}
