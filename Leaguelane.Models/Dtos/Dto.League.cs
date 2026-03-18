namespace Leaguelane.Models.Dtos
{
    public class LeaguesDto
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? LogoUrl { get; set; }
        public int? CountryId { get; set; }
        public string? CountryCode { get; set; }
        public int? SportId { get; set; }
        public int CurrentSeason { get; set; }
        public int? Rank { get; set; }
        public bool? Active { get; set; }
        public string? DisplayName { get; set; }
    }

    public class UpdateLeagueRequestDto: LeaguesDto
    {
        public int LeagueId { get; set; }
    }

    public class LeaguesResponseDto : LeaguesDto
    {
        public int LeagueId { get; set; }
        public int ApiLeagueId { get; set; }
    }
}
