using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Leaguelane.Models.Dtos
{
    public class OddsApiResponseDto
    {
        //[JsonPropertyName("fixture")]
        //public int Fixture { get; set; }
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
        [JsonConverter(typeof(StringOrNumberConverter))]
        public string Label { get; set; }
        [JsonPropertyName("odd")]
        public string Odd { get; set; }
    }

    public class StringOrNumberConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.TokenType switch
            {
                JsonTokenType.String => reader.GetString()!,

                JsonTokenType.Number => reader.TryGetInt64(out var l)
                    ? l.ToString()
                    : reader.GetDouble().ToString(),

                JsonTokenType.True => "true",
                JsonTokenType.False => "false",

                _ => throw new JsonException($"Unsupported token type: {reader.TokenType}")
            };
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }
}
