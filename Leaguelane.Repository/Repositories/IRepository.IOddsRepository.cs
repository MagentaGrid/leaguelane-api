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
        Task<List<Odd>> GetOddsAsync(int? fixtureId, int? bookmakerId, string? market, int skip, int take, bool onlyActive, CancellationToken cancellationToken);
        Task<List<Odd>> GetAllOddsAsync(CancellationToken cancellationToken);
        Task<Odd> GetOddsByIdAsync(int id, bool onlyActive, CancellationToken cancellationToken);
        Task<List<Odd>> GetDeletedOddsAsync(CancellationToken cancellationToken);
        Task UpdateOddsAsync(Odd odd, List<OddsValue> values, CancellationToken cancellationToken);
        Task SoftDeleteOddsAsync(int id, CancellationToken cancellationToken);
        Task RestoreOddsAsync(int id, CancellationToken cancellationToken);
        Task<List<OddsValue>> GetOddsValuesAsync(int oddsId, CancellationToken cancellationToken);
    }
}
