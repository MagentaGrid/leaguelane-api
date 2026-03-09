using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;

namespace Leaguelane.ApiService.Mappers
{
    public static class FixtureMapper
    {
        public static FixtureListItemDto MapToListItemDto(Fixture entity, Dictionary<int, Persistence.Entities.Team> teamNames)
        {
            var homeTeam = teamNames.GetValueOrDefault((int)entity.HomeTeamId);
            var awayTeam = teamNames.GetValueOrDefault((int)entity.AwayTeamId);
            return new FixtureListItemDto
            {
                FixtureId = entity.FixtureId,
                HomeTeam = new FixtureTeamDto { TeamId = entity.HomeTeamId, Name = homeTeam?.Name, Logo = homeTeam?.LogoUrl }, // Fill with actual data if available
                AwayTeam = new FixtureTeamDto { TeamId = entity.AwayTeamId, Name = awayTeam?.Name, Logo = awayTeam?.LogoUrl }, // Fill with actual data if available
                Date = entity.Date.HasValue ? entity.Date.Value.ToString("dddd") : null,
                Time = entity.Date.HasValue ? entity.Date.Value.ToString("HH:mm") : null
            };
        }

        public static FixtureDto MapToDto(Fixture entity)
        {
            return new FixtureDto
            {
                FixtureId = entity.FixtureId,
                Timezone = entity.Timezone,
                Date = entity.Date,
                Time = entity.Time,
                VenueId = entity.VenueId,
                LeagueId = entity.LeagueId,
                SeasonId = entity.SeasonId,
                SportId = entity.SportId,
                RoundId = entity.RoundId,
                Status = entity.Status,
                HomeTeamId = entity.HomeTeamId,
                AwayTeamId = entity.AwayTeamId,
                GoalsHome = entity.GoalsHome,
                GoalsAway = entity.GoalsAway,
                Rank = entity.Rank,
            };
        }

        public static Fixture MapToEntity(FixtureDto dto)
        {
            return new Fixture
            {
                FixtureId = dto.FixtureId,
                Timezone = dto.Timezone,
                Date = dto.Date,
                Time = dto.Time,
                VenueId = dto.VenueId,
                LeagueId = dto.LeagueId,
                SeasonId = dto.SeasonId,
                SportId = dto.SportId,
                RoundId = dto.RoundId,
                Status = dto.Status,
                HomeTeamId = dto.HomeTeamId,
                AwayTeamId = dto.AwayTeamId,
                GoalsHome = dto.GoalsHome,
                GoalsAway = dto.GoalsAway,
                Rank = dto.Rank,
                ApiFixtureId = dto.FixtureId
            };
        }

        public static List<FixtureApiResponseDto> MapToApiResponseDto(List<Fixture> fixtures
            , List<Leaguelane.Persistence.Entities.Venue> venues
            , List<Leaguelane.Persistence.Entities.Team> teams
            , List<Leaguelane.Persistence.Entities.League> leagues)
        {
           var fixtureResponse = new List<FixtureApiResponseDto>();

            foreach (Fixture fixture in fixtures)
            {
                var venue = venues.FirstOrDefault(v => v.ApiVenueId == fixture.VenueId);
                var homeTeam = teams.FirstOrDefault(t => t.ApiTeamId == fixture.HomeTeamId);
                var awayTeam = teams.FirstOrDefault(t => t.ApiTeamId == fixture.AwayTeamId);
                var league = leagues.FirstOrDefault(l => l.ApiLeagueId == fixture.LeagueId);
                fixtureResponse.Add(new FixtureApiResponseDto
                {
                    FixtureId = fixture.FixtureId,
                    Date = fixture.Date,
                    Time = fixture.Time,
                    GoalsHome = fixture.GoalsHome,
                    GoalsAway = fixture.GoalsAway,
                    Status = fixture.Status,
                    ApiFixtureId= fixture.ApiFixtureId,
                    AwayTeamId= fixture.AwayTeamId,
                    AwayTeamName= awayTeam?.Name,
                    HomeTeamId= fixture?.HomeTeamId,
                    HomeTeamName= homeTeam?.Name,
                    VenueId= fixture?.VenueId,
                    VenueName= venue?.Name,
                    LeagueId= fixture.LeagueId,
                    LeagueName= league?.Name,
                    SeasonId= fixture.SeasonId,
                    PublishStatus= fixture.PublishStatus,
                    Rank= fixture.Rank ?? league?.Rank,
                    RoundId= fixture.RoundId,
                    SportId= fixture.SportId,
                    Timezone= fixture.Timezone,
                    IsPreviewAdded= fixture.FixturePreviews.Any(p => p.FixtureId == fixture.FixtureId && p.Active == true),
                    IsTipAdded= fixture.FixtureTips.Any(t => t.FixtureId == fixture.FixtureId && t.Active == true),
                });
            }

            return fixtureResponse;
        }

        public static FixtureDetailsApiResponseDto FixtureDetailsApiResponseDto(Fixture fixture
            , List<Leaguelane.Persistence.Entities.Team> teams
            , League? league
            , Persistence.Entities.Venue? venue)
        {
            var homeTeam = teams.FirstOrDefault(t => t.ApiTeamId == fixture.HomeTeamId);
            var awayTeam = teams.FirstOrDefault(t => t.ApiTeamId == fixture.AwayTeamId);
            var preview = fixture.FixturePreviews.FirstOrDefault(p => p.FixtureId == fixture.FixtureId);
            return new FixtureDetailsApiResponseDto
            {
                FixtureId = fixture.FixtureId,
                Date = fixture.Date,
                Time = fixture.Time,
                GoalsHome = fixture.GoalsHome,
                GoalsAway = fixture.GoalsAway,
                Status = fixture.Status,
                ApiFixtureId = fixture.ApiFixtureId,
                AwayTeamId = fixture.AwayTeamId,
                AwayTeamName = awayTeam?.Name,
                HomeTeamId = fixture?.HomeTeamId,
                HomeTeamName = homeTeam?.Name,
                VenueId = fixture?.VenueId,
                LeagueId = fixture.LeagueId,
                VenueName = venue?.Name,
                LeagueName = league?.Name,
                SeasonId = fixture.SeasonId,
                PublishStatus = fixture.PublishStatus,
                Rank = fixture.Rank ?? league?.Rank,
                RoundId = fixture.RoundId,
                SportId = fixture.SportId,
                Timezone = fixture.Timezone,
                Tips = fixture.FixtureTips != null ? fixture.FixtureTips.Where(t => t.Active == true).Select(t => new TipsResponse
                {
                    FixtureTipId = t.FixtureTipId,
                    FixtureId = t.FixtureId,
                    BetId = t.BetId,
                    BookmakerId = t.BookmakerId,
                    IsSaved = t.IsSaved,
                    IsVisible = t.IsVisible,
                    OddsValueId = t.OddsValueId,
                    Reasoning = t.Reasoning,
                    Title = t.Title,
                    BetName = t.Bet?.Name,
                    BookmakerName = t.Bookmaker?.Name,
                    OddsValueName = t.OddsValue?.Label
                }).ToList() : null,
                Preview = preview != null ? new PreviewResponse
                {
                    FixturePreviewId = preview.FixturePreviewId,
                    FixtureId = preview.FixtureId,
                    FullAnalysis = preview.FullAnalysis,
                    Headline = preview.Headline,
                    ShortIntro = preview.ShortIntro
                } : null
            };
        }
    }
}
