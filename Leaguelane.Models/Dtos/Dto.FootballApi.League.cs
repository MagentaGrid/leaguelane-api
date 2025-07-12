using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Leaguelane.Models.Dtos
{
    public class LeagueResponseDto
    {
        [JsonPropertyName("league")]
        public LeagueDto League { get; set; }

        [JsonPropertyName("country")]
        public CountryDto Country { get; set; }

        [JsonPropertyName("seasons")]
        public List<SeasonDto> Seasons { get; set; }
    }

    public class LeagueDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("logo")]
        public string Logo { get; set; }
    }

    public class SeasonDto
    {
        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("start")]
        public string Start { get; set; }

        [JsonPropertyName("end")]
        public string End { get; set; }

        [JsonPropertyName("current")]
        public bool Current { get; set; }

        [JsonPropertyName("coverage")]
        public CoverageDto Coverage { get; set; }
    }

    public class CoverageDto
    {
        [JsonPropertyName("fixtures")]
        public FixturesDto Fixtures { get; set; }

        [JsonPropertyName("standings")]
        public bool Standings { get; set; }

        [JsonPropertyName("players")]
        public bool Players { get; set; }

        [JsonPropertyName("top_scorers")]
        public bool TopScorers { get; set; }

        [JsonPropertyName("top_assists")]
        public bool TopAssists { get; set; }

        [JsonPropertyName("top_cards")]
        public bool TopCards { get; set; }

        [JsonPropertyName("injuries")]
        public bool Injuries { get; set; }

        [JsonPropertyName("predictions")]
        public bool Predictions { get; set; }

        [JsonPropertyName("odds")]
        public bool Odds { get; set; }
    }

    public class FixturesDto
    {
        [JsonPropertyName("events")]
        public bool Events { get; set; }

        [JsonPropertyName("lineups")]
        public bool Lineups { get; set; }

        [JsonPropertyName("statistics_fixtures")]
        public bool StatisticsFixtures { get; set; }

        [JsonPropertyName("statistics_players")]
        public bool StatisticsPlayers { get; set; }
    }

}
