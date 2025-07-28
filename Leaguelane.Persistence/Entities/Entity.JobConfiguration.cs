using Leaguelane.Enums.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Persistence.Entities
{
    public class JobConfiguration : Entity
    {
        [Key]
        public Jobs JobId { get; set; }
        public string JobName { get; set; }
        public string JobType { get; set; }
        public string? JobParameter { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int SportId { get; set; }

        [ForeignKey("SportId")]
        public Sport Sport { get; set; }
    }
}
