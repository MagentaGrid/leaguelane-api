using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Models.Dtos
{
    public class FixtureResponseDto
    {
        public FixtureInfo Fixture { get; set; }
        public LeagueInfo League { get; set; }
        public TeamsInfo Teams { get; set; }
        public GoalsInfo Goals { get; set; }
        public ScoreInfo Score { get; set; }
    }

    public class FixtureInfo
    {
        public int Id { get; set; }
        public string Referee { get; set; }
        public string Timezone { get; set; }
        public DateTime Date { get; set; }
        public long Timestamp { get; set; }
        public Periods Periods { get; set; }
        public Venue Venue { get; set; }
        public Status Status { get; set; }
    }

    public class Periods
    {
        public long First { get; set; }
        public long Second { get; set; }
    }

    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
    }

    public class Status
    {
        public string Long { get; set; }
        public string Short { get; set; }
        public int Elapsed { get; set; }
        public int Extra { get; set; }
    }

    public class LeagueInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Logo { get; set; }
        public string Flag { get; set; }
        public int Season { get; set; }
        public string Round { get; set; }
        public bool Standings { get; set; }
    }

    public class TeamsInfo
    {
        public Team Home { get; set; }
        public Team Away { get; set; }
    }

    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public bool? Winner { get; set; }
    }

    public class GoalsInfo
    {
        public int Home { get; set; }
        public int Away { get; set; }
    }

    public class ScoreInfo
    {
        public ScoreDetail Halftime { get; set; }
        public ScoreDetail Fulltime { get; set; }
        public ScoreDetail Extratime { get; set; }
        public ScoreDetail Penalty { get; set; }
    }

    public class ScoreDetail
    {
        public int? Home { get; set; }
        public int? Away { get; set; }
    }
}
