using System;

namespace Leaguelane.Models.Dtos
{
    public class FixtureDto
    {
        public int FixtureId { get; set; }
        public string? Timezone { get; set; }
        public DateTime? Date { get; set; }
        public long? Time { get; set; }
        public int? VenueId { get; set; }
        public int LeagueId { get; set; }
        public int SeasonId { get; set; }
        public int? SportId { get; set; }
        public int? RoundId { get; set; }
        public string? Status { get; set; }
        public int? HomeTeamId { get; set; }
        public int? AwayTeamId { get; set; }
        public int? GoalsHome { get; set; }
        public int? GoalsAway { get; set; }
        public int? Rank { get; set; }
    }

    public class FixtureApiResponseDto
    {
        public int FixtureId { get; set; }

        public string? Timezone { get; set; }

        public DateTime? Date { get; set; }

        public long? Time { get; set; }

        public int? VenueId { get; set; }

        public string? VenueName { get; set; }

        public int LeagueId { get; set; }

        public string? LeagueName { get; set; }

        public int SeasonId { get; set; }

        public int? SportId { get; set; }

        public int? RoundId { get; set; }

        public string? Status { get; set; }

        public int? HomeTeamId { get; set; }

        public string? HomeTeamName { get; set; }

        public int? AwayTeamId { get; set; }

        public string? AwayTeamName { get; set; }

        public int? GoalsHome { get; set; }

        public int? GoalsAway { get; set; }

        public int? Rank { get; set; } // For ranking fixtures
        public int ApiFixtureId { get; set; }
        public bool PublishStatus { get; set; }
    }

    public class FixtureDetailsApiResponseDto: FixtureApiResponseDto
    {
        public List<TipsResponse>? Tips { get; set; } = new List<TipsResponse>(); // For ranking fixtures>
        public PreviewResponse? Preview { get; set; } = new PreviewResponse();
        public List<StatsResponse>? HomeTeamStats { get; set; } = new List<StatsResponse>();
        public List<StatsResponse>? AwayTeamStats { get; set; } = new List<StatsResponse>();
        public List<H2HResponse>? H2H { get; set; } = new List<H2HResponse>();
    }

    public class TipsResponse
    {
        public int FixtureTipId { get; set; }
        public int FixtureId { get; set; }
        public string Title { get; set; }
        public string? Reasoning { get; set; }
        public int BookmakerId { get; set; }
        public string BookmakerName { get; set; }
        public int OddsValueId { get; set; }
        public string OddsValueName { get; set; }
        public int BetId { get; set; }
        public string BetName { get; set; }
        public bool IsSaved { get; set; } = true;
        public bool IsVisible { get; set; } = false;
    }

    public class PreviewResponse
    {
        public int FixturePreviewId { get; set; }
        public int FixtureId { get; set; }
        public string Headline { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string ShortIntro { get; set; } = string.Empty;
        public string FullAnalysis { get; set; } = string.Empty;
    }

    public class OddsResponse
    {

    }

    public class StatsResponse
    {

    }

    public class H2HResponse
    {

    }

    public class PredictionTeam
    {
        public string Team { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
    }

    public class Prediction
    {
        public PredictionTeam? Home { get; set; }
        public PredictionTeam? Away { get; set; }
        public string Time { get; set; } = string.Empty;
        public string Day { get; set; } = string.Empty;
    }

    // Detailed DTOs for grouped predictions
    public class FormItem
    {
        public string Code { get; set; } = string.Empty;
        public string ColorHex { get; set; } = string.Empty;
    }

    public class TeamSummary
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
        public List<FormItem>? Form { get; set; }
    }

    public class MatchSummary
    {
        public string FixtureId { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string Day { get; set; } = string.Empty;
        public string Venue { get; set; } = string.Empty;
        public string Broadcaster { get; set; } = string.Empty;
        public TeamSummary? HomeTeam { get; set; }
        public TeamSummary? AwayTeam { get; set; }
    }

    public class LeaguePrediction
    {
        public string LeagueId { get; set; } = string.Empty;
        public string LeagueKey { get; set; } = string.Empty;
        public string LeagueName { get; set; } = string.Empty;
        public string? LeagueLogoUrl { get; set; }
        public string MatchdayLabel { get; set; } = string.Empty;
        public string TableUrl { get; set; } = string.Empty;
        public List<MatchSummary>? Matches { get; set; }
    }

    public class PredictionsData
    {
        public string League { get; set; } = string.Empty;
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalLeagues { get; set; }
        public int TotalMatches { get; set; }
        public List<int>? LeagueFilter { get; set; } = new List<int>();
        public List<LeaguePrediction>? Leagues { get; set; }
    }
}
