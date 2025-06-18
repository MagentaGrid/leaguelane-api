using Leaguelane.Constants.Enums;
using Leaguelane.Persistence.Context;
using Leaguelane.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly LeaguelaneDbContext _context;

        public SettingsRepository(LeaguelaneDbContext context)
        {
            _context = context;
        }

        public async Task<Settings> UpdateSettings(Settings settings, CancellationToken cancellationToken)
        {
            _context.Settings.Update(settings);
            await _context.SaveChangesAsync(cancellationToken);
            return settings;
        }

        public async Task<Settings> GetByIdAsync(int settingsId, CancellationToken cancellationToken)
        {
            return await _context.Settings.FindAsync(new object[] { settingsId }, cancellationToken);
        }

        public async Task<Settings> GetByNameAsync(SiteSettings name, CancellationToken cancellationToken) // Implemented method
        {
            return await _context.Settings.FirstOrDefaultAsync(s => s.Name == name.ToString(), cancellationToken);
        }
    }
}

