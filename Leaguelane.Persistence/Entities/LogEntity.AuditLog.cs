using System.ComponentModel.DataAnnotations;

namespace Leaguelane.Persistence.Entities;

public class AuditLog
{
    [Key]
    public long Id { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Method { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string? QueryString { get; set; }
    public string? RequestBody { get; set; }
    public string? ResponseBody { get; set; }
    public int StatusCode { get; set; }
    public long DurationMs { get; set; }
    public string? User { get; set; }
    public string? IpAddress { get; set; }
}
