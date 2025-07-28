using Leaguelane.ApiService.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.ApiService.Handlers
{
    public record UpdateJobConfigurationCommand(int id, JobConfigurationsDto JobConfigurations) : IRequest<JobConfigurationResponse>;
    public class UpdateJobConfigurationCommandHandler: IRequestHandler<UpdateJobConfigurationCommand, JobConfigurationResponse>
    {
        private readonly IJobConfigurationService _jobConfigurationService;

        public UpdateJobConfigurationCommandHandler(IJobConfigurationService jobConfigurationService)
        {
            _jobConfigurationService = jobConfigurationService;
        }

        public async Task<JobConfigurationResponse> Handle(UpdateJobConfigurationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var jobConfiguration = await _jobConfigurationService.GetJobConfigurationById((Enums.Enums.Jobs)request.id, cancellationToken);
                if (jobConfiguration == null)
                {
                    return new JobConfigurationResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = "Job configuratoin not found"
                    };
                }

                jobConfiguration.StartDate = request.JobConfigurations.StartDate;
                jobConfiguration.JobParameter = request.JobConfigurations.JobParameter;
                jobConfiguration.EndDate = request.JobConfigurations.EndDate;
                jobConfiguration.Active = request.JobConfigurations.Active;
                jobConfiguration.Updated = DateTime.UtcNow;

                jobConfiguration = await _jobConfigurationService.UpdateJobConfiguration(jobConfiguration, cancellationToken);

                return new JobConfigurationResponse
                {
                    IsSuccess = true,
                    JobConfiguration = JobConfigurationsMapper.MapToDto(jobConfiguration),
                };
            }
            catch (Exception ex)
            {
                return new JobConfigurationResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                };
            }
        }
    }
}
