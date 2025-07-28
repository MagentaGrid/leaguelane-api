using Leaguelane.Persistence.Context;
using Leaguelane.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Leaguelane.Repository.Repositories
{
    public class BookmakerRepository : IBookmakerRepository
    {
        private readonly LeaguelaneDbContext _context;

        public BookmakerRepository(LeaguelaneDbContext context)
        {
            _context = context;
        }

        public async Task<List<Bookmaker>> AddBookmakers(List<string> bookmakerNames, CancellationToken cancellationToken)
        {
            var existingBookmakers = await _context.Bookmakers.Where(x => x.Active == true).ToListAsync(cancellationToken);
            var existingNames = existingBookmakers.Select(b => b.Name).ToList();
            var newBookmakers = new List<Bookmaker>();

            foreach (var name in bookmakerNames)
            {
                if (!existingNames.Contains(name))
                {
                    var newBookmaker = new Bookmaker
                    {
                        Active = true,
                        Created = DateTime.UtcNow,
                        Name = name
                    };
                    newBookmakers.Add(newBookmaker);
                }
            }

            await _context.Bookmakers.AddRangeAsync(newBookmakers, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return newBookmakers;
        }
    }
}
