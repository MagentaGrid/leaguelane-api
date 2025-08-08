using Leaguelane.Persistence.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public interface IOddsRepository
    {
        Task<int> AddOrUpdateOddAsync(Odd odd, CancellationToken cancellationToken);
        Task AddOrUpdateOddsValuesAsync(List<OddsValue> values, CancellationToken cancellationToken);
    }
}
