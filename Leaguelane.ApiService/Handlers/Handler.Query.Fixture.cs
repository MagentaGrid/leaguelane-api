using Leaguelane.ApiService.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using Leaguelane.Service.Services;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.ApiService.Handlers
{
    // Query Handlers
    public record GetAllFixturesQuery() : IRequest<List<FixtureListItemDto>>;
    public class GetAllFixturesQueryHandler : IRequestHandler<GetAllFixturesQuery, List<FixtureListItemDto>>
    {
        private readonly IFixtureService _fixtureService;
        public GetAllFixturesQueryHandler(IFixtureService fixtureService)
        {
            _fixtureService = fixtureService;
        }
        public async Task<List<FixtureListItemDto>> Handle(GetAllFixturesQuery request, CancellationToken cancellationToken)
        {
            var fixtures = await _fixtureService.GetAllFixturesAsync(cancellationToken);
            return fixtures.ConvertAll(FixtureMapper.MapToListItemDto);
        }
    }

    

    
}
