using Leaguelane.Persistence.Context;
using Leaguelane.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public class SeasonRepository: ISeasonRepository
    {
        private readonly LeaguelaneDbContext _context;

        public SeasonRepository(LeaguelaneDbContext context)
        {
            _context = context;
        }

        public async Task<List<Season>> AddSeasons(List<int> seasons,CancellationToken cancellationToken)
        {
            var existingSeasons = await _context.Seasons.Where(x => x.Active == true).ToListAsync(cancellationToken);
            var seasonYears = existingSeasons.Select(s => s.Year).ToList();
            var newSeasons = new List<Season>();

            foreach (var season in seasons)
            {
                if (!seasonYears.Contains(season))
                {
                    var newSeason = new Season 
                    { 
                        Active = true ,
                        Created = DateTime.UtcNow,
                        Year = season
                    };
                    newSeasons.Add(newSeason);
                }
            }

            await _context.Seasons.AddRangeAsync(newSeasons, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return newSeasons;
        }
    }
}
