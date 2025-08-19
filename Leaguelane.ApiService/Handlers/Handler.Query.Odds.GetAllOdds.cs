using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.ApiService.Handlers
{
    public record GetAllOddsQuery() : IRequest<List<OddsDto>>;
    public class GetAllOddsQueryHandler : IRequestHandler<GetAllOddsQuery, List<OddsDto>>
    {
        private readonly IOddsService _oddsService;
        public GetAllOddsQueryHandler(IOddsService oddsService)
        {
            _oddsService = oddsService;
        }
        public async Task<List<OddsDto>> Handle(GetAllOddsQuery request, CancellationToken cancellationToken)
        {
            return await _oddsService.GetOddsAsync(null, null, null, 0, 1000, false, cancellationToken);
        }
    }
}
