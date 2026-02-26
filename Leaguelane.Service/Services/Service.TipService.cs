using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;

namespace Leaguelane.Service.Services
{
    public class TipService: ITipService
    {
        private IRepository _repository;

        public TipService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<FixtureTip> AddTipAsync(FixtureTip fixtureTip, CancellationToken cancellationToken)
        {
            await _repository.AddAsync<FixtureTip>(fixtureTip, cancellationToken);
            await _repository.SaveChangesAsync<FixtureTip>(cancellationToken);

            return fixtureTip;
        }

        public async Task<List<FixtureTip>> GetAllTipsByFixtureIdAsync(int fixtureId, CancellationToken cancellationToken)
        {
            return (await _repository.FindAllAsync<FixtureTip>(x => x.FixtureId == fixtureId, cancellationToken)).ToList();
        }
    }
}
