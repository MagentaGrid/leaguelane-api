using Leaguelane.Persistence.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public interface ITeamRepository
    {
        Task AddOrUpdateTeamsAsync(List<Team> teams, CancellationToken cancellationToken);
        Task<Dictionary<int, Team>> GetAllTeamsById(IEnumerable<int> teamIds, CancellationToken cancellationToken);
    }
}
