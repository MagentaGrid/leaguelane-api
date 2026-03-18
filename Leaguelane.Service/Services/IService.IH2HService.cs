using Leaguelane.Models.Dtos;

namespace Leaguelane.Service.Services
{
    public interface IH2HService
    {
        Task<List<H2HFixtureResponse>> GetH2H(int homeTeamId, int awayTeamId, CancellationToken cancellationToken);
    }
}
