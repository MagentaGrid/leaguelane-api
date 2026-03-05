using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;

namespace Leaguelane.Service.Services
{
    public class ExternalApiErrorService: IExternalApiErrorService
    {
        private readonly ILoggingRepository _repository;
        public ExternalApiErrorService(ILoggingRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddExeternalApiErrorAsync(string? ErrorMessage
            , string? RawRequest
            , string? RawResponse
            , DateTime DateTime
            , string Method
            , string Url
            , CancellationToken cancellationToken)
        {
            var exeternalApiError = new ExternalApiError
            {
                ErrorMessage = ErrorMessage,
                RawRequest = RawRequest,
                RawResponse = RawResponse,
                DateTime = DateTime,
                Method = Method,
                Url = Url
            };

            await _repository.AddAsync<ExternalApiError>(exeternalApiError, cancellationToken);
            await _repository.SaveChangesAsync<ExternalApiError>(cancellationToken);
            return true;
        }
    }
}
