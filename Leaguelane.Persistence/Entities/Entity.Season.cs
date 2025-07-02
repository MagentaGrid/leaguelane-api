using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Persistence.Entities
{
    public class Season: Entity
    {
        [Key]
        public int SeasonId { get; set; }
        public int Year { get; set; }
    }
}
