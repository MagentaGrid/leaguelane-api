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
    public class SportRepository: ISportRepository
    {
        private readonly LeaguelaneDbContext _context;

        public SportRepository(LeaguelaneDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sport>> GetAllSports(CancellationToken cancellationToken)
        {
            return await _context.Sports.Where(x => x.Active == true).ToListAsync(cancellationToken);
        }

        public async Task<Sport> GetSport(int id, CancellationToken cancellationToken)
        {
            return await _context.Sports.Where(x => x.Active == true && x.SportId == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Sport> AddSport(Sport sport, CancellationToken cancellationToken)
        {
            _context.Sports.Add(sport);
            await _context.SaveChangesAsync(cancellationToken);
            return sport;
        }

        public async Task<Sport> UpdateSport(Sport sport, CancellationToken cancellationToken)
        {
            _context.Sports.Update(sport);
            await _context.SaveChangesAsync(cancellationToken);
            return sport;
        }
    }
}
