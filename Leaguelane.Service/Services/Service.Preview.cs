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
            if(await _repository.ExistsAsync<FixturePreview>(x => x.FixtureId == fixturePreview.FixtureId, cancellationToken))
            {
                var preview = await _repository.FirstOrDefaultAsync<FixturePreview>(x => x.FixtureId == fixturePreview.FixtureId, cancellationToken);

                preview.FullAnalysis = fixturePreview.FullAnalysis;
                preview.ShortIntro = fixturePreview.ShortIntro;
                preview.Headline = fixturePreview.Headline;

                _repository.Update(preview);
                await _repository.SaveChangesAsync<FixturePreview>(cancellationToken);
                return preview;
            }

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
