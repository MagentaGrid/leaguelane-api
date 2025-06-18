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
    public class AboutRepository : IAboutRepository
    {
        private readonly LeaguelaneDbContext _context;
        public AboutRepository(LeaguelaneDbContext context)
        {
            _context = context;
        }

        public async Task<About> CreateAbout(About about, CancellationToken cancellationToken)
        {
            _context.Abouts.Add(about);
            await _context.SaveChangesAsync(cancellationToken);
            return about;
        }

        public async Task<About> UpdateAbout(About about, CancellationToken cancellationToken)
        {
            _context.Abouts.Update(about);
            await _context.SaveChangesAsync(cancellationToken);
            return about;
        }

        public async Task<About> GetAbout(int id, CancellationToken cancellationToken)
        {
            return await _context.Abouts.Where(x => x.Active == true && x.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<About>> GetAllAbouts(CancellationToken cancellationToken)
        {
            return await _context.Abouts.Where(x => x.Active == true).ToListAsync(cancellationToken);
        }
    }
}
