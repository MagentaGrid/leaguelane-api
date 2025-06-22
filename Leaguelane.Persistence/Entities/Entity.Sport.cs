using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Persistence.Entities
{
    public class Sport:Entity
    {
        [Key]
        public int SportId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string ApiUrl { get; set; }
        public string ApiHost { get; set; }
        public string ApiKey { get; set; }
    }
}
