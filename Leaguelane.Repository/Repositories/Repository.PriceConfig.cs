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
    public class PriceConfigRepository : IPriceConfigRepository
    {
        private readonly LeaguelaneDbContext _context;

        public PriceConfigRepository(LeaguelaneDbContext context)
        {
            _context = context;
        }

        public async Task<List<PriceConfig>> GetAllPriceConfigs(CancellationToken cancellationToken)
        {
            return await _context.PriceConfigs.AsNoTracking().Where(x => x.Active == true).ToListAsync(cancellationToken);
        }

        public async Task<PriceConfig> GetPriceConfigById(int id, CancellationToken cancellationToken)
        {
            return await _context.PriceConfigs.AsNoTracking().Where(x => x.PriceConfigId == id && x.Active == true).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PriceConfig> CreatePriceConfig(PriceConfig priceConfig, CancellationToken cancellationToken)
        {
            _context.PriceConfigs.Add(priceConfig);
            await _context.SaveChangesAsync(cancellationToken);
            return priceConfig;
        }

        public async Task<PriceConfig> UpdatePriceConfig(PriceConfig priceConfig, CancellationToken cancellationToken)
        {
            _context.PriceConfigs.Update(priceConfig);
            await _context.SaveChangesAsync(cancellationToken);
            return priceConfig;
        }

        public async Task<bool> IsPriceConfigNameExists(string priceConfigName, CancellationToken cancellationToken)
        {
            return await _context.PriceConfigs.AnyAsync(x => x.PriceConfigName == priceConfigName && x.Active == true, cancellationToken);
        }

        public async Task<bool> IsPriceConfigNameExistsForUpdate(string priceConfigName, int priceConfigId, CancellationToken cancellationToken)
        {
            return await _context.PriceConfigs.AnyAsync(x => x.PriceConfigName == priceConfigName && x.PriceConfigId != priceConfigId && x.Active == true, cancellationToken);
        }
    }
}
