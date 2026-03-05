namespace Leaguelane.Service.Services
{
    public interface IExternalApiErrorService
    {
        Task<bool> AddExeternalApiErrorAsync(string? ErrorMessage
            , string? RawRequest
            , string? RawResponse
            , DateTime DateTime
            , string Method
            , string Url
            , CancellationToken cancellationToken);
    }
}
