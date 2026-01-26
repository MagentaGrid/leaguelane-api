using Leaguelane.Enums.Enums;
using Leaguelane.Service.Services;
using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Scheduler.Scheduler
{
    public class TeamsScheduler : IJob
    {
        private readonly ITeamService _teamService;
        private readonly IAuditService _auditService;
        public TeamsScheduler(ITeamService teamService, IAuditService auditService)
        {
            _teamService = teamService;
            _auditService = auditService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            int auditId = await _auditService.AddAuditAsync(Jobs.Team, "Teams api scheduler initiated", CancellationToken.None);
            try
            {
                await _teamService.ImportAllTeams(CancellationToken.None);
                await _auditService.UpdateAuditAsync(auditId, "Completed", "Teams api scheduler completed successfully", CancellationToken.None);
            }
            catch (Exception ex)
            {
                await _auditService.UpdateAuditAsync(auditId, "Failed", ex.Message, CancellationToken.None);
            }
        }
    }
}
