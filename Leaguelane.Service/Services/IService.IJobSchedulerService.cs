using Leaguelane.Persistence.Entities;

namespace Leaguelane.Service.Services
{
    public interface IJobSchedulerService
    {
        Task<List<JobSchedueler>> GetAllJobScheduler(CancellationToken cancellationToken);
    }
}
