using Leaguelane.Enums.Enums;
using Leaguelane.Service.Services;
using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Scheduler.Scheduler
{
    public class OddsScheduler : IJob
    {
        private readonly IOddsService _oddsService;
        private readonly IAuditService _auditService;
        private readonly IJobSchedulerService _jobSchedulerService;
        public OddsScheduler(IOddsService oddsService, IAuditService auditService, IJobSchedulerService jobSchedulerService)
        {
            _oddsService = oddsService;
            _auditService = auditService;
            _jobSchedulerService = jobSchedulerService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            int auditId = await _auditService.AddAuditAsync(Jobs.Odds, "Odds api scheduler initiated", CancellationToken.None);
            await _jobSchedulerService.UpdateJobSchedulerStatus(Jobs.Odds, "InProgress", "Intiated scheduler", CancellationToken.None);
            try
            {
                await _oddsService.FetchOddsAsync(CancellationToken.None);
                await _jobSchedulerService.UpdateJobSchedulerStatus(Jobs.Odds, "Completed", "Completed scheduler", CancellationToken.None);
                await _auditService.UpdateAuditAsync(auditId, "Completed", "Odds api scheduler completed successfully", CancellationToken.None);
            }
            catch (Exception ex)
            {
                await _jobSchedulerService.UpdateJobSchedulerStatus(Jobs.Odds, "Failed", "Failed scheduler", CancellationToken.None);
                await _auditService.UpdateAuditAsync(auditId, "Failed", ex.Message, CancellationToken.None);
            }
        }
    }
}
