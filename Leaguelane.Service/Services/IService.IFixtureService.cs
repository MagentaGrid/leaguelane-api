using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public interface IFixtureService
    {
        Task GetAllFixtures(CancellationToken cancellationToken);
        Task GetAllFixturesByLeagueAndSeason(int leagueId, int season, CancellationToken cancellationToken);
    }
}
