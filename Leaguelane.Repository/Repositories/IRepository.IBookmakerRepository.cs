using Leaguelane.Persistence.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public interface IBookmakerRepository
    {
        Task<List<Bookmaker>> AddBookmakers(List<string> bookmakerNames, CancellationToken cancellationToken);
    }
}
