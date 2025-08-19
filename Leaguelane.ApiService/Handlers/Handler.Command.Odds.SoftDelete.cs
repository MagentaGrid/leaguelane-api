using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.ApiService.Handlers
{
    public record SoftDeleteOddsCommand(int Id) : IRequest<bool>;
    public class SoftDeleteOddsCommandHandler : IRequestHandler<SoftDeleteOddsCommand, bool>
    {
        private readonly IOddsService _oddsService;
        public SoftDeleteOddsCommandHandler(IOddsService oddsService)
        {
            _oddsService = oddsService;
        }
        public async Task<bool> Handle(SoftDeleteOddsCommand request, CancellationToken cancellationToken)
        {
            await _oddsService.SoftDeleteOddsAsync(request.Id, cancellationToken);
            return true;
        }
    }
}
