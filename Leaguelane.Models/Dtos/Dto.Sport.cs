using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Models.Dtos
{
    public class SportDto
    {
        public int SportId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string ApiUrl { get; set; }
        public string ApiHost { get; set; }
        public string ApiKey { get; set; }
        public bool Active { get; set; }
    }

    public class SportResponse : Response
    {
        public SportDto Sport { get; set; }
    }

    public class SportsResponse : Response
    {
        public List<SportDto> Sports { get; set; }
    }
}
