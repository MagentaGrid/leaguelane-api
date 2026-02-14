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
    public class FixtureRepository : IFixtureRepository
    {
        private readonly LeaguelaneDbContext _context;

        public FixtureRepository(LeaguelaneDbContext context)
        {
            _context = context;
        }

        public async Task<Fixture> AddFixtureAsync(Fixture fixture, CancellationToken cancellationToken)
        {
            await _context.Fixtures.AddAsync(fixture);
            await _context.SaveChangesAsync(cancellationToken);
            return fixture;
        }

        public async Task AddFixturesBatchAsync(List<Fixture> fixtures, CancellationToken cancellationToken)
        {
            var incomingApiIds = fixtures.Select(x => x.ApiFixtureId).ToList();

            // 1. Get IDs that already exist in the DB
            var existingApiIds = await _context.Fixtures
                .Where(x => incomingApiIds.Contains(x.ApiFixtureId))
                .Select(x => x.ApiFixtureId)
                .ToListAsync(cancellationToken);

            // 2. Only add fixtures whose ApiFixtureId is NOT in the database
            var fixturesToBeAdded = fixtures
                .Where(x => !existingApiIds.Contains(x.ApiFixtureId))
                .ToList();

            if (fixturesToBeAdded.Any())
            {
                await _context.Fixtures.AddRangeAsync(fixturesToBeAdded, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<List<Fixture>> GetAllFixturesAsync(CancellationToken cancellationToken)
        {
            return await _context.Fixtures.Where(f => f.Active == true).ToListAsync(cancellationToken);
        }

        public async Task<Fixture> GetFixtureByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Fixtures.FirstOrDefaultAsync(f => f.FixtureId == id && f.Active == true, cancellationToken);
        }

        public async Task UpdateFixtureAsync(Fixture fixture, CancellationToken cancellationToken)
        {
            _context.Fixtures.Update(fixture);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task SoftDeleteFixtureAsync(int id, CancellationToken cancellationToken)
        {
            var fixture = await _context.Fixtures.FirstOrDefaultAsync(f => f.FixtureId == id, cancellationToken);
            if (fixture != null)
            {
                fixture.Active = false;
                fixture.Updated = DateTime.UtcNow;
                _context.Fixtures.Update(fixture);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task SetRankAsync(int fixtureId, int rank, CancellationToken cancellationToken)
        {
            var fixture = await _context.Fixtures.FirstOrDefaultAsync(f => f.FixtureId == fixtureId, cancellationToken);
            if (fixture != null)
            {
                // Add a Rank property to Fixture entity if not present
                fixture.Rank = rank;
                fixture.Updated = DateTime.UtcNow;
                _context.Fixtures.Update(fixture);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<List<Fixture>> GetLatestFixturesAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            // Order by Rank descending, then by date descending
            return await _context.Fixtures
                .Where(f => f.Active == true)
                .OrderByDescending(f => f.Rank)
                .ThenByDescending(f => f.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Fixture>> GetFixturesForNextFourteenDaysAsync(CancellationToken cancellationToken)
        {
            return await _context.Fixtures
                .Where(f => f.Date >= DateTime.Now && f.Date <= DateTime.Now.AddDays(14) && f.Active == true)
                .OrderByDescending(f => f.Date)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Fixture>> GetAllFixturesWithPaginationAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            return await _context.Fixtures.Where(f => f.Active == true && f.Date != null && f.Date.Value >= DateTime.Now.Date)
                .OrderByDescending(f => f.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }
    }
}
