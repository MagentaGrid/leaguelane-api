using Leaguelane.Enums.Enums;
using Leaguelane.Service.Services;
using Quartz;

namespace Leaguelane.Scheduler.Scheduler
{
    public class CountryScheduler: IJob
    {
        private readonly ICountryService _countryService;
        private readonly IAuditService _auditService;
        public CountryScheduler(ICountryService countryService, IAuditService auditService)
        {
            _countryService = countryService;
            _auditService = auditService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            int auditId = await _auditService.AddAuditAsync(Jobs.Country, "Country api scheduler initiated", CancellationToken.None);
            try
            {
                await _countryService.GetAllCountriesAsync(CancellationToken.None);
                await _auditService.UpdateAuditAsync(auditId, "Completed", "Country api scheduler completed successfully", CancellationToken.None);
            }
            catch (Exception ex)
            {
                await _auditService.UpdateAuditAsync(auditId, "Failed", ex.Message, CancellationToken.None);
            }
        }
    }
}
