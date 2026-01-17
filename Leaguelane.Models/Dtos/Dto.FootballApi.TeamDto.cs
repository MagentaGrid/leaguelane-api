using System.Text.Json.Serialization;

namespace Leaguelane.Models.Dtos
{
    public class TeamApiResponseDto
    {
        [JsonPropertyName("team")]
        public TeamApiDto Team { get; set; }
        [JsonPropertyName("venue")]
        public VenueApiDto Venue { get; set; }
    }

    public class TeamApiDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("country")]
        public string Country { get; set; }
        [JsonPropertyName("founded")]
        public int? Founded { get; set; }
        [JsonPropertyName("national")]
        public bool? National { get; set; }
        [JsonPropertyName("logo")]
        public string Logo { get; set; }
    }

    public class VenueApiDto
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("address")]
        public string? Address { get; set; }
        [JsonPropertyName("city")]
        public string? City { get; set; }
        [JsonPropertyName("capacity")]
        public int? Capacity { get; set; }
        [JsonPropertyName("surface")]
        public string? Surface { get; set; }
        [JsonPropertyName("image")]
        public string? Image { get; set; }
    }
}
