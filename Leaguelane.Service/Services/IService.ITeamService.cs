using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public interface ITeamService
    {
        Task FetchAndStoreTeamsAndVenuesAsync(int leagueId, int seasonId, int sportId, CancellationToken cancellationToken);
        Task ImportAllTeams(CancellationToken cancellationToken);
        Task<Dictionary<int, Persistence.Entities.Team>> GetAllTeamsById(IEnumerable<int> teamIds, CancellationToken cancellationToken);
        Task<List<Persistence.Entities.Team>> GetAllTeamsByIds(List<int> teamIds, CancellationToken cancellationToken);
        Task<(int totalCount, List<Persistence.Entities.Team>)> GetAllTeams(int page, int pageSize, string searchText, string status, CancellationToken cancellationToken);
        Task<Persistence.Entities.Team> GetTeamByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> EnableTeamAsync(int id, CancellationToken cancellationToken);
        Task<bool> DisableTeamAsync(int id, CancellationToken cancellationToken);
        Task<bool> UpdateTeamAsync(TeamUpdateDto teamUpdate, CancellationToken cancellationToken);
    }
}
