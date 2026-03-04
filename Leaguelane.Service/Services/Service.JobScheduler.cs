using Leaguelane.Enums.Enums;
using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;
using Npgsql;

namespace Leaguelane.Service.Services
{
    public class JobSchedulerService: IJobSchedulerService
    {
        private readonly IRepository _repository;
        public JobSchedulerService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<JobSchedueler>> GetAllJobScheduler(CancellationToken cancellationToken)
        {
            return (await _repository.GetAllAsync<JobSchedueler>()).ToList();
        }

        public async Task<bool> UpdateJobSchedulerStatus(Jobs job, string status, string message, CancellationToken cancellationToken)
        {
            var jobScheduler = await _repository.GetByIdAsync<JobSchedueler>(job, cancellationToken);

            if (jobScheduler == null)
            {
                return false;
            }

            jobScheduler.Status = status;
            jobScheduler.RunStatus = message;
            jobScheduler.LastRun = DateTime.UtcNow;
            _repository.Update(jobScheduler);
            await _repository.SaveChangesAsync<JobSchedueler>(cancellationToken);
            return true;
        }
    }
}
