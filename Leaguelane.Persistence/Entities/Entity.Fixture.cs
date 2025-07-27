using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Persistence.Entities
{
    public class Fixture: Entity
    {
        [Key]
        public int FixtureId { get; set; }  

        public string Timezone { get; set; }

        public DateTime Date { get; set; }

        public long Time { get; set; }

        public int VenueId { get; set; }

        public int LeagueId { get; set; }

        public int SeasonId { get; set; }

        public int? SportId { get; set; }

        public int? RoundId { get; set; }

        public string? Status { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public int? GoalsHome { get; set; }

        public int? GoalsAway { get; set; }
    }
}
