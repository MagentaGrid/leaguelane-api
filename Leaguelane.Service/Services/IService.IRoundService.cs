using Leaguelane.Persistence.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public interface IRoundService
    {
        Task FetchAndStoreRoundsAsync(int leagueId, int seasonId, int sportId, CancellationToken cancellationToken);
    }
}
