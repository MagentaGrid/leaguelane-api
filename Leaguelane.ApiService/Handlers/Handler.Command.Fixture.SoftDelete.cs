using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.ApiService.Handlers
{
    public record SoftDeleteFixtureCommand(int Id) : IRequest<bool>;
    public class SoftDeleteFixtureCommandHandler : IRequestHandler<SoftDeleteFixtureCommand, bool>
    {
        private readonly IFixtureService _fixtureService;
        public SoftDeleteFixtureCommandHandler(IFixtureService fixtureService)
        {
            _fixtureService = fixtureService;
        }
        public async Task<bool> Handle(SoftDeleteFixtureCommand request, CancellationToken cancellationToken)
        {
            await _fixtureService.SoftDeleteFixtureAsync(request.Id, cancellationToken);
            return true;
        }
    }
}
