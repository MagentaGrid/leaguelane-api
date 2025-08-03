using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Leaguelane.Models.Dtos
{
    public class TeamStatApiResponseDto
    {
        [JsonPropertyName("fixtures")]
        public Dictionary<string, TeamStatFixtureDto> Fixtures { get; set; }
        [JsonPropertyName("goals")]
        public TeamStatGoalsDto Goals { get; set; }
    }

    public class TeamStatFixtureDto
    {
        [JsonPropertyName("home")]
        public int Home { get; set; }
        [JsonPropertyName("away")]
        public int Away { get; set; }
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }

    public class TeamStatGoalsDto
    {
        [JsonPropertyName("for")]
        public TeamStatGoalDetailDto For { get; set; }
        [JsonPropertyName("against")]
        public TeamStatGoalDetailDto Against { get; set; }
    }

    public class TeamStatGoalDetailDto
    {
        [JsonPropertyName("total")]
        public TeamStatGoalMetricDto Total { get; set; }
        [JsonPropertyName("average")]
        public TeamStatGoalMetricDto Average { get; set; }
    }

    public class TeamStatGoalMetricDto
    {
        [JsonPropertyName("home")]
        public decimal Home { get; set; }
        [JsonPropertyName("away")]
        public decimal Away { get; set; }
        [JsonPropertyName("total")]
        public decimal Total { get; set; }
    }
}
