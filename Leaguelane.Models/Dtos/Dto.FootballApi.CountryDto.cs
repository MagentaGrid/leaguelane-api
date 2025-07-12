using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Leaguelane.Models.Dtos
{
    public class CountryDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("flag")]
        public string Flag { get; set; }
    }
}
