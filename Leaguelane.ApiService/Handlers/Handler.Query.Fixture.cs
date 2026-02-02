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
    public record GetAllFixturesQuery(int page = 1, int pageSize = 6) : IRequest<List<FixtureListItemDto>>;
    public class GetAllFixturesQueryHandler : IRequestHandler<GetAllFixturesQuery, List<FixtureListItemDto>>
    {
        private readonly IFixtureService _fixtureService;
        private readonly ITeamService _teamService;
        public GetAllFixturesQueryHandler(IFixtureService fixtureService, ITeamService teamService)
        {
            _fixtureService = fixtureService;
            _teamService = teamService;
        }
        public async Task<List<FixtureListItemDto>> Handle(GetAllFixturesQuery request, CancellationToken cancellationToken)
        {
            var fixtures = await _fixtureService.GetAllFixturesWithPaginationAsync(request.page, request.pageSize, cancellationToken);
            var teams = await _teamService.GetAllTeamsById(fixtures.Select(f => (int)f.HomeTeamId).Concat( fixtures.Select(f => (int)f.AwayTeamId)), cancellationToken);
            return fixtures.ConvertAll(x => FixtureMapper.MapToListItemDto(x, teams));
        }
    }        
}
