using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Text.Json;

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

        public BookmakerService(IBookmakerRepository bookmakerRepository, IConfiguration configuration, IExternalApiErrorService externalApiErrorService)
        {
            _bookmakerRepository = bookmakerRepository;
            _baseUrl = configuration["FootballApi:BaseUrl"];
            _endpoint = configuration["FootballApi:BookmakersEndpoint"];
            _apiHost = configuration["FootballApi:ApiHost"];
            _apiKey = configuration["FootballApi:ApiKey"];
            _externalApiErrorService = externalApiErrorService;
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

        public async Task<List<Bookmaker>> GetAllBookmakersAsync(CancellationToken cancellationToken)
        {
            return await _bookmakerRepository.GetAllBookmakersAsync(cancellationToken);
        }
    }
}
