using Leaguelane.Persistence.Context;
using Leaguelane.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public class BetRepository : IBetRepository
    {
        private readonly LeaguelaneDbContext _context;

        public BetRepository(LeaguelaneDbContext context)
        {
            _context = context;
        }

        public async Task<List<Bet>> AddBets(List<Bet> bets, CancellationToken cancellationToken)
        {
            var fixtureIds = bets.Select(b => b.FixtureId).ToList();
            var existingBets = await _context.Bets.Where(x => fixtureIds.Contains(x.FixtureId)).ToListAsync(cancellationToken);
            var newBets = bets.Where(b => !existingBets.Any(e => e.FixtureId == b.FixtureId)).ToList();
            await _context.Bets.AddRangeAsync(newBets, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return newBets;
        }
    }
}
