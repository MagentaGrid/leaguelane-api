using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Leaguelane.Persistence.Entities
{
    [Index(nameof(ApiTeamId), IsUnique = true)]
    public class Team : Entity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ApiTeamId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(10)]
        public string? Code { get; set; }
        [MaxLength(100)]
        public string? Country { get; set; }
        public int? Founded { get; set; }
        public bool? National { get; set; }
        [MaxLength(255)]
        public string? LogoUrl { get; set; }
        [Required]
        public int SportId { get; set; }
        [Required]
        public int LeagueId { get; set; }
        [Required]
        public int SeasonId { get; set; }
    }
}
