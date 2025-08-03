using Leaguelane.Persistence.Context;
using Leaguelane.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public class TeamStatRepository : ITeamStatRepository
    {
        private readonly LeaguelaneDbContext _context;
        public TeamStatRepository(LeaguelaneDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddOrUpdateTeamStatAsync(TeamStat stat, CancellationToken cancellationToken)
        {
            var existing = await _context.TeamStats.FirstOrDefaultAsync(s => s.TeamId == stat.TeamId && s.LeagueId == stat.LeagueId && s.SeasonId == stat.SeasonId && s.SportId == stat.SportId, cancellationToken);
            if (existing == null)
            {
                stat.Created = System.DateTime.UtcNow;
                stat.Active = true;
                await _context.TeamStats.AddAsync(stat, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return stat.Id;
            }
            else
            {
                existing.Updated = System.DateTime.UtcNow;
                existing.Active = stat.Active;
                _context.TeamStats.Update(existing);
                await _context.SaveChangesAsync(cancellationToken);
                return existing.Id;
            }
        }

        public async Task AddOrUpdateTeamStatFixturesAsync(List<TeamStatFixture> fixtures, CancellationToken cancellationToken)
        {
            foreach (var fixture in fixtures)
            {
                var existing = await _context.TeamStatFixtures.FirstOrDefaultAsync(f => f.TeamStatsId == fixture.TeamStatsId && f.ResultType == fixture.ResultType, cancellationToken);
                if (existing == null)
                {
                    await _context.TeamStatFixtures.AddAsync(fixture, cancellationToken);
                }
                else
                {
                    existing.Home = fixture.Home;
                    existing.Away = fixture.Away;
                    existing.Total = fixture.Total;
                    _context.TeamStatFixtures.Update(existing);
                }
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddOrUpdateTeamStatGoalsAsync(List<TeamStatGoal> goals, CancellationToken cancellationToken)
        {
            foreach (var goal in goals)
            {
                var existing = await _context.TeamStatGoals.FirstOrDefaultAsync(g => g.TeamStatsId == goal.TeamStatsId && g.Type == goal.Type && g.Metric == goal.Metric, cancellationToken);
                if (existing == null)
                {
                    await _context.TeamStatGoals.AddAsync(goal, cancellationToken);
                }
                else
                {
                    existing.Home = goal.Home;
                    existing.Away = goal.Away;
                    existing.Total = goal.Total;
                    _context.TeamStatGoals.Update(existing);
                }
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
