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
    public class LeagueRepository: ILeagueRepository
    {
        private readonly LeaguelaneDbContext _context;

        public LeagueRepository(LeaguelaneDbContext context)
        {
            _context = context;
        }

        public async Task<League> AddLeague(League league, CancellationToken cancellationToken)
        {
            _context.Leagues.Add(league);
            await _context.SaveChangesAsync(cancellationToken);
            return league;
        }

        public async Task<bool> AddLeagues(List<League> leagues, CancellationToken cancellationToken)
        {
            var existingLeagues = _context.Leagues.ToList();
            var leaguesToBeAdded = leagues.Where(x => !existingLeagues.Any(y => y.ApiLeagueId == x.ApiLeagueId)).ToList();
            _context.Leagues.AddRange(leaguesToBeAdded);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<League>> GetAllActiveLeagues(CancellationToken cancellationToken)
        {
            return await _context.Leagues.Where(x => x.Active == true).ToListAsync(cancellationToken);
        }
    }
}
