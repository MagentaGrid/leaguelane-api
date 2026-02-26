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

        public async Task<List<Odd>> GetOddsAsync(int? fixtureId, int? bookmakerId, string? market, int skip, int take, bool onlyActive, CancellationToken cancellationToken)
        {
            var query = _context.Odds.AsQueryable();
            if (onlyActive)
                query = query.Where(o => o.Active == true);
            if (fixtureId.HasValue)
                query = query.Where(o => o.FixtureId == fixtureId);
            if (bookmakerId.HasValue)
                query = query.Where(o => o.BookmakerId == bookmakerId);
            // Market filter can be implemented if market is mapped to BetTypeId or similar
            return await query.Skip(skip).Take(take).ToListAsync(cancellationToken);
        }

        public async Task<List<Odd>> GetAllOddsAsync(CancellationToken cancellationToken)
        {
            return await _context.Odds.ToListAsync(cancellationToken);
        }

        public async Task<Odd> GetOddsByIdAsync(int id, bool onlyActive, CancellationToken cancellationToken)
        {
            var query = _context.Odds.AsQueryable();
            if (onlyActive)
                query = query.Where(o => o.Active == true);
            return await query.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task<List<Odd>> GetDeletedOddsAsync(CancellationToken cancellationToken)
        {
            return await _context.Odds.Where(o => o.Active == false).ToListAsync(cancellationToken);
        }

        public async Task UpdateOddsAsync(Odd odd, List<OddsValue> values, CancellationToken cancellationToken)
        {
            var existing = await _context.Odds.FirstOrDefaultAsync(o => o.Id == odd.Id, cancellationToken);
            if (existing != null)
            {
                existing.FixtureId = odd.FixtureId;
                existing.LeagueId = odd.LeagueId;
                existing.SeasonId = odd.SeasonId;
                existing.SportId = odd.SportId;
                existing.BookmakerId = odd.BookmakerId;
                existing.BetTypeId = odd.BetTypeId;
                existing.LastUpdated = odd.LastUpdated;
                existing.Updated = System.DateTime.UtcNow;
                _context.Odds.Update(existing);
                await _context.SaveChangesAsync(cancellationToken);
                // Update values
                var oddsValues = await _context.OddsValues.Where(v => v.OddsId == odd.Id).ToListAsync(cancellationToken);
                foreach (var value in values)
                {
                    var existingValue = oddsValues.FirstOrDefault(v => v.Label == value.Label);
                    if (existingValue != null)
                    {
                        existingValue.Odd = value.Odd;
                        existingValue.Updated = System.DateTime.UtcNow;
                        _context.OddsValues.Update(existingValue);
                    }
                    else
                    {
                        await _context.OddsValues.AddAsync(new OddsValue
                        {
                            OddsId = odd.Id,
                            Label = value.Label,
                            Odd = value.Odd,
                            Created = System.DateTime.UtcNow,
                            Active = true
                        }, cancellationToken);
                    }
                }
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task SoftDeleteOddsAsync(int id, CancellationToken cancellationToken)
        {
            var odd = await _context.Odds.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
            if (odd != null)
            {
                odd.Active = false;
                odd.Updated = System.DateTime.UtcNow;
                _context.Odds.Update(odd);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task RestoreOddsAsync(int id, CancellationToken cancellationToken)
        {
            var odd = await _context.Odds.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
            if (odd != null)
            {
                odd.Active = true;
                odd.Updated = System.DateTime.UtcNow;
                _context.Odds.Update(odd);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<List<OddsValue>> GetOddsValuesAsync(int oddsId, CancellationToken cancellationToken)
        {
            return await _context.OddsValues.Where(v => v.OddsId == oddsId && v.Active == true).ToListAsync(cancellationToken);
        }

        public async Task<bool> IsOddExistsAsync(int fixtureId, int bookmakerId, int betTypeId, CancellationToken cancellationToken)
        {
            return await _context.Odds.AnyAsync(o => o.FixtureId == fixtureId && o.BookmakerId == bookmakerId && o.BetTypeId == betTypeId, cancellationToken);
        }
    }
}
