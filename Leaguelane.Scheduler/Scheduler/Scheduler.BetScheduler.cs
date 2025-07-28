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
        public BetScheduler(IBetService betService, IAuditService auditService)
        {
            _betService = betService;
            _auditService = auditService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            int auditId = await _auditService.AddAuditAsync(Jobs.Bet, "Bet api scheduler initiated", CancellationToken.None);
            try
            {
                await _betService.GetAllBetsAsync(CancellationToken.None);
                await _auditService.UpdateAuditAsync(auditId, "Completed", "Bet api scheduler completed successfully", CancellationToken.None);
            }
            catch (Exception ex)
            {
                await _auditService.UpdateAuditAsync(auditId, "Failed", ex.Message, CancellationToken.None);
            }
        }
    }
}
