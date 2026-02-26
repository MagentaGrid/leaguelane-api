using Leaguelane.Enums.Enums;
using Leaguelane.Service.Services;
using Quartz;

namespace Leaguelane.Scheduler.Scheduler
{
    public class CountryScheduler: IJob
    {
        private readonly ICountryService _countryService;
        private readonly IAuditService _auditService;
        private readonly IJobSchedulerService _jobSchedulerService;
        public CountryScheduler(ICountryService countryService, IAuditService auditService, IJobSchedulerService jobSchedulerService)
        {
            _countryService = countryService;
            _auditService = auditService;
            _jobSchedulerService = jobSchedulerService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            int auditId = await _auditService.AddAuditAsync(Jobs.Country, "Country api scheduler initiated", CancellationToken.None);
            await _jobSchedulerService.UpdateJobSchedulerStatus(Jobs.Country, "InProgress", "Intiated scheduler", CancellationToken.None);
            try
            {
                await _countryService.GetAllCountriesAsync(CancellationToken.None);
                await _jobSchedulerService.UpdateJobSchedulerStatus(Jobs.Country, "Completed", "Completed scheduler", CancellationToken.None);
                await _auditService.UpdateAuditAsync(auditId, "Completed", "Country api scheduler completed successfully", CancellationToken.None);
            }
            catch (Exception ex)
            {
                await _jobSchedulerService.UpdateJobSchedulerStatus(Jobs.Country, "Failed", ex.Message, CancellationToken.None);
                await _auditService.UpdateAuditAsync(auditId, "Failed", ex.Message, CancellationToken.None);
            }
        }
    }
}
