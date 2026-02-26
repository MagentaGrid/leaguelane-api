using Leaguelane.Persistence.Entities;

namespace Leaguelane.Service.Services
{
    public interface ITipService
    {
        Task<FixtureTip> AddTipAsync(FixtureTip fixtureTip, CancellationToken cancellationToken);
        Task<List<FixtureTip>> GetAllTipsByFixtureIdAsync(int fixtureId, CancellationToken cancellationToken);
    }
}
