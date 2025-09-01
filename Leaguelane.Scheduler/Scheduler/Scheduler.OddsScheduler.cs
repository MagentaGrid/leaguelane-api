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
        public OddsScheduler(IOddsService oddsService, IAuditService auditService)
        {
            _oddsService = oddsService;
            _auditService = auditService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            int auditId = await _auditService.AddAuditAsync(Jobs.Odds, "Odds api scheduler initiated", CancellationToken.None);
            try
            {
                await _oddsService.FetchOddsAsync(CancellationToken.None);
                await _auditService.UpdateAuditAsync(auditId, "Completed", "Odds api scheduler completed successfully", CancellationToken.None);
            }
            catch (Exception ex)
            {
                await _auditService.UpdateAuditAsync(auditId, "Failed", ex.Message, CancellationToken.None);
            }
        }
    }
}
