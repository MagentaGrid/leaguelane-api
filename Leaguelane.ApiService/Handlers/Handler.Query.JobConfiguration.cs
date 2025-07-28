using Leaguelane.ApiService.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.ApiService.Handlers
{
    public record GetAllJobConfigurationQuery() : IRequest<JobConfigurationsResponse>;
    public class GetAllJobConfigurationQueryHandler: IRequestHandler<GetAllJobConfigurationQuery, JobConfigurationsResponse>
    {
        private readonly IJobConfigurationService _jobConfigurationService;

        public GetAllJobConfigurationQueryHandler(IJobConfigurationService jobConfigurationService)
        {
            _jobConfigurationService = jobConfigurationService;
        }

        public async Task<JobConfigurationsResponse> Handle(GetAllJobConfigurationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _jobConfigurationService.GetAllJobConfigurations(cancellationToken);

                return new JobConfigurationsResponse
                {
                    IsSuccess = true,
                    JobConfigurations = [.. result.Select(JobConfigurationsMapper.MapToDto)],
                };
            }
            catch(Exception ex)
            {
                return new JobConfigurationsResponse
                {
                    IsSuccess = true,
                    ErrorMessage = ex.Message,
                };
            }
        }
    }
}
