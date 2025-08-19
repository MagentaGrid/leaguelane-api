using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public interface IOddsService
    {
        Task FetchAndStoreOddsAsync(Persistence.Entities.Fixture fixture, CancellationToken cancellationToken);
        Task<List<OddsDto>> GetOddsAsync(int? fixtureId, int? bookmakerId, string? market, int skip, int take, bool onlyActive, CancellationToken cancellationToken);
        Task<List<OddsDto>> GetDeletedOddsAsync(CancellationToken cancellationToken);
        Task<List<OddsDto>> GetAllOddsAsync(CancellationToken cancellationToken);
        Task<OddsDto> GetOddsByIdAsync(int id, bool onlyActive, CancellationToken cancellationToken);
        Task UpdateOddsAsync(Odd odd, List<OddsValue> values, CancellationToken cancellationToken);
        Task SoftDeleteOddsAsync(int id, CancellationToken cancellationToken);
        Task RestoreOddsAsync(int id, CancellationToken cancellationToken);
        Task FetchOddsAsync(CancellationToken cancellationToken);
    }
}
