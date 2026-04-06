using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Leaguelane.Models.Dtos
{
    public class FootballApiBaseResponseDto<T>
    {
        [JsonPropertyName("get")]
        public string Get { get; set; }

        [JsonPropertyName("errors")]
        public JsonElement Errors { get; set; }

        [JsonPropertyName("results")]
        public int Results { get; set; }

        [JsonPropertyName("paging")]
        public Paging Paging { get; set; }

        [JsonPropertyName("response")]
        public T Response { get; set; }
    }

    public class Paging
    {
        [JsonPropertyName("current")]
        public int Current { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}
