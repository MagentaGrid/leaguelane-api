using Leaguelane.Models.Dtos;
using Leaguelane.Repository.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public class BetService : IBetService
    {
        private readonly IBetRepository _betRepository;
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _baseUrl;
        private readonly string _endpoint;
        private readonly string _apiHost;
        private readonly string _apiKey;
        private readonly IRepository _repository;
        private readonly IExternalApiErrorService _externalApiErrorService;

        public BetService(IBetRepository betRepository, IConfiguration configuration, IRepository repository, IExternalApiErrorService externalApiErrorService)
        {
            _betRepository = betRepository;
            _baseUrl = configuration["FootballApi:BaseUrl"];
            _endpoint = "/odds/bets"; // or configuration["FootballApi:BetsEndpoint"];
            _apiHost = configuration["FootballApi:ApiHost"];
            _apiKey = configuration["FootballApi:ApiKey"];
            _repository = repository;
            _externalApiErrorService = externalApiErrorService;
        }

        public async Task<bool> GetAllBetsAsync(CancellationToken cancellationToken)
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
                    try
                    {
                        response.EnsureSuccessStatusCode();
                        var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
                        var data = JsonSerializer.Deserialize<FootballApiBaseResponseDto<List<BetDto>>>(responseBody);

                        if (data != null && data.Response != null && data.Response.Count > 0)
                        {
                            var bets = data.Response.Select(b => new Leaguelane.Persistence.Entities.Bet
                            {
                                ApiBetId = b.Id,
                                Name = b.Name,
                                Active = true,
                                Created = DateTime.UtcNow
                            }).ToList();
                            await _betRepository.AddBets(bets, cancellationToken);
                        }
                        return true;
                    }catch (Exception ex)
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
            }
            catch (HttpRequestException)
            {
                throw;
            }
        }

        public async Task<List<Leaguelane.Persistence.Entities.Bet>> GetAllBets(CancellationToken cancellationToken)
        {
            return (await _repository.GetAllAsync<Leaguelane.Persistence.Entities.Bet>()).ToList(); 
        }
    }
}
