using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public interface IBookmakerService
    {
        Task<bool> GetAllBookmakersAsync(CancellationToken cancellationToken);
    }
}
