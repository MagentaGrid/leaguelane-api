using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Leaguelane.Persistence.Entities
{
    public class OddsValue : Entity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OddsId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Label { get; set; }
        [Required]
        public decimal Odd { get; set; }
    }
}
