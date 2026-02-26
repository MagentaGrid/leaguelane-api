using Leaguelane.Enums.Enums;
using Leaguelane.Service.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Scheduler.Scheduler
{
    public class LeagueScheduler: IJob
    {
        private readonly ILeagueService _leagueService;
        private readonly IAuditService _auditService;
        private readonly IJobSchedulerService _jobSchedulerService;

        public LeagueScheduler(ILeagueService leagueService, IAuditService auditService, IJobSchedulerService jobSchedulerService)
        {
            _leagueService = leagueService;
            _auditService = auditService;
            _jobSchedulerService = jobSchedulerService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            int auditId = await _auditService.AddAuditAsync(Jobs.League, "League api scheduler initiated", CancellationToken.None);
            await _jobSchedulerService.UpdateJobSchedulerStatus(Jobs.League, "InProgress", "Intiated scheduler", CancellationToken.None);
            try
            {
                await _leagueService.GetAllLeaguesAsync(CancellationToken.None);
                await _jobSchedulerService.UpdateJobSchedulerStatus(Jobs.League, "Completed", "Completed scheduler", CancellationToken.None);
                await _auditService.UpdateAuditAsync(auditId, "Completed", "League api scheduler completed successfully", CancellationToken.None);
            }
            catch (Exception ex)
            {
                await _jobSchedulerService.UpdateJobSchedulerStatus(Jobs.League, "Failed", ex.Message, CancellationToken.None);
                await _auditService.UpdateAuditAsync(auditId, "Failed", ex.Message, CancellationToken.None);
            }
        }
    }
}
