using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public class RoundService : IRoundService
    {
        private readonly IRoundRepository _roundRepository;
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _baseUrl;
        private readonly string _apiHost;
        private readonly string _apiKey;

        public RoundService(IRoundRepository roundRepository, IConfiguration configuration)
        {
            _roundRepository = roundRepository;
            _baseUrl = configuration["FootballApi:BaseUrl"] ?? string.Empty;
            _apiHost = configuration["FootballApi:ApiHost"] ?? string.Empty;
            _apiKey = configuration["FootballApi:ApiKey"] ?? string.Empty;
        }

        public async Task FetchAndStoreRoundsAsync(int leagueId, int seasonId, int sportId, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_baseUrl}fixtures/rounds?league={leagueId}&season={seasonId}")
            };
            request.Headers.Add("x-rapidapi-host", _apiHost);
            request.Headers.Add("x-rapidapi-key", _apiKey);

            using var response = await _httpClient.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
            var apiResponse = JsonSerializer.Deserialize<FootballApiBaseResponseDto<List<string>>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var rounds = new List<Round>();
            if (apiResponse?.Response != null)
            {
                foreach (var roundName in apiResponse.Response)
                {
                    rounds.Add(new Round
                    {
                        Name = roundName,
                        LeagueId = leagueId,
                        SeasonId = seasonId,
                        SportId = sportId,
                        Created = DateTime.UtcNow,
                        Active = true
                    });
                }
                await _roundRepository.AddOrUpdateRoundsAsync(rounds, cancellationToken);
            }
        }
    }
}
