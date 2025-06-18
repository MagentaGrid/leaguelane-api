using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;

namespace Leaguelane.Api.Mappers
{
    public static class PriceConfigMapper
    {
        public static PriceConfig MapPriceConfigDtoToPriceConfig(PriceConfigDto priceConfig)
        {
            return new PriceConfig
            {
                PriceConfigName = priceConfig.PriceConfigName,
                PriceConfigValue = priceConfig.PriceConfigValue,
                Active = true,
                Created = DateTime.UtcNow
            };
        }

        public static List<PriceConfigResponseDto> MapPriceConfigToDto(List<PriceConfig> priceConfig)
        {
            return priceConfig.Select(price => new PriceConfigResponseDto
            {
                PriceConfigId = price.PriceConfigId,
                PriceConfigName = price.PriceConfigName,
                PriceConfigValue = price.PriceConfigValue,
                Active = price.Active,
            }).ToList();
        }
    }
}
