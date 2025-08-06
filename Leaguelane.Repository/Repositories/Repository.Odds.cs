using Leaguelane.Persistence.Context;
using Leaguelane.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public class OddsRepository : IOddsRepository
    {
        private readonly LeaguelaneDbContext _context;
        public OddsRepository(LeaguelaneDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddOrUpdateOddAsync(Odd odd, CancellationToken cancellationToken)
        {
            var existing = await _context.Odds.FirstOrDefaultAsync(o => o.FixtureId == odd.FixtureId && o.BookmakerId == odd.BookmakerId && o.BetTypeId == odd.BetTypeId, cancellationToken);
            if (existing == null)
            {
                odd.Created = System.DateTime.UtcNow;
                odd.Active = true;
                await _context.Odds.AddAsync(odd, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return odd.Id;
            }
            else
            {
                existing.LastUpdated = odd.LastUpdated;
                existing.Updated = System.DateTime.UtcNow;
                existing.Active = odd.Active;
                _context.Odds.Update(existing);
                await _context.SaveChangesAsync(cancellationToken);
                return existing.Id;
            }
        }

        public async Task AddOrUpdateOddsValuesAsync(List<OddsValue> values, CancellationToken cancellationToken)
        {
            foreach (var value in values)
            {
                var existing = await _context.OddsValues.FirstOrDefaultAsync(v => v.OddsId == value.OddsId && v.Label == value.Label, cancellationToken);
                if (existing == null)
                {
                    await _context.OddsValues.AddAsync(value, cancellationToken);
                }
                else
                {
                    existing.Odd = value.Odd;
                    existing.Updated = System.DateTime.UtcNow;
                    existing.Active = value.Active;
                    _context.OddsValues.Update(existing);
                }
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
