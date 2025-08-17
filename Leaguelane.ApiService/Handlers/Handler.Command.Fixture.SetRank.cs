using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.ApiService.Handlers
{

    public record SetRankCommand(int FixtureId, int Rank) : IRequest<bool>;
    public class SetRankCommandHandler : IRequestHandler<SetRankCommand, bool>
    {
        private readonly IFixtureService _fixtureService;
        public SetRankCommandHandler(IFixtureService fixtureService)
        {
            _fixtureService = fixtureService;
        }
        public async Task<bool> Handle(SetRankCommand request, CancellationToken cancellationToken)
        {
            await _fixtureService.SetRankAsync(request.FixtureId, request.Rank, cancellationToken);
            return true;
        }
    }
}
