using System;
using System.ComponentModel.DataAnnotations;

namespace Leaguelane.Persistence.Entities
{
    public class Bet : Entity
    {
        [Key]
        public int Id { get; set; }
        public int BetId { get; set; }
        public string? Name { get; set; }
    }
}
