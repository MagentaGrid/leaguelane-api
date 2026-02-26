using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public interface IBetService
    {
        Task<bool> GetAllBetsAsync(CancellationToken cancellationToken);
        Task<List<Leaguelane.Persistence.Entities.Bet>> GetAllBets(CancellationToken cancellationToken);
    }
}
