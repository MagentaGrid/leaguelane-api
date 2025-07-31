using Leaguelane.Persistence.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public interface IVenueRepository
    {
        Task AddOrUpdateVenuesAsync(List<Venue> venues, CancellationToken cancellationToken);
    }
}
