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
    public class SeasonsScheduler : IJob
    {
        private readonly ISeasonService _seasonService;
        private readonly IAuditService _auditService;
        public SeasonsScheduler(ISeasonService seasonService, IAuditService auditService)
        {
            _seasonService = seasonService;
            _auditService = auditService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            int auditId = await _auditService.AddAuditAsync(Jobs.Season, "Season api scheduler initiated", CancellationToken.None);
            try
            {
                await _seasonService.GetAllSeasons(CancellationToken.None);
                await _auditService.UpdateAuditAsync(auditId, "Completed", "Season api scheduler completed successfully", CancellationToken.None);
            }
            catch (Exception ex)
            {
                await _auditService.UpdateAuditAsync(auditId, "Failed", ex.Message, CancellationToken.None);
            }
        }
    }
}
