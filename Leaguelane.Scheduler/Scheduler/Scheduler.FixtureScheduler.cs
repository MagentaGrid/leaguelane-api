using Leaguelane.Enums.Enums;
using Leaguelane.Service.Services;
using Quartz;

namespace Leaguelane.Scheduler.Scheduler
{
    public class FixtureScheduler : IJob
    {
        private readonly IAuditService _auditService;
        private readonly IFixtureService _fixtureService;
        private readonly IJobSchedulerService _jobSchedulerService;
        public FixtureScheduler(IFixtureService fixtureService, IAuditService auditService, IJobSchedulerService jobSchedulerService)
        {
            _auditService = auditService;
            _fixtureService = fixtureService;
            _jobSchedulerService = jobSchedulerService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            int auditId = await _auditService.AddAuditAsync(Jobs.Fixture, "Fixture api scheduler initiated", CancellationToken.None);
            await _jobSchedulerService.UpdateJobSchedulerStatus(Jobs.Fixture, "InProgress", "Intiated scheduler", CancellationToken.None);
            try
            {
                await _fixtureService.GetAllFixtures(CancellationToken.None);
                await _jobSchedulerService.UpdateJobSchedulerStatus(Jobs.Fixture, "Completed", "Completed scheduler", CancellationToken.None);
                await _auditService.UpdateAuditAsync(auditId, "Completed", "Fixture api scheduler completed successfully", CancellationToken.None);
            }
            catch (Exception ex)
            {
                await _jobSchedulerService.UpdateJobSchedulerStatus(Jobs.Fixture, "Failed", ex.Message, CancellationToken.None);
                await _auditService.UpdateAuditAsync(auditId, "Failed", ex.Message, CancellationToken.None);
            }
        }
    }
}
