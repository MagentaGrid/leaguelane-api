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
using PersistenceTeam = Leaguelane.Persistence.Entities.Team;
using PersistenceVenue = Leaguelane.Persistence.Entities.Venue;
using DtoTeam = Leaguelane.Models.Dtos.TeamApiDto;
using DtoVenue = Leaguelane.Models.Dtos.VenueApiDto;

namespace Leaguelane.Service.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IVenueRepository _venueRepository;
        private readonly ILeagueRepository _leagueRepository;
        private readonly ISeasonRepository _seasonRepository;
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _baseUrl;
        private readonly string _apiHost;
        private readonly string _apiKey;

        public TeamService(ITeamRepository teamRepository, IVenueRepository venueRepository, IConfiguration configuration, ISeasonRepository seasonRepository, ILeagueRepository leagueRepository)
        {
            _teamRepository = teamRepository;
            _venueRepository = venueRepository;
            _baseUrl = configuration["FootballApi:BaseUrl"] ?? string.Empty;
            _apiHost = configuration["FootballApi:ApiHost"] ?? string.Empty;
            _apiKey = configuration["FootballApi:ApiKey"] ?? string.Empty;
            _seasonRepository = seasonRepository;
            _leagueRepository = leagueRepository;
        }

        public async Task FetchAndStoreTeamsAndVenuesAsync(int leagueId, int seasonId, int sportId, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_baseUrl}teams?league={leagueId}&season={seasonId}")
            };
            request.Headers.Add("x-rapidapi-host", _apiHost);
            request.Headers.Add("x-rapidapi-key", _apiKey);

            using var response = await _httpClient.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
            var apiResponse = JsonSerializer.Deserialize<FootballApiBaseResponseDto<List<TeamApiResponseDto>>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var teams = new List<PersistenceTeam>();
            var venues = new List<PersistenceVenue>();
            if (apiResponse?.Response != null && apiResponse?.Response?.Count > 0)
            {
                foreach (var item in apiResponse.Response)
                {
                    var team = new PersistenceTeam
                    {
                        ApiTeamId = item.Team.Id,
                        Name = item.Team.Name,
                        Code = item.Team.Code,
                        Country = item.Team.Country,
                        Founded = item.Team.Founded,
                        National = item.Team.National,
                        LogoUrl = item.Team.Logo,
                        SportId = sportId,
                        LeagueId = leagueId,
                        SeasonId = seasonId,
                        Created = DateTime.UtcNow,
                        Active = true
                    };
                    teams.Add(team);

                    if (item.Venue != null && item?.Venue.Id != null)
                    {
                        venues.Add(new PersistenceVenue
                        {
                            ApiVenueId = item.Venue.Id ?? 0,
                            Name = item.Venue.Name ?? "",
                            Address = item.Venue.Address,
                            City = item.Venue.City,
                            Capacity = item.Venue.Capacity,
                            Surface = item.Venue.Surface,
                            ImageUrl = item.Venue.Image,
                            SportId = sportId,
                            LeagueId = leagueId,
                            SeasonId = seasonId,
                            TeamId = team.Id, // Will be set after team is saved if needed
                            Created = DateTime.UtcNow,
                            Active = true
                        });
                    }
                }
                await _teamRepository.AddOrUpdateTeamsAsync(teams, cancellationToken);
                // After teams are saved, update TeamId in venues if needed
                foreach (var venue in venues)
                {
                    var team = teams.FirstOrDefault(t => t.ApiTeamId == venue.ApiVenueId);
                    if (team != null)
                        venue.TeamId = team.Id;
                }
                await _venueRepository.AddOrUpdateVenuesAsync(venues, cancellationToken);
            }
        }

        public async Task ImportAllTeams(CancellationToken cancellationToken)
        {
            var leagues = await _leagueRepository.GetAllActiveLeagues(cancellationToken);
            var seasons = await _seasonRepository.GetAllSeasons(cancellationToken);
            foreach (var league in leagues)
            {
                foreach (var season in seasons)
                {
                    await FetchAndStoreTeamsAndVenuesAsync(league.ApiLeagueId, season.Year, 1, cancellationToken);
                }
            }
        }

        public async Task<Dictionary<int, PersistenceTeam>> GetAllTeamsById(IEnumerable<int> teamIds, CancellationToken cancellationToken)
        {
            return await _teamRepository.GetAllTeamsById(teamIds, cancellationToken);
        }
    }
}
