using Leaguelane.Models.Dtos;
using Leaguelane.Repository.Repositories;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Leaguelane.Service.Services
{
    public class H2HService : IH2HService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private readonly string _baseUrl;
        private readonly string _apiHost;
        private readonly string _apiKey;
        public H2HService(IConfiguration configuration)
        {
            _baseUrl = configuration["FootballApi:BaseUrl"];
            _apiHost = configuration["FootballApi:ApiHost"];
            _apiKey = configuration["FootballApi:ApiKey"];
        }
        public async Task<List<H2HFixtureResponse>> GetH2H(int homeTeamId, int awayTeamId, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_baseUrl}fixtures/headtohead?h2h={homeTeamId}-{awayTeamId}"),
            };

            request.Headers.Add("x-rapidapi-host", _apiHost);
            request.Headers.Add("x-rapidapi-key", _apiKey);

            using (var response = await _httpClient.SendAsync(request, cancellationToken))
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

                var data = JsonSerializer.Deserialize<FootballApiBaseResponseDto<List<H2HFixtureResponse>>>(responseBody);

                return data?.Response;
            }
        }
    }
}
