using Leaguelane.ApiService.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using Leaguelane.Service.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.ApiService.Handlers
{
    // Command Handlers
    public record UpdateFixtureCommand(int Id, FixtureDto Fixture) : IRequest<bool>;
    public class UpdateFixtureCommandHandler : IRequestHandler<UpdateFixtureCommand, bool>
    {
        private readonly IFixtureService _fixtureService;
        public UpdateFixtureCommandHandler(IFixtureService fixtureService)
        {
            _fixtureService = fixtureService;
        }
        public async Task<bool> Handle(UpdateFixtureCommand request, CancellationToken cancellationToken)
        {
            var fixture = FixtureMapper.MapToEntity(request.Fixture);
            fixture.FixtureId = request.Id;
            await _fixtureService.UpdateFixtureAsync(fixture, cancellationToken);
            return true;
        }
    }
}
