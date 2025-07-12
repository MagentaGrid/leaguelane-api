using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Leaguelane.Persistence.Entities
{
    public class LeagueSeason
    {
        [Key]
        public int LeagueSeasonId { get; set; }
        public int LeagueId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrent { get; set; }

        [ForeignKey("LeagueId")]
        public League? League { get; set; }
        public int SeasonId { get; set; }
        [ForeignKey("SeasonId")]
        public Season? Season { get; set; }
    }
}
