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

        public async Task AddFixturesBatchAsync(List<Fixture> fixture, CancellationToken cancellationToken)
        {
            var fixtureIds = fixture.Select(x => x.ApiFixtureId).ToList();

            var existingFixtures = await _context.Fixtures.Where(x => fixtureIds.Contains(x.ApiFixtureId)).ToListAsync(cancellationToken);

            var fixturesToBeAdded = fixture.Where(x => !existingFixtures.Any(y => y.ApiFixtureId == x.FixtureId)).ToList();

            await _context.Fixtures.AddRangeAsync(fixturesToBeAdded);
            await _context.SaveChangesAsync(cancellationToken);
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
    }
}
