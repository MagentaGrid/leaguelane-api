using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;

namespace Leaguelane.Service.Services
{
    public class VenueService: IVenueService
    {
        private readonly IRepository _repository;

        public VenueService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Venue>> GetAllVenues(List<int> ids, CancellationToken cancellationToken)
        {
            return (await _repository.FindAllAsync<Venue>(x => ids.Contains(x.ApiVenueId))).ToList();
        }

        public async Task<Venue> GetVenueByApiId(int id, CancellationToken cancellationToken)
        {
            return await _repository.FirstOrDefaultAsync<Venue>(x => x.ApiVenueId == id, cancellationToken);
        }
    }
}
