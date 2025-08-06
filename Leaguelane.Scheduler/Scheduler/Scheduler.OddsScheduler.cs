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
            int leagueId = 39; // as per requirement
            int seasonId = 2019; // as per requirement
            int sportId = 1; // assuming football
            int auditId = await _auditService.AddAuditAsync(Jobs.Bet, "Odds api scheduler initiated", CancellationToken.None);
            try
            {
                await _oddsService.FetchAndStoreOddsAsync(leagueId, seasonId, sportId, CancellationToken.None);
                await _auditService.UpdateAuditAsync(auditId, "Completed", "Odds api scheduler completed successfully", CancellationToken.None);
            }
            catch (Exception ex)
            {
                await _auditService.UpdateAuditAsync(auditId, "Failed", ex.Message, CancellationToken.None);
            }
        }
    }
}
