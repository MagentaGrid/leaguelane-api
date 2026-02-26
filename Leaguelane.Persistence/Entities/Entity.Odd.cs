using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Leaguelane.Persistence.Entities
{
    public class Odd : Entity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int FixtureId { get; set; }
        [Required]
        public int LeagueId { get; set; }
        [Required]
        public int SeasonId { get; set; }
        [Required]
        public int SportId { get; set; }
        [Required]
        public int BookmakerId { get; set; }
        [Required]
        public int BetTypeId { get; set; }
        [Required]
        public DateTime LastUpdated { get; set; }

        public ICollection<OddsValue> OddsValues { get; set; }
    }
}
