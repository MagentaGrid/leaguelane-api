using Leaguelane.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public interface ILeagueService
    {
        Task<bool> GetAllLeaguesAsync(CancellationToken cancellationToken);
        Task<List<League>> GetAllActiveLeaguesByIds(List<int> ids, CancellationToken cancellationToken);
    }
}
