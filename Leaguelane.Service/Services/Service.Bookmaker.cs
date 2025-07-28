using Leaguelane.Models.Dtos;
using Leaguelane.Repository.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

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

        public BookmakerService(IBookmakerRepository bookmakerRepository, IConfiguration configuration)
        {
            _bookmakerRepository = bookmakerRepository;
            _baseUrl = configuration["FootballApi:BaseUrl"];
            _endpoint = configuration["FootballApi:BookmakersEndpoint"];
            _apiHost = configuration["FootballApi:ApiHost"];
            _apiKey = configuration["FootballApi:ApiKey"];
        }

        public async Task<bool> GetAllBookmakersAsync(CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_baseUrl}{_endpoint}"),
            };

            request.Headers.Add("x-rapidapi-host", _apiHost);
            request.Headers.Add("x-rapidapi-key", _apiKey);

            try
            {
                using (var response = await _httpClient.SendAsync(request, cancellationToken))
                {
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
                    var data = JsonSerializer.Deserialize<FootballApiBaseResponseDto<List<BookmakerDto>>>(responseBody);

                    if (data != null && data.Response != null && data.Response.Count > 0)
                    {
                        var bookmakerNames = data.Response.Select(b => b.Name).ToList();
                        await _bookmakerRepository.AddBookmakers(bookmakerNames, cancellationToken);
                    }
                    return true;
                }
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }
    }
}
