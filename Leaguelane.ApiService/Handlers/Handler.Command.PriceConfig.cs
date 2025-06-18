using Leaguelane.Api.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record CreatePriceConfigCommand(PriceConfigDto priceConfigDto) : IRequest<PriceConfigResponse>;
    public class CreatePriceConfigCommandHandler : IRequestHandler<CreatePriceConfigCommand, PriceConfigResponse>
    {
        private readonly IPriceConfigService _priceConfigService;

        public CreatePriceConfigCommandHandler(IPriceConfigService priceConfigService)
        {
            _priceConfigService = priceConfigService;
        }

        public async Task<PriceConfigResponse> Handle(CreatePriceConfigCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (await _priceConfigService.IsPriceConfigNameExists(request.priceConfigDto.PriceConfigName, cancellationToken))
                {
                    return new PriceConfigResponse(false, "Price config name already exists", null);
                }

                var priceConfig = await _priceConfigService.CreatePriceConfig(PriceConfigMapper.MapPriceConfigDtoToPriceConfig(request.priceConfigDto), cancellationToken);

                return new PriceConfigResponse(true, "Price config created successfully", priceConfig);
            }
            catch (Exception ex)
            {
                return new PriceConfigResponse(false, ex.Message, null);
            }
        }
    }
}
