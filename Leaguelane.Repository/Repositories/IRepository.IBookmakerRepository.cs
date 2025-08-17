using Leaguelane.Persistence.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public interface IBookmakerRepository
    {
        Task<List<Bookmaker>> AddBookmakers(List<string> bookmakerNames, CancellationToken cancellationToken);
        Task<List<Bookmaker>> GetActiveBookmakersAsync(CancellationToken cancellationToken);
        Task<List<Bookmaker>> GetAllBookmakersAsync(CancellationToken cancellationToken);
        Task<Bookmaker?> GetBookmakerByIdAsync(int id, CancellationToken cancellationToken);
        Task<Bookmaker> CreateBookmakerAsync(Bookmaker bookmaker, CancellationToken cancellationToken);
        Task<Bookmaker> UpdateBookmakerAsync(Bookmaker bookmaker, CancellationToken cancellationToken);
        Task SoftDeleteBookmakerAsync(int id, CancellationToken cancellationToken);
        Task RestoreBookmakerAsync(int id, CancellationToken cancellationToken);
        Task<List<Bookmaker>> GetDeletedBookmakersAsync(CancellationToken cancellationToken);
    }
}
