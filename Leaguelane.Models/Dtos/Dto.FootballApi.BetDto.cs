using System;
using System.Text.Json.Serialization;

namespace Leaguelane.Models.Dtos
{
    public class BetDto
    {
        [JsonPropertyName("league")]
        public BetLeagueDto League { get; set; }
        [JsonPropertyName("fixture")]
        public BetFixtureDto Fixture { get; set; }
        [JsonPropertyName("update")]
        public DateTime Update { get; set; }
    }

    public class BetLeagueDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("season")]
        public int Season { get; set; }
    }

    public class BetFixtureDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }
    }
}
