using Leaguelane.Enums.Enums;
using Leaguelane.Persistence.Entities;

namespace Leaguelane.Service.Services
{
    public interface IJobSchedulerService
    {
        Task<List<JobSchedueler>> GetAllJobScheduler(CancellationToken cancellationToken);
        Task<bool> UpdateJobSchedulerStatus(Jobs job, string status, string message, CancellationToken cancellationToken);
    }
}
