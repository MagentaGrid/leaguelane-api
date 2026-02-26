using Leaguelane.Persistence.Entities;

namespace Leaguelane.Service.Services
{
    public interface IVenueService
    {
        Task<List<Venue>> GetAllVenues(List<int> ids, CancellationToken cancellationToken);
        Task<Venue> GetVenueByApiId(int id, CancellationToken cancellationToken);
    }
}
