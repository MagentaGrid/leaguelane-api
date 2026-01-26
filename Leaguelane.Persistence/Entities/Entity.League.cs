using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Persistence.Entities
{
    [Index(nameof(ApiLeagueId), IsUnique = true)]
    public class League: Entity
    {
        [Key]
        public int LeagueId { get; set; }
        public int ApiLeagueId { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? LogoUrl { get; set; }
        public int? CountryId { get; set; }
        public string? CountryCode { get; set; }
        public int? SportId { get; set; }
        public int CurrentSeason { get; set; }

        [ForeignKey("CountryId")]
        public Country? Country { get; set; }

        [ForeignKey("SportId")]
        public Sport? Sport { get; set; }
    }
}
