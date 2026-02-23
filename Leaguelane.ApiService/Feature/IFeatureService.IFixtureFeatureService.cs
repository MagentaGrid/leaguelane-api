using Leaguelane.Models.Dtos;

namespace Leaguelane.ApiService.Feature
{
    public interface IFixtureFeatureService
    {
        Task<PaginationBaseResponse> GetFixtures(int page, int pageSize, CancellationToken cancellationToken);
    }
}
