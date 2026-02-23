using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;

namespace Leaguelane.ApiService.Feature
{
    public class FixtureFeatureService: IFixtureFeatureService
    {
        private readonly IFixtureService _fixtureService;
        public FixtureFeatureService(IFixtureService fixtureService)
        {
            _fixtureService = fixtureService;
        }

        public async Task<PaginationBaseResponse> GetFixtures(int page, int pageSize,CancellationToken cancellationToken)
        {
            var (fixtures, totalCount) = await _fixtureService.GetAllFixturesAsync(page, pageSize,false, cancellationToken);

            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            return new PaginationBaseResponse(true, "Fixtures fetched successfully", fixtures, page, pageSize, totalCount, totalPages);
        }
    }
}
