using Leaguelane.Enums.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public interface IAuditService
    {
        Task<int> AddAuditAsync(Jobs job, string? message, CancellationToken cancellationToken);
        Task UpdateAuditAsync(int auditId, string status, string? message, CancellationToken cancellationToken);
        Task<bool> IsCompletedJobExist(Jobs job, CancellationToken cancellationToken);
    }
}
