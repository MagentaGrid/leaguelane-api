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
    public class TeamStatService : ITeamStatService
    {
        private readonly ITeamStatRepository _teamStatRepository;
        private readonly IExternalApiErrorService _externalApiErrorService;
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _baseUrl;
        private readonly string _apiHost;
        private readonly string _apiKey;

        public TeamStatService(ITeamStatRepository teamStatRepository, IConfiguration configuration, IExternalApiErrorService externalApiErrorService)
        {
            _teamStatRepository = teamStatRepository;
            _baseUrl = configuration["FootballApi:BaseUrl"] ?? string.Empty;
            _apiHost = configuration["FootballApi:ApiHost"] ?? string.Empty;
            _apiKey = configuration["FootballApi:ApiKey"] ?? string.Empty;
            _externalApiErrorService = externalApiErrorService;
        }

        public async Task FetchAndStoreTeamStatsAsync(int leagueId, int teamId, int seasonId, int sportId, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_baseUrl}teams/statistics?league={leagueId}&team={teamId}&season={seasonId}")
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
                    var apiResponse = JsonSerializer.Deserialize<FootballApiBaseResponseDto<TeamStatApiResponseDto>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (apiResponse?.Response != null)
                    {
                        var stat = new TeamStat
                        {
                            TeamId = teamId,
                            LeagueId = leagueId,
                            SeasonId = seasonId,
                            SportId = sportId,
                            Created = DateTime.UtcNow,
                            Active = true
                        };
                        int statId = await _teamStatRepository.AddOrUpdateTeamStatAsync(stat, cancellationToken);

                        var fixtures = new List<TeamStatFixture>();
                        foreach (var kv in apiResponse.Response.Fixtures)
                        {
                            fixtures.Add(new TeamStatFixture
                            {
                                TeamStatsId = statId,
                                ResultType = Enum.Parse<TeamStatsResultType>(kv.Key, true),
                                Home = kv.Value.Home,
                                Away = kv.Value.Away,
                                Total = kv.Value.Total
                            });
                        }
                        await _teamStatRepository.AddOrUpdateTeamStatFixturesAsync(fixtures, cancellationToken);

                        var goals = new List<TeamStatGoal>();
                        foreach (var type in new[] { "for", "against" })
                        {
                            var goalType = Enum.Parse<TeamStatsGoalType>(type == "for" ? "for" : "against", true);
                            foreach (var metric in new[] { "total", "average" })
                            {
                                var goalMetric = Enum.Parse<TeamStatsGoalMetric>(metric, true);
                                var goalData = type == "for" ? apiResponse.Response.Goals.For : apiResponse.Response.Goals.Against;
                                var metricData = metric == "total"
                                    ? goalData.Total
                                    : new TeamStatGoalMetricTotalDto
                                    {
                                        Home = string.IsNullOrEmpty(goalData.Average.Home) ? 0 : decimal.Parse(goalData.Average.Home),
                                        Away = string.IsNullOrEmpty(goalData.Average.Away) ? 0 : decimal.Parse(goalData.Average.Away),
                                        Total = string.IsNullOrEmpty(goalData.Average.Total) ? 0 : decimal.Parse(goalData.Average.Total),
                                    };
                                goals.Add(new TeamStatGoal
                                {
                                    TeamStatsId = statId,
                                    Type = goalType,
                                    Metric = goalMetric,
                                    Home = metricData.Home,
                                    Away = metricData.Away,
                                    Total = metricData.Total,
                                });
                            }
                        }
                        await _teamStatRepository.AddOrUpdateTeamStatGoalsAsync(goals, cancellationToken);
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
    }
}
