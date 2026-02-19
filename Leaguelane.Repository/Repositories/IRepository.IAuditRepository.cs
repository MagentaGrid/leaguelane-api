using Leaguelane.Enums.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public interface IAuditRepository
    {
        Task<int> AddAuditAsync(Jobs job, string? message, CancellationToken cancellationToken);
        Task UpdateAuditAsync(int auditId, string status, string? message, CancellationToken cancellationToken);
        Task<bool> IsAnyExistAsync(CancellationToken cancellationToken);
        Task<bool> IsCompletedJobExist(Jobs job, CancellationToken cancellationToken);
    }
}
