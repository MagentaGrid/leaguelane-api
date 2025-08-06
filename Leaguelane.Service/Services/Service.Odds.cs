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
    public class OddsService : IOddsService
    {
        private readonly IOddsRepository _oddsRepository;
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _baseUrl;
        private readonly string _apiHost;
        private readonly string _apiKey;

        public OddsService(IOddsRepository oddsRepository, IConfiguration configuration)
        {
            _oddsRepository = oddsRepository;
            _baseUrl = configuration["FootballApi:BaseUrl"] ?? string.Empty;
            _apiHost = configuration["FootballApi:ApiHost"] ?? string.Empty;
            _apiKey = configuration["FootballApi:ApiKey"] ?? string.Empty;
        }

        public async Task FetchAndStoreOddsAsync(int leagueId, int seasonId, int sportId, CancellationToken cancellationToken)
        {
            // TODO: Add logic to get fixtures within the call window (14 to 1 days before fixture date)
            // For each fixture, call the odds API if within window
            // Example: get all fixtures for the league/season
            // This is a placeholder for the actual fixture fetching logic
            var fixtures = new List<Fixture>(); // Should be fetched from DB

            foreach (var fixture in fixtures)
            {
                var today = DateTime.UtcNow.Date;
                var callStart = fixture.Date.AddDays(-14).Date;
                var callEnd = fixture.Date.AddDays(-1).Date;
                if (today < callStart || today > callEnd)
                    continue; // Skip if not in call window

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{_baseUrl}odds?fixture={fixture.FixtureId}")
                };
                request.Headers.Add("x-rapidapi-host", _apiHost);
                request.Headers.Add("x-rapidapi-key", _apiKey);

                using var response = await _httpClient.SendAsync(request, cancellationToken);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
                var apiResponse = JsonSerializer.Deserialize<FootballApiBaseResponseDto<List<OddsApiResponseDto>>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (apiResponse?.Response != null)
                {
                    foreach (var oddsDto in apiResponse.Response)
                    {
                        foreach (var bookmaker in oddsDto.Bookmakers)
                        {
                            foreach (var bet in bookmaker.Bets)
                            {
                                var odd = new Odd
                                {
                                    FixtureId = fixture.FixtureId,
                                    LeagueId = leagueId,
                                    SeasonId = seasonId,
                                    SportId = sportId,
                                    BookmakerId = bookmaker.Id,
                                    BetTypeId = bet.Id,
                                    LastUpdated = DateTime.UtcNow,
                                    Created = DateTime.UtcNow,
                                    Active = true
                                };
                                int oddId = await _oddsRepository.AddOrUpdateOddAsync(odd, cancellationToken);

                                var values = new List<OddsValue>();
                                foreach (var value in bet.Values)
                                {
                                    values.Add(new OddsValue
                                    {
                                        OddsId = oddId,
                                        Label = value.Label,
                                        Odd = value.Odd,
                                        Created = DateTime.UtcNow,
                                        Active = true
                                    });
                                }
                                await _oddsRepository.AddOrUpdateOddsValuesAsync(values, cancellationToken);
                            }
                        }
                    }
                }
            }
        }
    }
}
