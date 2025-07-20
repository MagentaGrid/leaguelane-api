using Leaguelane.Enums.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Persistence.Entities
{
    public class Audit: Entity
    {
        [Key]
        public int AuditId { get; set; }

        public Jobs JobId { get; set; }

        public string JobName { get; set; }

        public string Status { get; set; }

        public string? Message { get; set; }
    }
}
