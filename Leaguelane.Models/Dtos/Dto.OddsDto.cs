using System;
using System.Collections.Generic;

namespace Leaguelane.Models.Dtos
{
    public class OddsDto
    {
        public int Id { get; set; }
        public int FixtureId { get; set; }
        public int LeagueId { get; set; }
        public int SeasonId { get; set; }
        public int SportId { get; set; }
        public int BookmakerId { get; set; }
        public int BetTypeId { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Active { get; set; }
        public List<OddsValueDto> Values { get; set; }
    }
}
