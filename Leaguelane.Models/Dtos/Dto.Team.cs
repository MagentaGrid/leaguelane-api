using System.ComponentModel.DataAnnotations;

namespace Leaguelane.Models.Dtos
{
    public class TeamDto
    {
        public string Name { get; set; }
        public string? Code { get; set; }
        public string? Country { get; set; }
        public int? Founded { get; set; }
        public bool? National { get; set; }
        public string? LogoUrl { get; set; }
        public int SportId { get; set; }
        public int LeagueId { get; set; }
        public string? LeagueName { get; set; }
        public int SeasonId { get; set; }
        public string? DisplayName { get; set; }
        public bool? Active { get; set; }
    }

    public class TeamResponseDto : TeamDto
    {

        public int Id { get; set; }
        public int ApiTeamId { get; set; }
    }

    public class TeamUpdateDto: TeamDto
    {
        public int Id { get; set; }
    }
}
