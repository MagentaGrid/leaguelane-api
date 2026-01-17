using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Leaguelane.Persistence.Entities
{
    public class Round : Entity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } // e.g., "Regular Season - 1"

        [Required]
        public int LeagueId { get; set; } // FK to Leagues(Id)
        [Required]
        public int SeasonId { get; set; } // FK to Seasons(Id)
        [Required]
        public int SportId { get; set; } // FK to Sports(SportId)
    }
}
