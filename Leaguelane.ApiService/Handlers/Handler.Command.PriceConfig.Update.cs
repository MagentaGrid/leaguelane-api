using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record UpdatePriceConfigCommand(PriceConfigDto priceConfigDto, int id) : IRequest<PriceConfigResponse>;
    public class UpdatePriceConfigCommandHandler : IRequestHandler<UpdatePriceConfigCommand, PriceConfigResponse>
    {
        private readonly IPriceConfigService _priceConfigService;

        public UpdatePriceConfigCommandHandler(IPriceConfigService priceConfigService)
        {
            _priceConfigService = priceConfigService;
        }

        public async Task<PriceConfigResponse> Handle(UpdatePriceConfigCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var priceConfig = await _priceConfigService.GetPriceConfigById(request.id, cancellationToken);

                if (priceConfig == null)
                {
                    return new PriceConfigResponse(false, "Price Config not found", null);
                }

                if (await _priceConfigService.IsPriceConfigNameExistsForUpdate(request.priceConfigDto.PriceConfigName, request.id, cancellationToken))
                {
                    return new PriceConfigResponse(false, "Price Config name already exists", null);
                }

                priceConfig.PriceConfigName = request.priceConfigDto.PriceConfigName;
                priceConfig.PriceConfigValue = request.priceConfigDto.PriceConfigValue;
                priceConfig.Active = request.priceConfigDto.Active;
                priceConfig.Updated = DateTime.UtcNow;

                var result = await _priceConfigService.UpdatePriceConfig(priceConfig, cancellationToken);

                return new PriceConfigResponse(true, "Price Config updated successfully", result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
