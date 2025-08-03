using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Leaguelane.Persistence.Entities
{
    public enum TeamStatsGoalType { @for, against }
    public enum TeamStatsGoalMetric { total, average }

    public class TeamStatGoal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TeamStatsId { get; set; } // FK to TeamStats(Id)
        [Required]
        public TeamStatsGoalType Type { get; set; } // for or against
        [Required]
        public TeamStatsGoalMetric Metric { get; set; } // total or average
        public decimal? Home { get; set; }
        public decimal? Away { get; set; }
        public decimal? Total { get; set; }
    }
}
