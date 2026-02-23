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
        Task<(List<Fixture>, int)> GetAllFixturesAsync(int page, int pageSize, bool publishStatus, CancellationToken cancellationToken);
        Task<Fixture> GetFixtureByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateFixtureAsync(Fixture fixture, CancellationToken cancellationToken);
        Task SoftDeleteFixtureAsync(int id, CancellationToken cancellationToken);
        Task SetRankAsync(int fixtureId, int rank, CancellationToken cancellationToken);
        Task<List<FixtureListItemDto>> GetLatestFixturesAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<List<Fixture>> GetAllFixturesWithPaginationAsync(int page, int pageSize, CancellationToken cancellationToken);
        Task<int> GetUpcomingFixturesCountAsync(CancellationToken cancellationToken);
        Task<int> GetMissingPreviewsCountAsync(CancellationToken cancellationToken);
        Task<int> GetMissingTipsCountAsync(CancellationToken cancellationToken);
    }
}
