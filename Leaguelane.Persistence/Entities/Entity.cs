using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Persistence.Entities;

public class Entity
{
    public DateTime? Created { get; set; }
    public DateTime? Updated { get; set; }
    public int? CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }

    public bool? Active { get; set; }
}