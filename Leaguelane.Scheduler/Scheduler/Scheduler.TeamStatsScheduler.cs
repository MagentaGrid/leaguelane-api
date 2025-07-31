using Leaguelane.Enums.Enums;
using Leaguelane.Service.Services;
using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Scheduler.Scheduler
{
    public class TeamStatsScheduler : IJob
    {
        private readonly ITeamStatService _teamStatService;
        private readonly IAuditService _auditService;
        public TeamStatsScheduler(ITeamStatService teamStatService, IAuditService auditService)
        {
            _teamStatService = teamStatService;
            _auditService = auditService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            int leagueId = 39; // as per requirement
            int teamId = 33; // as per requirement
            int seasonId = 2019; // as per requirement
            int sportId = 1; // assuming football
            int auditId = await _auditService.AddAuditAsync(Jobs.TeamStat, "TeamStats api scheduler initiated", CancellationToken.None);
            try
            {
                await _teamStatService.FetchAndStoreTeamStatsAsync(leagueId, teamId, seasonId, sportId, CancellationToken.None);
                await _auditService.UpdateAuditAsync(auditId, "Completed", "TeamStats api scheduler completed successfully", CancellationToken.None);
            }
            catch (Exception ex)
            {
                await _auditService.UpdateAuditAsync(auditId, "Failed", ex.Message, CancellationToken.None);
            }
        }
    }
}
