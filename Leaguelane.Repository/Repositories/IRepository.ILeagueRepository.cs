using Leaguelane.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public interface ILeagueRepository
    {
        Task<bool> AddLeagues(List<League> leagues, CancellationToken cancellationToken);
        Task<List<League>> GetAllActiveLeagues(CancellationToken cancellationToken);
    }
}
