using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Leaguelane.Persistence.Entities
{
    public enum TeamStatsResultType { played, wins, draws, losses }

    public class TeamStatFixture
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TeamStatsId { get; set; } // FK to TeamStats(Id)
        [Required]
        public TeamStatsResultType ResultType { get; set; }
        public int Home { get; set; }
        public int Away { get; set; }
        public int Total { get; set; }
    }
}
