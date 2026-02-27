using Leaguelane.Api.Mappers;
using Leaguelane.ApiService.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Leaguelane.ApiService.Feature
{
    public class FixtureFeatureService: IFixtureFeatureService
    {
        private readonly IFixtureService _fixtureService;
        private readonly IVenueService _venueService;
        private readonly ITeamService _teamService;
        private readonly ILeagueService _leagueService;
        private readonly ITipService _tipService;
        private readonly IPreviewService _previewService;
        private readonly IOddsService _oddsService;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public FixtureFeatureService(IFixtureService fixtureService
            , IVenueService venueService
            , ITeamService teamService
            , ILeagueService leagueService
            , ITipService tipService
            , IPreviewService previewService
            , IOddsService oddsService
            , IServiceScopeFactory serviceScopeFactory)
        {
            _fixtureService = fixtureService;
            _venueService = venueService;
            _teamService = teamService;
            _leagueService = leagueService;
            _tipService = tipService;
            _previewService = previewService;
            _oddsService = oddsService;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<PaginationBaseResponse> GetFixtures(int page, int pageSize,CancellationToken cancellationToken)
        {
            var (fixtures, totalCount) = await _fixtureService.GetAllFixturesAsync(page, pageSize,false, cancellationToken);

            var leagues = await _leagueService.GetAllActiveLeaguesByIds(fixtures.Select(x => (int)x.LeagueId).Distinct().ToList(), cancellationToken);

            var homeTeamIds = fixtures.Where(x => x.HomeTeamId != null).Select(x => (int)x.HomeTeamId).Distinct().ToList();

            var awayTeamIds = fixtures.Where(x => x.AwayTeamId != null).Select(x => (int)x.AwayTeamId).Distinct().ToList();

            var teams = await _teamService.GetAllTeamsByIds(homeTeamIds.Concat(awayTeamIds).Distinct().ToList(), cancellationToken);

            var venues = await _venueService.GetAllVenues(fixtures.Where(x => x.VenueId != null).Select(x => (int) x.VenueId).Distinct().ToList(), cancellationToken);

            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            return new PaginationBaseResponse
            (
                true, 
                "Fixtures fetched successfully", 
                FixtureMapper.MapToApiResponseDto(fixtures, venues, teams, leagues),
                page, 
                pageSize, 
                totalCount, 
                totalPages
            );
        }

        public async Task<BaseResponse> PublishFixture(int fixtureId, CancellationToken cancellationToken)
        {
            await _fixtureService.PublishFixture(fixtureId, cancellationToken);

            return new BaseResponse(true, "Published fixture", true);
        }

        public async Task<BaseResponse> UnPublishFixture(int fixtureId, CancellationToken cancellationToken)
        {
            await _fixtureService.UnPublishFixture(fixtureId, cancellationToken);

            return new BaseResponse(true, "Unpublished fixture", true);
        }

        public async Task<BaseResponse> CreateTips(TipRequestDto tipRequestDto, CancellationToken cancellationToken)
        {
            await _tipService.AddTipAsync(TipMapper.MapToEntity(tipRequestDto), cancellationToken);

            return new BaseResponse(true, "Tip added successfully", true);
        }

        public async Task<BaseResponse> CreatePreview(PreviewRequestDto previewRequestDto, CancellationToken cancellationToken)
        {
            await _previewService.AddPreviewAsync(PreviewMapper.MapToEntity(previewRequestDto), cancellationToken);

            return new BaseResponse(true, "Preview added successfully", true);
        }

        public async Task<BaseResponse> GetFixtureDetailsById(int fixtureId, CancellationToken cancellationToken)
        {
            var fixture = await _fixtureService.GetFixtureByIdAsync(fixtureId, cancellationToken);

            if (fixture == null)
                return new BaseResponse(false, "Fixture not found", null);

            if(!await _oddsService.IsOddExistsAsync(fixtureId, cancellationToken))
            {
                _ = Task.Run(async () =>
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var oddsService = scope.ServiceProvider.GetRequiredService<IOddsService>();

                    await oddsService.FetchAndStoreOddsAsync(fixture, CancellationToken.None);
                });
            }

            var teams = await _teamService.GetAllTeamsByIds([fixture.HomeTeamId ?? 0, fixture.AwayTeamId ?? 0], cancellationToken);

            var league = await _leagueService.GetLeagueByApiIdAsync(fixture.LeagueId, cancellationToken);
            var venue = await _venueService.GetVenueByApiId(fixture.VenueId ?? 0, cancellationToken);

            return new BaseResponse(true, "Fixture fetched successfully", FixtureMapper.FixtureDetailsApiResponseDto(fixture, teams, league, venue));
        }

        public async Task<BaseResponse> GetFeaturedPredictions(int count, CancellationToken cancellationToken)
        {
            var (fixtures, totalCount) = await _fixtureService.GetAllFixturesAsync(1, count, true, cancellationToken);

            if (fixtures == null || !fixtures.Any())
                return new BaseResponse(true, "No predictions found", new { predictions = new List<Prediction>() });

            var homeTeamIds = fixtures.Where(x => x.HomeTeamId != null).Select(x => (int)x.HomeTeamId).Distinct().ToList();
            var awayTeamIds = fixtures.Where(x => x.AwayTeamId != null).Select(x => (int)x.AwayTeamId).Distinct().ToList();

            var teams = await _teamService.GetAllTeamsByIds(homeTeamIds.Concat(awayTeamIds).Distinct().ToList(), cancellationToken);

            var predictions = fixtures.Select(f =>
            {
                var home = teams.FirstOrDefault(t => t.ApiTeamId == f.HomeTeamId);
                var away = teams.FirstOrDefault(t => t.ApiTeamId == f.AwayTeamId);

                return new Prediction
                {
                    Home = home == null ? null : new PredictionTeam { Team = home.Name, LogoUrl = home.LogoUrl },
                    Away = away == null ? null : new PredictionTeam { Team = away.Name, LogoUrl = away.LogoUrl },
                    Time = f.Date?.ToString("HH:mm") ?? string.Empty,
                    Day = f.Date?.ToString("ddd") ?? string.Empty
                };
            }).ToList();

            return new BaseResponse(true, "Predictions fetched successfully", new { predictions });
        }
    }
}
