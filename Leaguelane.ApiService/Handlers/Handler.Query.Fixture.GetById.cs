using Leaguelane.ApiService.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.ApiService.Handlers
{
    public record GetFixtureByIdQuery(int Id) : IRequest<FixtureDto>;
    public class GetFixtureByIdQueryHandler : IRequestHandler<GetFixtureByIdQuery, FixtureDto>
    {
        private readonly IFixtureService _fixtureService;
        public GetFixtureByIdQueryHandler(IFixtureService fixtureService)
        {
            _fixtureService = fixtureService;
        }
        public async Task<FixtureDto> Handle(GetFixtureByIdQuery request, CancellationToken cancellationToken)
        {
            var fixture = await _fixtureService.GetFixtureByIdAsync(request.Id, cancellationToken);
            return fixture == null ? null : FixtureMapper.MapToDto(fixture);
        }
    }
}
