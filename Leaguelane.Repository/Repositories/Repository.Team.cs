using Leaguelane.Persistence.Context;
using Leaguelane.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly LeaguelaneDbContext _context;
        public TeamRepository(LeaguelaneDbContext context)
        {
            _context = context;
        }
        public async Task AddOrUpdateTeamsAsync(List<Team> teams, CancellationToken cancellationToken)
        {
            var apiTeamIds = teams.Select(t => t.ApiTeamId).ToList();
            var existingTeams = await _context.Teams.Where(t => apiTeamIds.Contains(t.ApiTeamId)).ToListAsync(cancellationToken);
            foreach (var team in teams)
            {
                var existing = existingTeams.FirstOrDefault(t => t.ApiTeamId == team.ApiTeamId);
                if (existing == null)
                {
                    team.Created = System.DateTime.UtcNow;
                    team.Active = true;
                    await _context.Teams.AddAsync(team, cancellationToken);
                }
                else
                {
                    existing.Name = team.Name;
                    existing.Code = team.Code;
                    existing.Country = team.Country;
                    existing.Founded = team.Founded;
                    existing.National = team.National;
                    existing.LogoUrl = team.LogoUrl;
                    existing.SportId = team.SportId;
                    existing.LeagueId = team.LeagueId;
                    existing.SeasonId = team.SeasonId;
                    existing.Updated = System.DateTime.UtcNow;
                    existing.Active = team.Active;
                    _context.Teams.Update(existing);
                }
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
