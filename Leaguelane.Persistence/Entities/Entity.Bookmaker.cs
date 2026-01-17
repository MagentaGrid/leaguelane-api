using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Leaguelane.Persistence.Entities
{
    [Index(nameof(ApiBookMakerId), IsUnique = true)]
    public class Bookmaker : Entity
    {
        [Key]
        public int BookmakerId { get; set; }
        public int ApiBookMakerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? AffiliateLink { get; set; }
        public string? BookieLogo { get; set; }
        public bool? Active { get; set; }
    }
}
