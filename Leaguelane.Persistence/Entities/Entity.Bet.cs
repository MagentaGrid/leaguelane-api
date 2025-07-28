using System;
using System.ComponentModel.DataAnnotations;

namespace Leaguelane.Persistence.Entities
{
    public class Bet : Entity
    {
        [Key]
        public int BetId { get; set; }
        public int LeagueId { get; set; }
        public int Season { get; set; }
        public int FixtureId { get; set; }
        public DateTime FixtureDate { get; set; }
        public long FixtureTimestamp { get; set; }
        public DateTime Update { get; set; }
    }
}
