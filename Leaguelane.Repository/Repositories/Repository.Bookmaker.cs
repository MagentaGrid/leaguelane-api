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

        public async Task<List<Bookmaker>> AddBookmakers(List<Bookmaker> bookmakers, CancellationToken cancellationToken)
        {
            var existingBookmakers = await _context.Bookmakers.Where(x => x.Active == true).ToListAsync(cancellationToken);
            var existingApiIds = existingBookmakers.Select(b => (int)b.ApiBookMakerId).ToList();
            var newBookmakers = bookmakers.Where(b => !existingApiIds.Contains(b.ApiBookMakerId)).ToList();

            await _context.Bookmakers.AddRangeAsync(newBookmakers, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return newBookmakers;
        }

        public async Task<List<Bookmaker>> GetActiveBookmakersAsync(CancellationToken cancellationToken)
        {
            return await _context.Bookmakers.Where(b => b.Active == true).ToListAsync(cancellationToken);
        }

        public async Task<List<Bookmaker>> GetAllBookmakersAsync(CancellationToken cancellationToken)
        {
            return await _context.Bookmakers.ToListAsync(cancellationToken);
        }

        public async Task<Bookmaker?> GetBookmakerByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Bookmakers.FirstOrDefaultAsync(b => b.BookmakerId == id && b.Active == true, cancellationToken);
        }

        public async Task<Bookmaker> CreateBookmakerAsync(Bookmaker bookmaker, CancellationToken cancellationToken)
        {
            bookmaker.Created = DateTime.UtcNow;
            bookmaker.Active = true;
            _context.Bookmakers.Add(bookmaker);
            await _context.SaveChangesAsync(cancellationToken);
            return bookmaker;
        }

        public async Task<Bookmaker> UpdateBookmakerAsync(Bookmaker bookmaker, CancellationToken cancellationToken)
        {
            var existing = await _context.Bookmakers.FindAsync(new object[] { bookmaker.BookmakerId }, cancellationToken);
            if (existing == null) throw new KeyNotFoundException("Bookmaker not found");
            existing.Name = bookmaker.Name;
            existing.ApiBookMakerId = bookmaker.ApiBookMakerId;
            existing.AffiliateLink = bookmaker.AffiliateLink;
            existing.BookieLogo = bookmaker.BookieLogo;
            existing.Updated = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
            return existing;
        }

        public async Task SoftDeleteBookmakerAsync(int id, CancellationToken cancellationToken)
        {
            var bookmaker = await _context.Bookmakers.FindAsync(new object[] { id }, cancellationToken);
            if (bookmaker == null) throw new KeyNotFoundException("Bookmaker not found");
            bookmaker.Active = false;
            bookmaker.Updated = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RestoreBookmakerAsync(int id, CancellationToken cancellationToken)
        {
            var bookmaker = await _context.Bookmakers.FindAsync(new object[] { id }, cancellationToken);
            if (bookmaker == null) throw new KeyNotFoundException("Bookmaker not found");
            bookmaker.Active = true;
            bookmaker.Updated = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Bookmaker>> GetDeletedBookmakersAsync(CancellationToken cancellationToken)
        {
            return await _context.Bookmakers.Where(b => b.Active == false).ToListAsync(cancellationToken);
        }
    }
}
