using Leaguelane.Persistence.Context;
using Leaguelane.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public class RoundRepository : IRoundRepository
    {
        private readonly LeaguelaneDbContext _context;
        public RoundRepository(LeaguelaneDbContext context)
        {
            _context = context;
        }

        public async Task AddOrUpdateRoundsAsync(List<Round> rounds, CancellationToken cancellationToken)
        {
            var roundNames = rounds.Select(r => r.Name).ToList();
            var leagueIds = rounds.Select(r => r.LeagueId).ToList();
            var seasonIds = rounds.Select(r => r.SeasonId).ToList();
            var sportIds = rounds.Select(r => r.SportId).ToList();

            var existingRounds = await _context.Rounds
                .Where(r => roundNames.Contains(r.Name) && leagueIds.Contains(r.LeagueId) && seasonIds.Contains(r.SeasonId) && sportIds.Contains(r.SportId))
                .ToListAsync(cancellationToken);

            foreach (var round in rounds)
            {
                var existing = existingRounds.FirstOrDefault(r => r.Name == round.Name && r.LeagueId == round.LeagueId && r.SeasonId == round.SeasonId && r.SportId == round.SportId);
                if (existing == null)
                {
                    round.Created = System.DateTime.UtcNow;
                    round.Active = true;
                    await _context.Rounds.AddAsync(round, cancellationToken);
                }
                else
                {
                    existing.Updated = System.DateTime.UtcNow;
                    existing.Active = round.Active;
                    existing.Name = round.Name;
                    _context.Rounds.Update(existing);
                }
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
