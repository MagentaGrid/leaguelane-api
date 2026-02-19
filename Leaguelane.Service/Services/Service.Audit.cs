using Leaguelane.Enums.Enums;
using Leaguelane.Repository.Repositories;

namespace Leaguelane.Service.Services
{
    public class AuditService : IAuditService
    {
        private readonly IAuditRepository _auditRepository;
        public AuditService(IAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }
        public async Task<int> AddAuditAsync(Jobs job, string? message, CancellationToken cancellationToken)
        {
            return await _auditRepository.AddAuditAsync(job, message, cancellationToken);
        }
        public async Task UpdateAuditAsync(int auditId, string status, string? message, CancellationToken cancellationToken)
        {
            await _auditRepository.UpdateAuditAsync(auditId, status, message, cancellationToken);
        }

        public async Task<bool> IsCompletedJobExist(Jobs job, CancellationToken cancellationToken)
        {
            return await _auditRepository.IsCompletedJobExist(job,cancellationToken);
        }
    }
}
