using System.Collections.Generic;

namespace Leaguelane.Models.Dtos
{
    public class SetRankRequestDto
    {
        public int Rank { get; set; }
    }

    public class FixtureTeamDto
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
    }

    public class FixtureListItemDto
    {
        public int FixtureId { get; set; }
        public FixtureTeamDto HomeTeam { get; set; }
        public FixtureTeamDto AwayTeam { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }

    public class GetLatestFixtureResponseDto
    {
        public List<FixtureListItemDto> Fixtures { get; set; }
    }

    public class LeagueFixtureListDto
    {
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
        public string Logo { get; set; }
        public List<FixtureListItemDto> Fixtures { get; set; }
    }
}
