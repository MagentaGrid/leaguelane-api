using Leaguelane.Persistence.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public interface IFixtureRepository
    {
        Task<Fixture> AddFixtureAsync(Fixture fixture, CancellationToken cancellationToken);
        Task AddFixturesBatchAsync(List<Fixture> fixture, CancellationToken cancellationToken);
        Task<List<Fixture>> GetAllFixturesAsync(CancellationToken cancellationToken);
        Task<Fixture> GetFixtureByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateFixtureAsync(Fixture fixture, CancellationToken cancellationToken);
        Task SoftDeleteFixtureAsync(int id, CancellationToken cancellationToken);
        Task SetRankAsync(int fixtureId, int rank, CancellationToken cancellationToken);
        Task<List<Fixture>> GetLatestFixturesAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<List<Fixture>> GetFixturesForNextFourteenDaysAsync(CancellationToken cancellationToken);
    }
}
