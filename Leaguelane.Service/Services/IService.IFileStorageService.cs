using Leaguelane.Models.Dtos;

namespace Leaguelane.Service.Services
{
    public interface IFileStorageService
    {
        Task<FileUploadResult> UploadAsync(
        string path,
        Stream content,
        string contentType,
        CancellationToken ct = default);
    }
}
