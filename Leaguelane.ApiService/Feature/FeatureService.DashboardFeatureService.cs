using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;

namespace Leaguelane.ApiService.Feature
{
    public class DashboardFeatureService: IDashboardFeatureService
    {
        private readonly IFixtureService _fixtureService;
        private readonly IUserService _userService;

        public DashboardFeatureService(IFixtureService fixtureService, IUserService userService)
        {
            _fixtureService = fixtureService;
            _userService = userService;
        }

        public async Task<BaseResponse> GetDashboard(CancellationToken cancellationToken)
        {
            var totalFixtures = await _fixtureService.GetUpcomingFixturesCountAsync(cancellationToken);
            var missingPreviews = await _fixtureService.GetMissingPreviewsCountAsync(cancellationToken);
            var missingTips = await _fixtureService.GetMissingTipsCountAsync(cancellationToken);

            return new BaseResponse(true, "Dashboard fetched successfully", new DashboardDto
            {
                TotalFixtures = totalFixtures,
                MissingPreview = missingPreviews,
                MissingTips = missingTips
            });
        }
    }
}
