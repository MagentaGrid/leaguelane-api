using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Persistence.Entities
{
    public class Country: Entity
    {
        [Key]
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; }
        public string? FlagUrl { get; set; }
    }
}
