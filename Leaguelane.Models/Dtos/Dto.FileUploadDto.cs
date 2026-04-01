using Microsoft.AspNetCore.Http;

namespace Leaguelane.Models.Dtos
{
    internal class FileUploadDto
    {
    }

    public class FileUploadResult
    {
        public string Path { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public long SizeBytes { get; set; }
        public string ContentType { get; set; } = null!;
        public DateTime UploadedAt { get; set; }
        public string Url { get; set; }
    }

    public class FileUploadRequest
    {
        public IFormFile File { get; set; } 
        public string Module { get; set; }
    }
}
