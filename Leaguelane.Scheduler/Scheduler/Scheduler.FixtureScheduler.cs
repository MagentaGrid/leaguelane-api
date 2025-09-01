using Leaguelane.Enums.Enums;
using Leaguelane.Service.Services;
using Quartz;

namespace Leaguelane.Scheduler.Scheduler
{
    public class FixtureScheduler : IJob
    {
        private readonly IAuditService _auditService;
        private readonly IFixtureService _fixtureService;
        public FixtureScheduler(IFixtureService fixtureService, IAuditService auditService)
        {
            _auditService = auditService;
            _fixtureService = fixtureService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            int auditId = await _auditService.AddAuditAsync(Jobs.Fixture, "Fixture api scheduler initiated", CancellationToken.None);
            try
            {
                await _fixtureService.GetAllFixtures(CancellationToken.None);
                await _auditService.UpdateAuditAsync(auditId, "Completed", "Fixture api scheduler completed successfully", CancellationToken.None);
            }
            catch (Exception ex)
            {
                await _auditService.UpdateAuditAsync(auditId, "Failed", ex.Message, CancellationToken.None);
            }
        }
    }
}
