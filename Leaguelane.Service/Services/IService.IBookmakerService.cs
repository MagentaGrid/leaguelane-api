using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public interface IBookmakerService
    {
        Task SoftDeleteBookmakerAsync(int id, CancellationToken cancellationToken);
        Task RestoreBookmakerAsync(int id, CancellationToken cancellationToken);
        Task<bool> ImportBookmakersFromApiAsync(CancellationToken cancellationToken);
        Task<(int total, List<Bookmaker>)> GetAllBookmakersAsync(int page, int pageSize, string search, CancellationToken cancellationToken);
        Task<List<Bookmaker>> GetAllActiveBookmakersAsync(CancellationToken cancellationToken);
        Task<bool> EnableBookmakerAsync(int id, CancellationToken cancellationToken);
        Task<bool> DisableBookmakerAsync(int id, CancellationToken cancellationToken);
    }
}
