using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.ApiService.Handlers
{
    public record RestoreOddsCommand(int Id) : IRequest<bool>;
    public class RestoreOddsCommandHandler : IRequestHandler<RestoreOddsCommand, bool>
    {
        private readonly IOddsService _oddsService;
        public RestoreOddsCommandHandler(IOddsService oddsService)
        {
            _oddsService = oddsService;
        }
        public async Task<bool> Handle(RestoreOddsCommand request, CancellationToken cancellationToken)
        {
            await _oddsService.RestoreOddsAsync(request.Id, cancellationToken);
            return true;
        }
    }
}
