using Leaguelane.Models.Dtos;
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

        public async Task<bool> DeleteTipAsync(int fixtureTipId, CancellationToken cancellationToken)
        {
            var fixtureTip = await _repository.GetByIdAsync<FixtureTip>(fixtureTipId, cancellationToken);
            if (fixtureTip == null) throw new Exception("Tip not found");

            fixtureTip.Active = false;
            await _repository.UpdateAsync(fixtureTip);
            await _repository.SaveChangesAsync<FixtureTip>(cancellationToken);
            return true;
        }

        public async Task<bool> UpdateTipAsync(TipUpdateRequestDto fixtureTip, CancellationToken cancellationToken)
        {
            var tip = await _repository.GetByIdAsync<FixtureTip>(fixtureTip.FixtureTipId, cancellationToken);
            if (tip == null) throw new Exception("Tip not found");
           
            tip.OddsValueId = fixtureTip.OddsId;
            tip.BetId = fixtureTip.BetId;
            tip.BookmakerId = fixtureTip.BookmakerId;
            tip.Title = fixtureTip.Title;
            tip.Reasoning = fixtureTip.Reasoning;

            _repository.Update(tip);
            await _repository.SaveChangesAsync<FixtureTip>(cancellationToken);
            return true;
        }
    }
}
