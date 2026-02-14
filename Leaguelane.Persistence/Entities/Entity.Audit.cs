using Leaguelane.Enums.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Leaguelane.Persistence.Entities;

public class Audit: Entity
{
    [Key]
    public int AuditId { get; set; }

    public Jobs JobId { get; set; }

    public string JobName { get; set; }

    public string Status { get; set; }

    public string? Message { get; set; }

    public int SportId { get; set; }

    [ForeignKey("SportId")]
    public virtual Sport Sport { get; set; }
}
