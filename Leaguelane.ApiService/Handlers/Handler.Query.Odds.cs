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
    public record GetOddsQuery(int? FixtureId, int? BookmakerId, string? Market, int Skip, int Take) : IRequest<List<OddsDto>>;
    public class GetOddsQueryHandler : IRequestHandler<GetOddsQuery, List<OddsDto>>
    {
        private readonly IOddsService _oddsService;
        public GetOddsQueryHandler(IOddsService oddsService)
        {
            _oddsService = oddsService;
        }
        public async Task<List<OddsDto>> Handle(GetOddsQuery request, CancellationToken cancellationToken)
        {
            var odds = await _oddsService.GetOddsAsync(request.FixtureId, request.BookmakerId, request.Market, request.Skip, request.Take, true, cancellationToken);
            // No additional mapping needed, already OddsDto
            return odds;
        }
    }

    

    

    
}
