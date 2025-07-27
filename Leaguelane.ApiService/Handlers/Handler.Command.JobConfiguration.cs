using Leaguelane.ApiService.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.ApiService.Handlers
{
    public record CreateJobConfigurationCommand(JobConfigurationsDto request) : IRequest<JobConfigurationResponse>;
    public class CreateJobConfigurationCommandHandler: IRequestHandler<CreateJobConfigurationCommand, JobConfigurationResponse>
    {
        private readonly IJobConfigurationService _jobConfigurationService;

        public CreateJobConfigurationCommandHandler(IJobConfigurationService jobConfigurationService)
        {
            _jobConfigurationService = jobConfigurationService;
        }

        public async Task<JobConfigurationResponse> Handle(CreateJobConfigurationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var jobConfiguration = await _jobConfigurationService.AddJobConfiguration(JobConfigurationsMapper.MapToEntity( request.request), cancellationToken);
                return new JobConfigurationResponse
                {
                    JobConfiguration = JobConfigurationsMapper.MapToDto(jobConfiguration),
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new JobConfigurationResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
