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
        public bool IsTipAdded { get; set; }
        public bool IsPreviewAdded { get; set; }
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
}
