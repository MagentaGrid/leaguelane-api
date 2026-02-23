using Leaguelane.ApiService.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;

namespace Leaguelane.ApiService.Feature
{
    public class JobSchedulerFeatureService : IJobSchedulerFeatureService
    {
        private readonly IJobSchedulerService _jobSchedulerService;
        public JobSchedulerFeatureService(IJobSchedulerService jobSchedulerService)
        {
            _jobSchedulerService = jobSchedulerService;
        }
        public async Task<BaseResponse> GetAllJobScheduler(CancellationToken cancellationToken)
        {
            var data = await _jobSchedulerService.GetAllJobScheduler(cancellationToken);

            return new BaseResponse(true, "Job Schedulers fetched successfully", data.Select(JobSchedulerMapper.ToDto).ToList());
        }
    }
}
