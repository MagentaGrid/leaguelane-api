using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;

namespace Leaguelane.ApiService.Mappers
{
    public static class JobSchedulerMapper
    {
        public static JobSchedulerDto ToDto(this JobSchedueler jobSchedueler)
        {
            return new JobSchedulerDto
            {
                JobScheduelerId = jobSchedueler.JobScheduelerId,
                RunStatus = jobSchedueler.RunStatus,
                LastRun = jobSchedueler.LastRun,
                NextRun = jobSchedueler.NextRun,
                RunBy = jobSchedueler.RunBy,
                Status = jobSchedueler.Status,
                Name = jobSchedueler.Name
            };
        }
    }
}
