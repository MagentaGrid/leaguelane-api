using Leaguelane.ApiService.Mappers;
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
            var (fixtures, totalCount) = await _fixtureService.GetAllFixturesWithPaginationAsync(request.page, request.pageSize, cancellationToken);

            if (fixtures == null || !fixtures.Any())
                return new List<FixtureListItemDto>();

            var teamIds = fixtures
                .Where(f => f.HomeTeamId.HasValue || f.AwayTeamId.HasValue)
                .SelectMany(f => new[] { f.HomeTeamId ?? 0, f.AwayTeamId ?? 0 })
                .Where(id => id != 0)
                .Distinct()
                .ToList();

            var teams = await _teamService.GetAllTeamsById(teamIds, cancellationToken);

            return fixtures.ConvertAll(x => FixtureMapper.MapToListItemDto(x, teams));
        }
    }        
}
