using Leaguelane.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Models.Dtos
{
    public class PriceConfigDto
    {
        public int PriceConfigId { get; set; }
        public string PriceConfigName { get; set; }
        public decimal PriceConfigValue { get; set; }
        public bool? Active { get; set; } = true;

    }

    public record PriceConfigResponse
    (
         bool IsSuccess,
         string? Message,
         PriceConfig? PriceConfig
    );

    public class PriceConfigsResponse : Response
    {
        public List<PriceConfigResponseDto>? PriceConfigs { get; set; }
    }

    public class PriceConfigResponseDto
    {
        public int PriceConfigId { get; set; }
        public string? PriceConfigName { get; set; }
        public decimal PriceConfigValue { get; set; }
        public bool? Active { get; set; }
    }
}
