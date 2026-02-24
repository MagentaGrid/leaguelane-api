using Leaguelane.ApiService.Mappers;
using Leaguelane.Enums.Enums;
using Leaguelane.Models.Dtos;
using Leaguelane.Scheduler.Scheduler;
using Leaguelane.Service.Services;
using Quartz;

namespace Leaguelane.ApiService.Feature
{
    public class JobSchedulerFeatureService : IJobSchedulerFeatureService
    {
        private readonly IJobSchedulerService _jobSchedulerService;
        private readonly ISchedulerFactory _schedulerFactory;
        public JobSchedulerFeatureService(IJobSchedulerService jobSchedulerService, ISchedulerFactory schedulerFactory)
        {
            _jobSchedulerService = jobSchedulerService;
            _schedulerFactory = schedulerFactory;
        }
        public async Task<BaseResponse> GetAllJobScheduler(CancellationToken cancellationToken)
        {
            var data = await _jobSchedulerService.GetAllJobScheduler(cancellationToken);

            return new BaseResponse(true, "Job Schedulers fetched successfully", data.Select(JobSchedulerMapper.ToDto).ToList());
        }

        public async Task<BaseResponse> TriggerJob(Jobs job, CancellationToken cancellationToken)
        {
            var scheduler = await _schedulerFactory.GetScheduler();

            JobKey jobKey = job switch
            {
                Jobs.Country => new JobKey(nameof(CountryScheduler)),
                Jobs.League => new JobKey(nameof(LeagueScheduler)),
                Jobs.Season => new JobKey(nameof(SeasonsScheduler)),
                Jobs.Round => new JobKey(nameof(RoundsScheduler)),
                Jobs.Team => new JobKey(nameof(TeamsScheduler)),
                Jobs.TeamStat => new JobKey(nameof(TeamStatsScheduler)),
                Jobs.Fixture => new JobKey(nameof(FixtureScheduler)),
                _ => throw new Exception("Invalid job")
            };

            if (!await scheduler.CheckExists(jobKey))
                throw new Exception($"Job '{jobKey.Name}' not found");

            await scheduler.TriggerJob(jobKey);

            return new BaseResponse(true, $"{jobKey.Name} triggered successfully", true);
        }
    }
}
