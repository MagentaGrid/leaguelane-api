using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IFixtureRepository _fixtureRepository;
        public OddsService(IOddsRepository oddsRepository, IConfiguration configuration, IFixtureRepository fixtureRepository)
        {
            _oddsRepository = oddsRepository;
            _oddsRepository = oddsRepository;
            _baseUrl = configuration["FootballApi:BaseUrl"] ?? string.Empty;
            _apiHost = configuration["FootballApi:ApiHost"] ?? string.Empty;
            _apiKey = configuration["FootballApi:ApiKey"] ?? string.Empty;
            _fixtureRepository = fixtureRepository;
        }

        public async Task<List<OddsDto>> GetOddsAsync(int? fixtureId, int? bookmakerId, string? market, int skip, int take, bool onlyActive, CancellationToken cancellationToken)
        {
            var odds = await _oddsRepository.GetOddsAsync(fixtureId, bookmakerId, market, skip, take, onlyActive, cancellationToken);
            var result = new List<OddsDto>();
            foreach (var odd in odds)
            {
                var values = await _oddsRepository.GetOddsValuesAsync(odd.Id, cancellationToken);
                // Mapping moved to handler
                result.Add(new OddsDto
                {
                    Id = odd.Id,
                    FixtureId = odd.FixtureId,
                    LeagueId = odd.LeagueId,
                    SeasonId = odd.SeasonId,
                    SportId = odd.SportId,
                    BookmakerId = odd.BookmakerId,
                    BetTypeId = odd.BetTypeId,
                    LastUpdated = odd.LastUpdated,
                    Active = odd.Active ?? true,
                    Values = values?.Select(v => new OddsValueDto { Label = v.Label, Odd = v.Odd }).ToList() ?? new List<OddsValueDto>()
                });
            }
            return result;
        }

        public async Task<List<OddsDto>> GetDeletedOddsAsync(CancellationToken cancellationToken)
        {
            var odds = await _oddsRepository.GetDeletedOddsAsync(cancellationToken);
            var result = new List<OddsDto>();
            foreach (var odd in odds)
            {
                var values = await _oddsRepository.GetOddsValuesAsync(odd.Id, cancellationToken);
                result.Add(new OddsDto
                {
                    Id = odd.Id,
                    FixtureId = odd.FixtureId,
                    LeagueId = odd.LeagueId,
                    SeasonId = odd.SeasonId,
                    SportId = odd.SportId,
                    BookmakerId = odd.BookmakerId,
                    BetTypeId = odd.BetTypeId,
                    LastUpdated = odd.LastUpdated,
                    Active = odd.Active ?? true,
                    Values = values?.Select(v => new OddsValueDto { Label = v.Label, Odd = v.Odd }).ToList() ?? new List<OddsValueDto>()
                });
            }
            return result;
        }

        public async Task<List<OddsDto>> GetAllOddsAsync(CancellationToken cancellationToken)
        {
            var odds = await _oddsRepository.GetAllOddsAsync(cancellationToken);
            var result = new List<OddsDto>();
            foreach (var odd in odds)
            {
                var values = await _oddsRepository.GetOddsValuesAsync(odd.Id, cancellationToken);
                result.Add(new OddsDto
                {
                    Id = odd.Id,
                    FixtureId = odd.FixtureId,
                    LeagueId = odd.LeagueId,
                    SeasonId = odd.SeasonId,
                    SportId = odd.SportId,
                    BookmakerId = odd.BookmakerId,
                    BetTypeId = odd.BetTypeId,
                    LastUpdated = odd.LastUpdated,
                    Active = odd.Active ?? true,
                    Values = values?.Select(v => new OddsValueDto { Label = v.Label, Odd = v.Odd }).ToList() ?? new List<OddsValueDto>()
                });
            }
            return result;
        }

        public async Task<OddsDto> GetOddsByIdAsync(int id, bool onlyActive, CancellationToken cancellationToken)
        {
            var odd = await _oddsRepository.GetOddsByIdAsync(id, onlyActive, cancellationToken);
            if (odd == null) return null;
            var values = await _oddsRepository.GetOddsValuesAsync(odd.Id, cancellationToken);
            return new OddsDto
            {
                Id = odd.Id,
                FixtureId = odd.FixtureId,
                LeagueId = odd.LeagueId,
                SeasonId = odd.SeasonId,
                SportId = odd.SportId,
                BookmakerId = odd.BookmakerId,
                BetTypeId = odd.BetTypeId,
                LastUpdated = odd.LastUpdated,
                Active = odd.Active ?? true,
                Values = values?.Select(v => new OddsValueDto { Label = v.Label, Odd = v.Odd }).ToList() ?? new List<OddsValueDto>()
            };
        }

        public async Task UpdateOddsAsync(Odd odd, List<OddsValue> values, CancellationToken cancellationToken)
        {
            await _oddsRepository.UpdateOddsAsync(odd, values, cancellationToken);
        }

        public async Task SoftDeleteOddsAsync(int id, CancellationToken cancellationToken)
        {
            await _oddsRepository.SoftDeleteOddsAsync(id, cancellationToken);
        }

        public async Task RestoreOddsAsync(int id, CancellationToken cancellationToken)
        {
            await _oddsRepository.RestoreOddsAsync(id, cancellationToken);
        }

        public async Task FetchOddsAsync(CancellationToken cancellationToken)
        {
            var fixtures = await _fixtureRepository.GetFixturesForNextFourteenDaysAsync(cancellationToken);

            foreach (var fixture in fixtures)
            {
                await FetchAndStoreOddsAsync(fixture, cancellationToken);
            }
        }

        public async Task FetchAndStoreOddsAsync(Persistence.Entities.Fixture fixture, CancellationToken cancellationToken)
        {

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
                                LeagueId = fixture.LeagueId,
                                SeasonId = fixture.SeasonId,
                                SportId = fixture.SportId ?? 0,
                                BookmakerId = bookmaker.Id,
                                BetTypeId = bet.Id,
                                LastUpdated = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                                Created = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
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
