using Leaguelane.Models.Dtos;

namespace Leaguelane.ApiService.Feature
{
    public interface IDashboardFeatureService
    {
        Task<BaseResponse> GetDashboard(CancellationToken cancellationToken);
    }
}
