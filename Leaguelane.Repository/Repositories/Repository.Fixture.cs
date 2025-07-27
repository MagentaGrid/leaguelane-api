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
    public class FixtureRepository: IFixtureRepository
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
            var fixtureIds = fixture.Select(x => x.FixtureId).ToList();

            var existingFixtures = await _context.Fixtures.Where(x => fixtureIds.Contains(x.FixtureId)).ToListAsync(cancellationToken);

            var fixturesToBeAdded = fixture.Where(x => !existingFixtures.Any(y => y.FixtureId == x.FixtureId)).ToList();

            await _context.Fixtures.AddRangeAsync(fixturesToBeAdded);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
