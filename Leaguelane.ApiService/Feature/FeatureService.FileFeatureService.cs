using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using Microsoft.Identity.Client.Extensions.Msal;

namespace Leaguelane.ApiService.Feature
{
    public class FileFeatureService: IFileFeatureService
    {
        private readonly IFileStorageService _fileStorageService;

        public FileFeatureService(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }

        public async Task<BaseResponse> UploadFile(IFormFile file,string module, CancellationToken cancellationToken)
        {
            // 1. Basic Validation
            if (file == null || file.Length == 0)
            {
                return new BaseResponse(false, "No file provided",null);
            }

            var extension = Path.GetExtension(file.FileName).ToLower();
            var fileName = $"{Guid.NewGuid()}{extension}";

            var storagePath = $"{module}/{fileName}".ToLower();

            using var stream = file.OpenReadStream();

            var result = await _fileStorageService.UploadAsync(
                storagePath,
                stream,
                file.ContentType,
                cancellationToken);

            return new BaseResponse
            (
                true,
                "File uploaded successfully",
                result
            );
        }
    }
}
