using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;

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
    }
}
