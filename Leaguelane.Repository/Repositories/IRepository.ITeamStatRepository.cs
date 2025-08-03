using Leaguelane.Persistence.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public interface ITeamStatRepository
    {
        Task<int> AddOrUpdateTeamStatAsync(TeamStat stat, CancellationToken cancellationToken);
        Task AddOrUpdateTeamStatFixturesAsync(List<TeamStatFixture> fixtures, CancellationToken cancellationToken);
        Task AddOrUpdateTeamStatGoalsAsync(List<TeamStatGoal> goals, CancellationToken cancellationToken);
    }
}
