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
        private readonly IJobSchedulerService _jobSchedulerService;
        public BookmakerScheduler(IBookmakerService bookmakerService, IAuditService auditService, IJobSchedulerService jobSchedulerService)
        {
            _bookmakerService = bookmakerService;
            _auditService = auditService;
            _jobSchedulerService = jobSchedulerService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            int auditId = await _auditService.AddAuditAsync(Jobs.Bookmaker, "Bookmaker api scheduler initiated", CancellationToken.None);
            await _jobSchedulerService.UpdateJobSchedulerStatus(Jobs.Bookmaker, "InProgress", "Intiated scheduler", CancellationToken.None);
            try
            {
                await _bookmakerService.ImportBookmakersFromApiAsync(CancellationToken.None);
                await _jobSchedulerService.UpdateJobSchedulerStatus(Jobs.Bookmaker, "Completed", "Completed scheduler", CancellationToken.None);
                await _auditService.UpdateAuditAsync(auditId, "Completed", "Bookmaker api scheduler completed successfully", CancellationToken.None);
            }
            catch (Exception ex)
            {
                await _jobSchedulerService.UpdateJobSchedulerStatus(Jobs.Bookmaker, "Failed", ex.Message, CancellationToken.None);
                await _auditService.UpdateAuditAsync(auditId, "Failed", ex.Message, CancellationToken.None);
            }
        }
    }
}
