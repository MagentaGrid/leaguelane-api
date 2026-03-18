using Leaguelane.Models.Dtos;

namespace Leaguelane.ApiService.Mappers
{
    public static class TeamMapper
    {
        public static TeamResponseDto MapToDto(Persistence.Entities.Team team)
        {
            return new TeamResponseDto
            {
                Id = team.Id,
                Name = team.Name,
                ApiTeamId = team.ApiTeamId,
                LogoUrl = team.LogoUrl,
                DisplayName = team.DisplayName,
                Code = team.Code,
                Country = team.Country,
                Active = team.Active,
                Founded = team.Founded,
                LeagueId = team.LeagueId,
                National = team.National,
                SeasonId = team.SeasonId,
            };
        }
    }
}
