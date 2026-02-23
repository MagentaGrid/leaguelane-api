using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Leaguelane.Persistence.Entities;

[Index(nameof(ApiFixtureId), IsUnique = true)]
public class Fixture: Entity
{
    [Key]
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

    public int? Rank { get; set; } // For ranking fixtures
    public int ApiFixtureId { get; set; }
    public bool PublishStatus { get; set; }

    public virtual ICollection<FixturePreview>? FixturePreviews { get; set; }
    public virtual ICollection<FixtureTip>? FixtureTips { get; set; }
}
