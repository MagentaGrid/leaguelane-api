using Leaguelane.Persistence.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public interface IBetRepository
    {
        Task<List<Bet>> AddBets(List<Bet> bets, CancellationToken cancellationToken);
    }
}
