using Leaguelane.Constants.Enums;
using Leaguelane.Persistence.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public interface ISettingsService
    {
        Task<Settings> UpdateSettings(Settings settings, CancellationToken cancellationToken);
        Task<Settings> GetSettingsByIdAsync(int settingsId, CancellationToken cancellationToken);
        Task<Settings> GetSettingsByNameAsync(SiteSettings name, CancellationToken cancellationToken);
    }
}
