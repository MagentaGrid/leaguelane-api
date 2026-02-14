using System.ComponentModel.DataAnnotations;

namespace Leaguelane.Persistence.Entities;

public class LogEntry
{
    [Key]
    public int Id { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Message { get; set; } = string.Empty;
    public string? StackTrace { get; set; }
    public string? Source { get; set; }
    public string? RequestPath { get; set; }
    public string? RequestMethod { get; set; }
    public string? User { get; set; }
}
