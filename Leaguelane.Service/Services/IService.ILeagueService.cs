using Leaguelane.Models.Dtos;
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
        Task<League> GetLeagueByApiIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> UpdateLeagueAsync(UpdateLeagueRequestDto leagueDto, CancellationToken cancellationToken);
        Task<bool> DisableLeagueAsync(int id, CancellationToken cancellationToken);
        Task<(int totalCount, List<League>)> GetAllLeagues(int page, int pageSize, string? search, string status, CancellationToken cancellationToken);
        Task<bool> EnableLeagueAsync(int id, CancellationToken cancellationToken);
    }
}
