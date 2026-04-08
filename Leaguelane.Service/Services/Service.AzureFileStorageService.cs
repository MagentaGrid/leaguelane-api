using Leaguelane.Models.Dtos;

namespace Leaguelane.Service.Services
{
    public class AzureFileStorageService: IFileStorageService
    {
        public async Task<FileUploadResult> UploadAsync(
        string path,
        Stream content,
        string contentType,
        CancellationToken ct = default)
        {
            return null;
        }
    }
}
