using Leaguelane.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Leaguelane.Persistence.Context;

public class LoggingDbContext : DbContext
{
    public LoggingDbContext(DbContextOptions<LoggingDbContext> options) : base(options)
    {
    }

    public DbSet<LogEntry> LogEntries => Set<LogEntry>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<ExternalApiError> ExternalApiErrors => Set<ExternalApiError>();
}
