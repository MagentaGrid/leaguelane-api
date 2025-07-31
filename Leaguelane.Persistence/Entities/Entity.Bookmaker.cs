using System.ComponentModel.DataAnnotations;

namespace Leaguelane.Persistence.Entities
{
    public class Bookmaker : Entity
    {
        [Key]
        public int BookmakerId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
