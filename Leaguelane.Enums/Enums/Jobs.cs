using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Enums.Enums
{
    public enum Jobs
    {
        Season = 1,
        League = 2,
        Country = 3,
        Fixture = 4,
        Round = 5,
        Bookmaker = 6,
        Bet = 7
        Round = 5, // Added Round for RoundsScheduler
        Team = 6, // Added Team for TeamsScheduler
        TeamStat = 7 // Added TeamStat for TeamStatsScheduler
    }
}
