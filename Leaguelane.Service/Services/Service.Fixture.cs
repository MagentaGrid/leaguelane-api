using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Context;
using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Leaguelane.Service.Services
{
    public class FixtureService : IFixtureService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private readonly string _baseUrl;
        private readonly string _apiHost;
        private readonly string _apiKey;
        private readonly IFixtureRepository _fixtureRepository;
        private readonly ILeagueRepository _leagueRepository;
        private readonly IRepository _repository;
        private readonly LeaguelaneDbContext _context;
        private readonly IExternalApiErrorService _externalApiErrorService;

        public FixtureService(IConfiguration configuration
            , ILeagueRepository leagueRepository
            , IFixtureRepository fixtureRepository
            , IRepository repository
            , LeaguelaneDbContext context)
        {
            _baseUrl = configuration["FootballApi:BaseUrl"] ?? throw new ArgumentNullException("BaseUrl");
            _apiHost = configuration["FootballApi:ApiHost"] ?? throw new ArgumentNullException("ApiHost");
            _apiKey = configuration["FootballApi:ApiKey"] ?? throw new ArgumentNullException("ApiKey");
            _fixtureRepository = fixtureRepository;
            _leagueRepository = leagueRepository;
            _repository = repository;
            _context = context;
        }
        public async Task GetAllFixtures(CancellationToken cancellationToken)
        {
            var leagues = await _leagueRepository.GetAllActiveLeagues(cancellationToken);

            foreach (var item in leagues)
            {
                await GetAllFixturesByLeagueAndSeason(item.LeagueId, item.CurrentSeason, item.Rank, cancellationToken);
                await Task.Delay(500, cancellationToken); // Rate limiting
            }
        }
        public async Task GetAllFixturesByLeagueAndSeason(int leagueId, int season, int? leagueRank, CancellationToken cancellationToken)
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
                try
                {
                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

                    var fixtures = JsonSerializer.Deserialize<FootballApiBaseResponseDto<List<FixtureResponseDto?>>>(
                        responseBody,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (fixtures != null && fixtures.Response != null && fixtures.Response.Count > 0)
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
                            Rank = leagueRank,

                            Created = DateTime.UtcNow,
                            Active = true,
                            ApiFixtureId = (int)f.Fixture.Id
                        }).ToList();

                        await _fixtureRepository.AddFixturesBatchAsync(fixtureList, cancellationToken);
                    }
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
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<(List<Fixture>, int)> GetAllFixturesAsync(int page, int pagesize, bool publishStatus, CancellationToken cancellationToken)
        {
            var fixtures = await _repository.FindAllAsync<Fixture>(x => x.Date >= DateTime.UtcNow && (!publishStatus || x.PublishStatus), cancellationToken);

            var totalCount = fixtures.Count();

            var data = fixtures
                .Include(x => x.FixturePreviews)
                .Include(x => x.FixtureTips)
                .OrderBy( x => x.Rank)
                .ThenBy(x => x.Date)
                .Skip((page - 1) * pagesize)
                .Take(pagesize).ToList();

            return (data, totalCount);
        }

        public async Task<Fixture> GetFixtureByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Fixtures
                .Include(x => x.FixturePreviews)
                .Include(x => x.FixtureTips)
                    .ThenInclude(t => t.OddsValue)
                .Include(x => x.FixtureTips)
                    .ThenInclude(t => t.Bookmaker)
                .Include(x => x.FixtureTips)
                    .ThenInclude(t => t.Bet)
                .FirstOrDefaultAsync(x => x.FixtureId == id, cancellationToken);
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
                Time = f.Date.HasValue ? f.Date.Value.ToString("HH:mm") : null
            }).ToList();
        }

        public async Task<(List<Fixture>, int)> GetAllFixturesWithPaginationAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            // Get fixtures from now into the future that are published
            var predicate = (Func<Fixture, bool>)(x => x.Date.HasValue && x.Date.Value >= DateTime.UtcNow && x.PublishStatus);

            // Use repository to get IQueryable then count and page
            var fixturesQuery = await _repository.FindAllAsync<Fixture>(x => x.Date.HasValue && x.Date >= DateTime.UtcNow && x.PublishStatus, cancellationToken);

            var totalCount = fixturesQuery.Count();

            var data = fixturesQuery
                .OrderBy(x => x.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (data, totalCount);
        }

        public async Task<int> GetUpcomingFixturesCountAsync(CancellationToken cancellationToken)
        {
            return await _repository.CountAsync<Fixture>(x => x.Date.HasValue && x.Date >= DateTime.UtcNow, cancellationToken);
        }

        public async Task<int> GetMissingTipsCountAsync(CancellationToken cancellationToken)
        {
            return await _repository.CountAsync<Fixture>(x => x.Date.HasValue && x.Date >= DateTime.UtcNow && !x.FixtureTips.Any(), cancellationToken);
        }

        public async Task<int> GetMissingPreviewsCountAsync(CancellationToken cancellationToken)
        {
            return await _repository.CountAsync<Fixture>(x => x.Date.HasValue && x.Date >= DateTime.UtcNow && !x.FixturePreviews.Any(), cancellationToken);
        }

        public async Task<bool> PublishFixture(int fixtureId, CancellationToken cancellationToken)
        {
            var fixture = await _repository.GetByIdAsync<Fixture>(fixtureId, cancellationToken);

            if (fixture == null)
            {
                throw new Exception("Fixture not found");
            }

            fixture.PublishStatus = true;

            _repository.Update(fixture);
            await _repository.SaveChangesAsync<Fixture>(cancellationToken);

            return true;
        }

        public async Task<bool> UnPublishFixture(int fixtureId, CancellationToken cancellationToken)
        {
            var fixture = await _repository.GetByIdAsync<Fixture>(fixtureId, cancellationToken);

            if (fixture == null)
            {
                throw new Exception("Fixture not found");
            }

            fixture.PublishStatus = false;

            _repository.Update(fixture);
            await _repository.SaveChangesAsync<Fixture>(cancellationToken);

            return true;
        }
    }
}
