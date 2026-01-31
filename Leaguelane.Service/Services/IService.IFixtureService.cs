using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;

namespace Leaguelane.Service.Services
{
    public interface IFixtureService
    {
        Task GetAllFixtures(CancellationToken cancellationToken);
        Task GetAllFixturesByLeagueAndSeason(int leagueId, int season, CancellationToken cancellationToken);
        Task<List<Fixture>> GetAllFixturesAsync(CancellationToken cancellationToken);
        Task<Fixture> GetFixtureByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateFixtureAsync(Fixture fixture, CancellationToken cancellationToken);
        Task SoftDeleteFixtureAsync(int id, CancellationToken cancellationToken);
        Task SetRankAsync(int fixtureId, int rank, CancellationToken cancellationToken);
        Task<List<FixtureListItemDto>> GetLatestFixturesAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<List<Fixture>> GetAllFixturesWithPaginationAsync(int page, int pageSize, CancellationToken cancellationToken);
    }
}
