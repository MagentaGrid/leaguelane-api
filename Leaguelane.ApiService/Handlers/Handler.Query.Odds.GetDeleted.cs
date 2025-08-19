using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.ApiService.Handlers
{
    public record GetDeletedOddsQuery() : IRequest<List<OddsDto>>;
    public class GetDeletedOddsQueryHandler : IRequestHandler<GetDeletedOddsQuery, List<OddsDto>>
    {
        private readonly IOddsService _oddsService;
        public GetDeletedOddsQueryHandler(IOddsService oddsService)
        {
            _oddsService = oddsService;
        }
        public async Task<List<OddsDto>> Handle(GetDeletedOddsQuery request, CancellationToken cancellationToken)
        {
            return await _oddsService.GetDeletedOddsAsync(cancellationToken);
        }
    }
}
