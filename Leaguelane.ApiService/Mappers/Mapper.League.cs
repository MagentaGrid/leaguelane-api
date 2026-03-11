namespace Leaguelane.ApiService.Mappers
{
    public static class LeagueMapper
    {
        public static Models.Dtos.LeaguesResponseDto MapToDto(Persistence.Entities.League league)
        {
            return new Models.Dtos.LeaguesResponseDto
            {
                LeagueId = league.LeagueId,
                Name = league.Name,
                ApiLeagueId = league.ApiLeagueId,
                Rank = league.Rank,
                CountryCode = league.CountryCode,
                CurrentSeason = league.CurrentSeason,
                LogoUrl = league.LogoUrl,
                Type = league.Type,
                Active = league.Active
            };
        }
    }
}
