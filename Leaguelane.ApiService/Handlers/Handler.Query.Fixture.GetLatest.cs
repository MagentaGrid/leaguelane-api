using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.ApiService.Handlers
{
    public record GetLatestFixtureQuery(int Page, int PageSize) : IRequest<List<FixtureListItemDto>>;
    public class GetLatestFixtureQueryHandler : IRequestHandler<GetLatestFixtureQuery, List<FixtureListItemDto>>
    {
        private readonly IFixtureService _fixtureService;
        public GetLatestFixtureQueryHandler(IFixtureService fixtureService)
        {
            _fixtureService = fixtureService;
        }
        public async Task<List<FixtureListItemDto>> Handle(GetLatestFixtureQuery request, CancellationToken cancellationToken)
        {
            return await _fixtureService.GetLatestFixturesAsync(request.Page, request.PageSize, cancellationToken);
        }
    }
}
