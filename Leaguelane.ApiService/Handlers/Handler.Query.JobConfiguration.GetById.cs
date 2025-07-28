using Leaguelane.ApiService.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.ApiService.Handlers
{
    public record GetJobConfigurationQuery(int id): IRequest<JobConfigurationResponse>;
    public class GetJobConfigurationQueryHandler: IRequestHandler<GetJobConfigurationQuery, JobConfigurationResponse>
    {
        private readonly IJobConfigurationService _jobConfigurationService;

        public GetJobConfigurationQueryHandler(IJobConfigurationService jobConfigurationService)
        {
            _jobConfigurationService = jobConfigurationService;
        }

        public async Task<JobConfigurationResponse> Handle(GetJobConfigurationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _jobConfigurationService.GetJobConfigurationById((Enums.Enums.Jobs)request.id, cancellationToken);
                if (result == null)
                {
                    return new JobConfigurationResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = "Job configuration not found"
                    };
                }

                return new JobConfigurationResponse
                {
                    IsSuccess = true,
                    JobConfiguration = JobConfigurationsMapper.MapToDto(result)
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
