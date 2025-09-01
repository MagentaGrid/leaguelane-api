using Leaguelane.Models.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public interface IBookmakerService
    {
        Task<List<BookmakerDto>> GetActiveBookmakersAsync(CancellationToken cancellationToken);
        Task<List<BookmakerDto>> GetAllBookmakersAsync(CancellationToken cancellationToken);
        Task<BookmakerDto?> GetBookmakerByIdAsync(int id, CancellationToken cancellationToken);
        Task<BookmakerDto> CreateBookmakerAsync(BookmakerDto bookmaker, CancellationToken cancellationToken);
        Task<BookmakerDto> UpdateBookmakerAsync(BookmakerDto bookmaker, CancellationToken cancellationToken);
        Task SoftDeleteBookmakerAsync(int id, CancellationToken cancellationToken);
        Task RestoreBookmakerAsync(int id, CancellationToken cancellationToken);
        Task<List<BookmakerDto>> GetDeletedBookmakersAsync(CancellationToken cancellationToken);
        Task<bool> ImportBookmakersFromApiAsync(CancellationToken cancellationToken);
    }
}
