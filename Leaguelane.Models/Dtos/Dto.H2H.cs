using System.Text.Json.Serialization;

namespace Leaguelane.Models.Dtos
{
    public class H2HDto
    {
    }

    public class H2HFixtureResponse
    {
        [JsonPropertyName("fixture")]
        public H2HFixture? Fixture { get; set; }

        [JsonPropertyName("league")]
        public H2HLeague? League { get; set; }

        [JsonPropertyName("teams")]
        public H2HTeams? Teams { get; set; }

        [JsonPropertyName("goals")]
        public H2HGoals? Goals { get; set; }

        [JsonPropertyName("score")]
        public H2HScore? Score { get; set; }
    }

    public class H2HFixture
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("referee")]
        public string? Referee { get; set; }

        [JsonPropertyName("timezone")]
        public string? Timezone { get; set; }

        [JsonPropertyName("date")]
        public DateTime? Date { get; set; }

        [JsonPropertyName("timestamp")]
        public long? Timestamp { get; set; }

        [JsonPropertyName("periods")]
        public H2HPeriods? Periods { get; set; }

        [JsonPropertyName("venue")]
        public H2HVenue? Venue { get; set; }

        [JsonPropertyName("status")]
        public H2HFixtureStatus? Status { get; set; }
    }

    public class H2HPeriods
    {
        [JsonPropertyName("first")]
        public long? First { get; set; }

        [JsonPropertyName("second")]
        public long? Second { get; set; }
    }

    public class H2HVenue
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }
    }

    public class H2HFixtureStatus
    {
        [JsonPropertyName("long")]
        public string? Long { get; set; }

        [JsonPropertyName("short")]
        public string? Short { get; set; }

        [JsonPropertyName("elapsed")]
        public int? Elapsed { get; set; }

        [JsonPropertyName("extra")]
        public int? Extra { get; set; }
    }

    public class H2HLeague
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("logo")]
        public string? Logo { get; set; }

        [JsonPropertyName("flag")]
        public string? Flag { get; set; }

        [JsonPropertyName("season")]
        public int? Season { get; set; }

        [JsonPropertyName("round")]
        public string? Round { get; set; }

        [JsonPropertyName("standings")]
        public bool? Standings { get; set; }
    }

    public class H2HTeams
    {
        [JsonPropertyName("home")]
        public H2HTeam? Home { get; set; }

        [JsonPropertyName("away")]
        public H2HTeam? Away { get; set; }
    }

    public class H2HTeam
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("logo")]
        public string? Logo { get; set; }

        [JsonPropertyName("winner")]
        public bool? Winner { get; set; }
    }

    public class H2HGoals
    {
        [JsonPropertyName("home")]
        public int? Home { get; set; }

        [JsonPropertyName("away")]
        public int? Away { get; set; }
    }

    public class H2HScore
    {
        [JsonPropertyName("halftime")]
        public H2HGoals? Halftime { get; set; }

        [JsonPropertyName("fulltime")]
        public H2HGoals? Fulltime { get; set; }

        [JsonPropertyName("extratime")]
        public H2HGoals? Extratime { get; set; }

        [JsonPropertyName("penalty")]
        public H2HGoals? Penalty { get; set; }
    }
}