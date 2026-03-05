using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public class LeagueService: ILeagueService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly ILeagueRepository _leagueRepository;
        private readonly IRepository _repository;
        private readonly IExternalApiErrorService _externalApiErrorService;

        private readonly string _baseUrl;
        private readonly string _apiHost;
        private readonly string _apiKey;

        public LeagueService(IConfiguration configuration, ILeagueRepository leagueRepository, IRepository repository, IExternalApiErrorService externalApiErrorService)
        {
            _baseUrl = configuration["FootballApi:BaseUrl"] ?? throw new ArgumentNullException("BaseUrl");
            _apiHost = configuration["FootballApi:ApiHost"] ?? throw new ArgumentNullException("ApiHost");
            _apiKey = configuration["FootballApi:ApiKey"] ?? throw new ArgumentNullException("ApiKey");

            _leagueRepository = leagueRepository;
            _repository = repository;
            _externalApiErrorService = externalApiErrorService;
        }

        public async Task<bool> GetAllLeaguesAsync(CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_baseUrl}leagues"),
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

                    var data = JsonSerializer.Deserialize<FootballApiBaseResponseDto<List<LeagueResponseDto>>>(
                        responseBody,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (data != null && data.Response != null && data.Response.Count > 0)
                    {
                        var leagues = data.Response.Select(l => new League
                        {
                            Name = l.League.Name,
                            Active = true,
                            Created = DateTime.UtcNow,
                            LogoUrl = l.League.Logo,
                            ApiLeagueId = l.League.Id,
                            Type = l.League.Type,
                            CurrentSeason = l.Seasons.Where(s => s.Current == true).Select(s => s.Year).FirstOrDefault(),
                            CountryCode = l.Country.Code,
                        }).ToList();

                        await _leagueRepository.AddLeagues(leagues, cancellationToken);
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
                    return false;
                }

                return true;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (JsonException ex)
            {
                throw ex;
            }
        }

        public async Task<List<League>> GetAllActiveLeaguesByIds(List<int> ids, CancellationToken cancellationToken)
        {
            return (await _repository.FindAllAsync<League>(x => ids.Contains(x.ApiLeagueId), cancellationToken)).ToList();
        }

        public async Task<League> GetLeagueByApiIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _repository.FirstOrDefaultAsync<League>(x => x.ApiLeagueId == id, cancellationToken);
        }
    }
}
