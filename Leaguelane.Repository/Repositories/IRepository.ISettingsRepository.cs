using Leaguelane.Constants.Enums;
using Leaguelane.Persistence.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public interface ISettingsRepository
    {
        Task<Settings> UpdateSettings(Settings settings, CancellationToken cancellationToken);
        Task<Settings> GetByIdAsync(int settingsId, CancellationToken cancellationToken);
        Task<Settings> GetByNameAsync(SiteSettings name, CancellationToken cancellationToken);
    }
}

