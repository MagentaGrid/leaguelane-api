using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Leaguelane.Persistence.Entities
{
    public class FixtureTip : Entity
    {
        [Key]
        public int FixtureTipId { get; set; }
        public int FixtureId { get; set; }
        public string Title { get; set; }
        public string? Reasoning { get; set; }
        public int BookmakerId { get; set; }
        public int OddsId { get; set; }
        public int BetId { get; set; }
        public bool IsSaved { get; set; } = true;
        public bool IsVisible { get; set; } = false;

        [ForeignKey("FixtureId")]
        public virtual Fixture Fixture { get; set; }

        [ForeignKey("BookmakerId")]
        public virtual Bookmaker Bookmaker { get; set; }

        [ForeignKey("OddsId")]
        public virtual Odd Odds { get; set; }

        [ForeignKey("BetId")]
        public virtual Bet Bet { get; set; }
    }
}
