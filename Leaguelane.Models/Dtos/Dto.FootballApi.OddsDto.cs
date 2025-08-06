using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Leaguelane.Models.Dtos
{
    public class OddsApiResponseDto
    {
        [JsonPropertyName("fixture")]
        public int Fixture { get; set; }
        [JsonPropertyName("bookmakers")]
        public List<OddsBookmakerDto> Bookmakers { get; set; }
    }

    public class OddsBookmakerDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("bets")]
        public List<OddsBetDto> Bets { get; set; }
    }

    public class OddsBetDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("values")]
        public List<OddsValueDto> Values { get; set; }
    }

    public class OddsValueDto
    {
        [JsonPropertyName("value")]
        public string Label { get; set; }
        [JsonPropertyName("odd")]
        public decimal Odd { get; set; }
    }
}
