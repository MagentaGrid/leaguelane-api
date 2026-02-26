using Leaguelane.Enums.Enums;
using Leaguelane.Service.Services;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Leaguelane.Scheduler.Scheduler
{
    public class BetScheduler : IJob
    {
        private readonly IBetService _betService;
        private readonly IAuditService _auditService;
        private readonly IJobSchedulerService _jobSchedulerService;
        public BetScheduler(IBetService betService, IAuditService auditService, IJobSchedulerService jobSchedulerService)
        {
            _betService = betService;
            _auditService = auditService;
            _jobSchedulerService = jobSchedulerService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            int auditId = await _auditService.AddAuditAsync(Jobs.Bet, "Bet api scheduler initiated", CancellationToken.None);
            await _jobSchedulerService.UpdateJobSchedulerStatus(Jobs.Bet, "InProgress", "Intiated scheduler", CancellationToken.None);
            try
            {
                await _betService.GetAllBetsAsync(CancellationToken.None);
                await _jobSchedulerService.UpdateJobSchedulerStatus(Jobs.Bet, "Completed", "Completed scheduler", CancellationToken.None);
                await _auditService.UpdateAuditAsync(auditId, "Completed", "Bet api scheduler completed successfully", CancellationToken.None);
            }
            catch (Exception ex)
            {
                await _jobSchedulerService.UpdateJobSchedulerStatus(Jobs.Bet, "Failed", ex.Message, CancellationToken.None);
                await _auditService.UpdateAuditAsync(auditId, "Failed", ex.Message, CancellationToken.None);
            }
        }
    }
}
