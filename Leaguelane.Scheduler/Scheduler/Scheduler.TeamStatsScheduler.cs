using Leaguelane.Enums.Enums;
using Leaguelane.Service.Services;
using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Scheduler.Scheduler
{
    public class TeamStatsScheduler : IJob
    {
        private readonly ITeamStatService _teamStatService;
        private readonly IAuditService _auditService;
        private readonly IJobSchedulerService _jobSchedulerService;
        public TeamStatsScheduler(ITeamStatService teamStatService, IAuditService auditService, IJobSchedulerService jobSchedulerService)
        {
            _teamStatService = teamStatService;
            _auditService = auditService;
            _jobSchedulerService = jobSchedulerService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            int leagueId = 39; // as per requirement
            int teamId = 33; // as per requirement
            int seasonId = 2019; // as per requirement
            int sportId = 1; // assuming football
            int auditId = await _auditService.AddAuditAsync(Jobs.TeamStat, "TeamStats api scheduler initiated", CancellationToken.None);
            await _jobSchedulerService.UpdateJobSchedulerStatus(Jobs.TeamStat, "InProgress", "Intiated scheduler", CancellationToken.None);
            try
            {
                await _teamStatService.FetchAndStoreTeamStatsAsync(leagueId, teamId, seasonId, sportId, CancellationToken.None);
                await _jobSchedulerService.UpdateJobSchedulerStatus(Jobs.TeamStat, "Completed", "TeamStats api scheduler completed successfully", CancellationToken.None);
                await _auditService.UpdateAuditAsync(auditId, "Completed", "TeamStats api scheduler completed successfully", CancellationToken.None);
            }
            catch (Exception ex)
            {
                await _jobSchedulerService.UpdateJobSchedulerStatus(Jobs.TeamStat, "Failed", ex.Message, CancellationToken.None);
                await _auditService.UpdateAuditAsync(auditId, "Failed", ex.Message, CancellationToken.None);
            }
        }
    }
}
