using Leaguelane.Models.Dtos;

namespace Leaguelane.ApiService.Feature
{
    public interface IJobSchedulerFeatureService
    {
        Task<BaseResponse> GetAllJobScheduler(CancellationToken cancellationToken);
    }
}
