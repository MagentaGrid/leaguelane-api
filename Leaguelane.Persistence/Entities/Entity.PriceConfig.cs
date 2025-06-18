using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Persistence.Entities
{
    public class PriceConfig : Entity
    {
        public int PriceConfigId { get; set; }

        public string PriceConfigName { get; set; }

        public decimal PriceConfigValue { get; set; }
    }
}
