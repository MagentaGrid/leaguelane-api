using Amazon.S3;
using Amazon.S3.Model;
using Leaguelane.Models.Dtos;
using Microsoft.Extensions.Configuration;

namespace Leaguelane.Service.Services
{
    public class S3FileStorageService: IFileStorageService
    {
        private readonly IAmazonS3 _s3;
        private readonly string _bucket;
        public S3FileStorageService(IConfiguration config)
        {
            _bucket = config["Storage:Bucket"]
            ?? throw new ArgumentNullException("Storage:Bucket configuration is missing.");

            var serviceUrl = config["Storage:Endpoint"];
            var accessKey = config["Storage:AccessKey"];
            var secretKey = config["Storage:SecretKey"];

            var s3Config = new AmazonS3Config
            {
                ServiceURL = serviceUrl,
                ForcePathStyle = true // Essential for MinIO and R2
            };

            _s3 = new AmazonS3Client(accessKey, secretKey, s3Config);

            SetBucketPublicAsync().GetAwaiter().GetResult();
        }

        public async Task<FileUploadResult> UploadAsync(
        string path,
        Stream content,
        string contentType,
        CancellationToken ct = default)
        {
            var request = new PutObjectRequest
            {
                BucketName = _bucket,
                Key = path,
                InputStream = content,
                ContentType = contentType,
                AutoCloseStream = false,
            };

            // --- LOGIC TO PREVENT KMS ERROR ---
            // Check if we are running against localhost (MinIO)
            var isLocal = _s3.Config.ServiceURL.Contains("localhost") ||
                          _s3.Config.ServiceURL.Contains("127.0.0.1");

            if (!isLocal)
            {
                // AWS S3 handles this automatically, MinIO requires KMS config
                request.ServerSideEncryptionMethod = ServerSideEncryptionMethod.AES256;
            }
            // ----------------------------------

            var uploadData = await _s3.PutObjectAsync(request, ct);

            return new FileUploadResult
            {
                Path = path,
                FileName = Path.GetFileName(path),
                SizeBytes = content.CanSeek ? content.Length : 0,
                ContentType = contentType,
                UploadedAt = DateTime.UtcNow,
                Url = _s3.Config.ServiceURL + _bucket + "/" + path
            };
        }

        private async Task SetBucketPublicAsync()
        {
            var policyJson = $@"{{
                                    ""Version"": ""2012-10-17"",
                                    ""Statement"": [{{
                                        ""Effect"": ""Allow"",
                                        ""Principal"": {{""AWS"": [""*""]}},
                                        ""Action"": [""s3:GetObject""],
                                        ""Resource"": [""arn:aws:s3:::{_bucket}/*""]
                                    }}]
                                }}";

            try
            {
                await _s3.PutBucketPolicyAsync(new PutBucketPolicyRequest
                {
                    BucketName = _bucket,
                    Policy = policyJson
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Could not set bucket policy: {ex.Message}");
            }
        }
    }
}
