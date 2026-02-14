using Leaguelane.Enums.Enums;
using Leaguelane.Persistence.Context;
using Leaguelane.Persistence.Entities;

namespace Leaguelane.Repository.Repositories
{
    public class AuditRepository: IAuditRepository
    {
        private readonly LeaguelaneDbContext _context;
        public AuditRepository(LeaguelaneDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAuditAsync(Jobs job, string? message, CancellationToken cancellationToken)
        {
            var audit = new Audit
            {
                JobId = job,
                JobName = job.ToString(),
                Status = "Started",
                Message = message,
                Active = true,
                Created = DateTime.UtcNow,
                Updated = null,
                SportId = 1
            };

            await _context.Audits.AddAsync(audit);
            await _context.SaveChangesAsync(cancellationToken);

            return audit.AuditId;
        }

        public async Task UpdateAuditAsync(int auditId, string status, string? message, CancellationToken cancellationToken)
        {
            var audit = await _context.Audits.FindAsync(auditId);
            if (audit != null)
            {
                audit.Status = status;
                audit.Message = message;
                audit.Updated = DateTime.UtcNow;
                _context.Audits.Update(audit);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
