using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record GetPriceConfigDetailsQuery(int Id) : IRequest<PriceConfigResponse>;
    public class GetPriceConfigDetailsQueryHandler : IRequestHandler<GetPriceConfigDetailsQuery, PriceConfigResponse>
    {
        private readonly IPriceConfigService _priceConfigService;

        public GetPriceConfigDetailsQueryHandler(IPriceConfigService priceConfigService)
        {
            _priceConfigService = priceConfigService;
        }
        public async Task<PriceConfigResponse> Handle(GetPriceConfigDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _priceConfigService.GetPriceConfigById(request.Id, cancellationToken);

                if (result == null)
                {
                    return new PriceConfigResponse(false, "Price config not found", null);
                }

                return new PriceConfigResponse(true, "Price config details fetched successfully", result);
            }
            catch (Exception ex)
            {
                return new PriceConfigResponse(false, ex.Message, null);
            }
        }
    }
}
