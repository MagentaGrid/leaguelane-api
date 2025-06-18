using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public class PriceConfigService : IPriceConfigService
    {
        private readonly IPriceConfigRepository _priceConfigRepository;

        public PriceConfigService(IPriceConfigRepository priceConfigRepository)
        {
            _priceConfigRepository = priceConfigRepository;
        }

        public async Task<List<PriceConfig>> GetAllPriceConfigs(CancellationToken cancellationToken)
        {
            return await _priceConfigRepository.GetAllPriceConfigs(cancellationToken);
        }

        public async Task<PriceConfig> GetPriceConfigById(int id, CancellationToken cancellationToken)
        {
            return await _priceConfigRepository.GetPriceConfigById(id, cancellationToken);
        }

        public async Task<PriceConfig> CreatePriceConfig(PriceConfig priceConfig, CancellationToken cancellationToken)
        {
            return await _priceConfigRepository.CreatePriceConfig(priceConfig, cancellationToken);
        }

        public async Task<PriceConfig> UpdatePriceConfig(PriceConfig priceConfig, CancellationToken cancellationToken)
        {
            return await _priceConfigRepository.UpdatePriceConfig(priceConfig, cancellationToken);
        }

        public async Task<bool> IsPriceConfigNameExists(string priceConfigName, CancellationToken cancellationToken)
        {
            return await _priceConfigRepository.IsPriceConfigNameExists(priceConfigName, cancellationToken);
        }

        public async Task<bool> IsPriceConfigNameExistsForUpdate(string priceConfigName, int priceConfigId, CancellationToken cancellationToken)
        {
            return await _priceConfigRepository.IsPriceConfigNameExistsForUpdate(priceConfigName, priceConfigId, cancellationToken);
        }
    }
}
