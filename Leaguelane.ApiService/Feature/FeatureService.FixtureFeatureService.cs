using Leaguelane.ApiService.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;

namespace Leaguelane.ApiService.Feature
{
    public class FixtureFeatureService: IFixtureFeatureService
    {
        private readonly IFixtureService _fixtureService;
        private readonly IVenueService _venueService;
        private readonly ITeamService _teamService;
        private readonly ILeagueService _leagueService;
        public FixtureFeatureService(IFixtureService fixtureService, IVenueService venueService, ITeamService teamService, ILeagueService leagueService)
        {
            _fixtureService = fixtureService;
            _venueService = venueService;
            _teamService = teamService;
            _leagueService = leagueService;
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
    }
}
