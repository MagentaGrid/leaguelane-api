using Leaguelane.Constants.Enums;
using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository _settingsRepository;

        public SettingsService(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public async Task<Settings> UpdateSettings(Settings settings, CancellationToken cancellationToken)
        {
            return await _settingsRepository.UpdateSettings(settings, cancellationToken);
        }

        public async Task<Settings> GetSettingsByIdAsync(int settingsId, CancellationToken cancellationToken) 
        {
            return await _settingsRepository.GetByIdAsync(settingsId, cancellationToken);
        }

        public async Task<Settings> GetSettingsByNameAsync(SiteSettings name, CancellationToken cancellationToken) // Implemented method
        {
            return await _settingsRepository.GetByNameAsync(name, cancellationToken);
        }
    }
}
