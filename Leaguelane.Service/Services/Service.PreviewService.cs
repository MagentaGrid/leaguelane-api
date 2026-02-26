using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;

namespace Leaguelane.Service.Services
{
    public class PreviewService: IPreviewService
    {
        private readonly IRepository _repository;

        public PreviewService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<FixturePreview> AddPreviewAsync(FixturePreview fixturePreview, CancellationToken cancellationToken)
        {
            await _repository.AddAsync<FixturePreview>(fixturePreview,cancellationToken);
            await _repository.SaveChangesAsync<FixturePreview>(cancellationToken);
            return fixturePreview;
        }

        public async Task<List<FixturePreview>> GetAllPreviewByFixtureId(int fixtureId, CancellationToken cancellationToken)
        {
            return (await _repository.FindAllAsync<FixturePreview>(x => x.FixtureId == fixtureId, cancellationToken)).ToList();
        }
    }
}
