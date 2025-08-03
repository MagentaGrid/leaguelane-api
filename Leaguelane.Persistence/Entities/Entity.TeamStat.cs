using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Leaguelane.Persistence.Entities
{
    public class TeamStat : Entity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TeamId { get; set; }
        [Required]
        public int LeagueId { get; set; }
        [Required]
        public int SeasonId { get; set; }
        [Required]
        public int SportId { get; set; }
    }
}
