using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Leaguelane.Persistence.Entities
{
    [Index(nameof(ApiVenueId), IsUnique = true)]
    public class Venue : Entity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ApiVenueId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string? Address { get; set; }
        [MaxLength(100)]
        public string? City { get; set; }
        public int? Capacity { get; set; }
        [MaxLength(50)]
        public string? Surface { get; set; }
        [MaxLength(255)]
        public string? ImageUrl { get; set; }
        [Required]
        public int SportId { get; set; }
        [Required]
        public int LeagueId { get; set; }
        [Required]
        public int SeasonId { get; set; }
        [Required]
        public int TeamId { get; set; }
    }
}
