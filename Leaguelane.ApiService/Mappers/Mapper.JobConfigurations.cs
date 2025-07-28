using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;

namespace Leaguelane.ApiService.Mappers
{
    public class JobConfigurationsMapper
    {
        public static JobConfigurationResponseDto MapToDto(JobConfiguration jobConfiguration)
        {
            if (jobConfiguration == null) return null;
            return new JobConfigurationResponseDto
            {
                JobId = jobConfiguration.JobId,
                JobName = jobConfiguration.JobName,
                JobType = jobConfiguration.JobType,
                JobParameter = jobConfiguration.JobParameter,
                StartDate = jobConfiguration.StartDate,
                EndDate = jobConfiguration.EndDate,
                SportId = jobConfiguration.SportId
            };
        }
        public static JobConfiguration MapToEntity(JobConfigurationsDto jobConfigurationRequest)
        {
            if (jobConfigurationRequest == null) return null;
            return new JobConfiguration
            {
                JobId = jobConfigurationRequest.JobId,
                JobName = jobConfigurationRequest.JobName,
                JobType = jobConfigurationRequest.JobType,
                JobParameter = jobConfigurationRequest.JobParameter,
                StartDate = jobConfigurationRequest.StartDate,
                EndDate = jobConfigurationRequest.EndDate,
                SportId = jobConfigurationRequest.SportId,
                Created = DateTime.UtcNow,
                Active =  true
            };
        }
    }
}
