using Leaguelane.Persistence.Context;
using Leaguelane.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public class VenueRepository : IVenueRepository
    {
        private readonly LeaguelaneDbContext _context;
        public VenueRepository(LeaguelaneDbContext context)
        {
            _context = context;
        }
        public async Task AddOrUpdateVenuesAsync(List<Venue> venues, CancellationToken cancellationToken)
        {
            var apiVenueIds = venues.Select(v => v.ApiVenueId).ToList();
            var existingVenues = await _context.Venues.Where(v => apiVenueIds.Contains(v.ApiVenueId)).ToListAsync(cancellationToken);
            var uniqueVenues = venues.DistinctBy(x => x.ApiVenueId).ToList();
            foreach (var venue in uniqueVenues)
            {
                var existing = existingVenues.FirstOrDefault(v => v.ApiVenueId == venue.ApiVenueId);
                if (existing == null)
                {
                    venue.Created = System.DateTime.UtcNow;
                    venue.Active = true;
                    await _context.Venues.AddAsync(venue, cancellationToken);
                }
                else
                {
                    existing.Name = venue.Name;
                    existing.Address = venue.Address;
                    existing.City = venue.City;
                    existing.Capacity = venue.Capacity;
                    existing.Surface = venue.Surface;
                    existing.ImageUrl = venue.ImageUrl;
                    existing.SportId = venue.SportId;
                    existing.LeagueId = venue.LeagueId;
                    existing.SeasonId = venue.SeasonId;
                    existing.TeamId = venue.TeamId;
                    existing.Updated = System.DateTime.UtcNow;
                    existing.Active = venue.Active;
                    _context.Venues.Update(existing);
                }
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
