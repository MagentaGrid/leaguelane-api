using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.ApiService.Handlers
{
    public record GetOddsByIdQuery(int Id) : IRequest<OddsDto>;
    public class GetOddsByIdQueryHandler : IRequestHandler<GetOddsByIdQuery, OddsDto>
    {
        private readonly IOddsService _oddsService;
        public GetOddsByIdQueryHandler(IOddsService oddsService)
        {
            _oddsService = oddsService;
        }
        public async Task<OddsDto> Handle(GetOddsByIdQuery request, CancellationToken cancellationToken)
        {
            return await _oddsService.GetOddsByIdAsync(request.Id, true, cancellationToken);
        }
    }
}
