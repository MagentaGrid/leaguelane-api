using Leaguelane.Enums.Enums;
using Leaguelane.Service.Services;
using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Scheduler.Scheduler
{
    public class RoundsScheduler : IJob
    {
        private readonly IRoundService _roundService;
        private readonly IAuditService _auditService;
        private readonly IJobSchedulerService _jobSchedulerService;
        public RoundsScheduler(IRoundService roundService, IAuditService auditService, IJobSchedulerService jobSchedulerService)
        {
            _roundService = roundService;
            _auditService = auditService;
            _jobSchedulerService = jobSchedulerService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            int auditId = await _auditService.AddAuditAsync(Jobs.Round, "Rounds api scheduler initiated", CancellationToken.None);
            await _jobSchedulerService.UpdateJobSchedulerStatus(Jobs.Round, "InProgress", "Intiated scheduler", CancellationToken.None);
            try
            {
                await _roundService.ImportAllRounds(CancellationToken.None);
                await _jobSchedulerService.UpdateJobSchedulerStatus(Jobs.Round, "Completed", "Rounds api scheduler completed successfully", CancellationToken.None);
                await _auditService.UpdateAuditAsync(auditId, "Completed", "Rounds api scheduler completed successfully", CancellationToken.None);
            }
            catch (Exception ex)
            {
                await _jobSchedulerService.UpdateJobSchedulerStatus(Jobs.Round, "Failed", ex.Message, CancellationToken.None);
                await _auditService.UpdateAuditAsync(auditId, "Failed", ex.Message, CancellationToken.None);
            }
        }
    }
}
