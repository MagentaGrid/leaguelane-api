using Leaguelane.Persistence.Entities;

namespace Leaguelane.Service.Services
{
    public interface IPreviewService
    {
        Task<FixturePreview> AddPreviewAsync(FixturePreview fixturePreview, CancellationToken cancellationToken);
        Task<List<FixturePreview>> GetAllPreviewByFixtureId(int fixtureId, CancellationToken cancellationToken);
    }
}
