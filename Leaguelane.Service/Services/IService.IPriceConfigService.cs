using Leaguelane.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public interface IPriceConfigService
    {
        Task<List<PriceConfig>> GetAllPriceConfigs(CancellationToken cancellationToken);
        Task<PriceConfig> GetPriceConfigById(int id, CancellationToken cancellationToken);
        Task<PriceConfig> CreatePriceConfig(PriceConfig priceConfig, CancellationToken cancellationToken);
        Task<PriceConfig> UpdatePriceConfig(PriceConfig priceConfig, CancellationToken cancellationToken);
        Task<bool> IsPriceConfigNameExists(string priceConfigName, CancellationToken cancellationToken);
        Task<bool> IsPriceConfigNameExistsForUpdate(string priceConfigName, int priceConfigId, CancellationToken cancellationToken);
    }
}
