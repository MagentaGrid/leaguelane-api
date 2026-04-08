using Leaguelane.Models.Dtos;

namespace Leaguelane.ApiService.Feature
{
    public interface IFileFeatureService
    {
        Task<BaseResponse> UploadFile(IFormFile file, string module, CancellationToken cancellationToken);
    }
}
