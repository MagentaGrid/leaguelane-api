using Leaguelane.Api.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record GetPriceConfigQuery : IRequest<PriceConfigsResponse>;
    public class GetPriceConfigQueryHandler : IRequestHandler<GetPriceConfigQuery, PriceConfigsResponse>
    {
        private readonly IPriceConfigService _priceConfigService;

        public GetPriceConfigQueryHandler(IPriceConfigService priceConfigService)
        {
            _priceConfigService = priceConfigService;
        }
        public async Task<PriceConfigsResponse> Handle(GetPriceConfigQuery request, CancellationToken cancellationToken)
        {
            var result = await _priceConfigService.GetAllPriceConfigs(cancellationToken);
            return new PriceConfigsResponse
            {
                IsSuccess = true,
                ErrorMessage = "Price configs fetched successfully",
                PriceConfigs = PriceConfigMapper.MapPriceConfigToDto(result)
            };
        }
    }
}
