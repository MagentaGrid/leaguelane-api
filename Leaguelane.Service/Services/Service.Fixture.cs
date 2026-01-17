using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public class FixtureService: IFixtureService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private readonly string _baseUrl;
        private readonly string _apiHost;
        private readonly string _apiKey;
        private readonly IFixtureRepository _fixtureRepository;
        private readonly ILeagueRepository _leagueRepository;

        public FixtureService(IConfiguration configuration, ILeagueRepository leagueRepository, IFixtureRepository fixtureRepository)
        {
            _baseUrl = configuration["FootballApi:BaseUrl"] ?? throw new ArgumentNullException("BaseUrl");
            _apiHost = configuration["FootballApi:ApiHost"] ?? throw new ArgumentNullException("ApiHost");
            _apiKey = configuration["FootballApi:ApiKey"] ?? throw new ArgumentNullException("ApiKey");
            _fixtureRepository = fixtureRepository;
            _leagueRepository = leagueRepository;
        }
        public async Task GetAllFixtures(CancellationToken cancellationToken)
        {
            var leagues = await _leagueRepository.GetAllActiveLeagues(cancellationToken);

            foreach (var item in leagues)
            {
                await GetAllFixturesByLeagueAndSeason(item.LeagueId, item.CurrentSeason, cancellationToken);
                await Task.Delay(500, cancellationToken); // Rate limiting
            }
        }
        public async Task GetAllFixturesByLeagueAndSeason(int leagueId, int season, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_baseUrl}fixtures?league={leagueId}&season={season}&from={DateTime.UtcNow:yyyy-MM-dd}&to={DateTime.UtcNow.AddDays(3):yyyy-MM-dd}")
            };

            request.Headers.Add("x-rapidapi-host", _apiHost);
            request.Headers.Add("x-rapidapi-key", _apiKey);

            try
            {
                using var response = await _httpClient.SendAsync(request, cancellationToken);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

                var fixtures = JsonSerializer.Deserialize<FootballApiBaseResponseDto<List<FixtureResponseDto?>>>(
                    responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if(fixtures != null && fixtures.Response != null && fixtures.Response.Count > 0)
                {
                    var fixtureList = fixtures.Response.Select(f => new Fixture
                    {
                        //FixtureId = f.Fixture.Id,
                        Timezone = f.Fixture.Timezone,
                        Date = f.Fixture.Date,
                        Time = f.Fixture.Timestamp,

                        VenueId = f.Fixture.Venue.Id,
                        LeagueId = f.League.Id,
                        SeasonId = f.League.Season,
                        RoundId = 7,

                        HomeTeamId = f.Teams.Home.Id,
                        AwayTeamId = f.Teams.Away.Id,
                        GoalsHome = f.Goals.Home,
                        GoalsAway = f.Goals.Away,

                        Created = DateTime.UtcNow,
                        Active = true,
                        ApiFixtureId = (int)f.Fixture.Id
                    }).ToList();

                    await _fixtureRepository.AddFixturesBatchAsync(fixtureList, cancellationToken);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Fixture>> GetAllFixturesAsync(CancellationToken cancellationToken)
        {
            return await _fixtureRepository.GetAllFixturesAsync(cancellationToken);
        }

        public async Task<Fixture> GetFixtureByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _fixtureRepository.GetFixtureByIdAsync(id, cancellationToken);
        }

        public async Task UpdateFixtureAsync(Fixture fixture, CancellationToken cancellationToken)
        {
            await _fixtureRepository.UpdateFixtureAsync(fixture, cancellationToken);
        }

        public async Task SoftDeleteFixtureAsync(int id, CancellationToken cancellationToken)
        {
            await _fixtureRepository.SoftDeleteFixtureAsync(id, cancellationToken);
        }

        public async Task SetRankAsync(int fixtureId, int rank, CancellationToken cancellationToken)
        {
            await _fixtureRepository.SetRankAsync(fixtureId, rank, cancellationToken);
        }

        public async Task<List<FixtureListItemDto>> GetLatestFixturesAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            var fixtures = await _fixtureRepository.GetLatestFixturesAsync(page, pageSize, cancellationToken);
            return fixtures.Select(f => new FixtureListItemDto
            {
                FixtureId = f.FixtureId,
                HomeTeam = new FixtureTeamDto { TeamId = f.HomeTeamId, Name = "", Logo = "" }, // Fill with actual data if available
                AwayTeam = new FixtureTeamDto { TeamId = f.AwayTeamId, Name = "", Logo = "" },
                Date = f.Date.HasValue ? f.Date.Value.ToString("yyyy-MM-dd") : null,
                Time =  f.Date.HasValue ? f.Date.Value.ToString("HH:mm") : null
            }).ToList();
        }
    }
}
