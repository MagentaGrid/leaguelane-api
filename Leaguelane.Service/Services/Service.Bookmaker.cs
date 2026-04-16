using Azure;
using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Leaguelane.Service.Services
{
    public class BookmakerService : IBookmakerService
    {
        private readonly IBookmakerRepository _bookmakerRepository;
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _baseUrl;
        private readonly string _endpoint;
        private readonly string _apiHost;
        private readonly string _apiKey;
        private readonly IExternalApiErrorService _externalApiErrorService;

        private readonly IRepository _repository;

        public BookmakerService(IBookmakerRepository bookmakerRepository
            , IConfiguration configuration
            , IExternalApiErrorService externalApiErrorService
            , IRepository repository)
        {
            _bookmakerRepository = bookmakerRepository;
            _baseUrl = configuration["FootballApi:BaseUrl"];
            _endpoint = configuration["FootballApi:BookmakersEndpoint"];
            _apiHost = configuration["FootballApi:ApiHost"];
            _apiKey = configuration["FootballApi:ApiKey"];
            _externalApiErrorService = externalApiErrorService;
            _repository = repository;
        }

        

        public async Task SoftDeleteBookmakerAsync(int id, CancellationToken cancellationToken)
        {
            await _bookmakerRepository.SoftDeleteBookmakerAsync(id, cancellationToken);
        }

        public async Task RestoreBookmakerAsync(int id, CancellationToken cancellationToken)
        {
            await _bookmakerRepository.RestoreBookmakerAsync(id, cancellationToken);
        }

        public async Task<bool> ImportBookmakersFromApiAsync(CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_baseUrl}/odds/bookmakers"),
            };

            request.Headers.Add("x-rapidapi-host", _apiHost);
            request.Headers.Add("x-rapidapi-key", _apiKey);

            try
            {
                using (var response = await _httpClient.SendAsync(request, cancellationToken))
                {
                    try
                    {
                        response.EnsureSuccessStatusCode();
                        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
                        var data = JsonSerializer.Deserialize<FootballApiBaseResponseDto<List<BookmakerDto>>>(responseBody);

                        if (data != null && data.Response != null && data.Response.Count > 0)
                        {
                            var bookmakers = data.Response.Select(b => new Bookmaker
                            {
                                ApiBookMakerId = (int)b.Id,
                                Name = b.Name,
                                Active = true
                            }).ToList();

                            await _bookmakerRepository.AddBookmakers(bookmakers, cancellationToken);
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        await _externalApiErrorService.AddExeternalApiErrorAsync(ex.Message
                            , request.ToString()
                            , response.ToString()
                            , DateTime.UtcNow
                            , request.Method.Method
                            , request.RequestUri.AbsoluteUri
                            , cancellationToken);
                        throw;
                    }
                }
            }
            catch (HttpRequestException)
            {
                throw;
            }
        }

        public async Task<(int total,List<Bookmaker>)> GetAllBookmakersAsync(int page, int pageSize, string search, CancellationToken cancellationToken)
        {
            var bookmakers = await _repository.GetAllAsync<Bookmaker>();

            return (bookmakers.Count(), bookmakers.OrderBy(x => x.BookmakerId).Skip((page - 1) * pageSize).Take(pageSize).ToList());
        }

        public async Task<List<Bookmaker>> GetAllActiveBookmakersAsync(CancellationToken cancellationToken)
        {
            return await _bookmakerRepository.GetActiveBookmakersAsync(cancellationToken);
        }

        public async Task<bool> EnableBookmakerAsync(int id, CancellationToken cancellationToken)
        {
            var bookmaker = await _repository.GetByIdAsync<Bookmaker>(id, cancellationToken);

            if (bookmaker == null) throw new Exception("Bookmaker not found");
            bookmaker.Active = true;
            await _repository.UpdateAsync(bookmaker);
            await _repository.SaveChangesAsync<Bookmaker>(cancellationToken);

            return true;
        }

        public async Task<bool> DisableBookmakerAsync(int id, CancellationToken cancellationToken)
        {
            var bookmaker = await _repository.GetByIdAsync<Bookmaker>(id, cancellationToken);

            if (bookmaker == null) throw new Exception("Bookmaker not found");
            bookmaker.Active = false;
            await _repository.UpdateAsync(bookmaker);
            await _repository.SaveChangesAsync<Bookmaker>(cancellationToken);

            return true;
        }
    }
}
