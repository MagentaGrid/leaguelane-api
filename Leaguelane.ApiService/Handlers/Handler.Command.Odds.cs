using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using Leaguelane.Service.Services;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.ApiService.Handlers
{
    // Command Handlers
    public record UpdateOddsCommand(int Id, OddsDto Odds) : IRequest<bool>;
    public class UpdateOddsCommandHandler : IRequestHandler<UpdateOddsCommand, bool>
    {
        private readonly IOddsService _oddsService;
        public UpdateOddsCommandHandler(IOddsService oddsService)
        {
            _oddsService = oddsService;
        }
        public async Task<bool> Handle(UpdateOddsCommand request, CancellationToken cancellationToken)
        {
            // Inline mapping logic from OddsDto to Odd and OddsValue
            var entity = new Odd
            {
                Id = request.Id,
                FixtureId = request.Odds.FixtureId,
                LeagueId = request.Odds.LeagueId,
                SeasonId = request.Odds.SeasonId,
                SportId = request.Odds.SportId,
                BookmakerId = request.Odds.BookmakerId,
                BetTypeId = request.Odds.BetTypeId,
                LastUpdated = request.Odds.LastUpdated,
                Active = request.Odds.Active
            };
            var valueEntities = request.Odds.Values?.Select(v => new OddsValue
            {
                OddsId = request.Id,
                Label = v.Label,
                Odd = v.Odd,
                Active = true
            }).ToList() ?? new List<OddsValue>();
            await _oddsService.UpdateOddsAsync(entity, valueEntities, cancellationToken);
            return true;
        }
    }
}
