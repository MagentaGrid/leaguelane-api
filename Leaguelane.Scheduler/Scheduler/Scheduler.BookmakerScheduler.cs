using Leaguelane.Enums.Enums;
using Leaguelane.Service.Services;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Leaguelane.Scheduler.Scheduler
{
    public class BookmakerScheduler : IJob
    {
        private readonly IBookmakerService _bookmakerService;
        private readonly IAuditService _auditService;
        public BookmakerScheduler(IBookmakerService bookmakerService, IAuditService auditService)
        {
            _bookmakerService = bookmakerService;
            _auditService = auditService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            int auditId = await _auditService.AddAuditAsync(Jobs.Bookmaker, "Bookmaker api scheduler initiated", CancellationToken.None);
            try
            {
                await _bookmakerService.ImportBookmakersFromApiAsync(CancellationToken.None);
                await _auditService.UpdateAuditAsync(auditId, "Completed", "Bookmaker api scheduler completed successfully", CancellationToken.None);
            }
            catch (Exception ex)
            {
                await _auditService.UpdateAuditAsync(auditId, "Failed", ex.Message, CancellationToken.None);
            }
        }
    }
}
