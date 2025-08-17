using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;

namespace Leaguelane.ApiService.Mappers
{
    public static class FixtureMapper
    {
        public static FixtureListItemDto MapToListItemDto(Fixture entity)
        {
            return new FixtureListItemDto
            {
                FixtureId = entity.FixtureId,
                HomeTeam = new FixtureTeamDto { TeamId = entity.HomeTeamId, Name = "", Logo = "" }, // Fill with actual data if available
                AwayTeam = new FixtureTeamDto { TeamId = entity.AwayTeamId, Name = "", Logo = "" },
                Date = entity.Date.ToString("dddd"),
                Time = entity.Date.ToString("HH:mm")
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
                Rank = entity.Rank
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
                Rank = dto.Rank
            };
        }
    }
}
