using Leaguelane.Models.Dtos;
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
    public class SeasonService: ISeasonService
    {
        private readonly ISeasonRepository _seasonRepository;
        private readonly IExternalApiErrorService _externalApiErrorService;

        private readonly HttpClient _httpClient = new HttpClient();

        private readonly string _baseUrl;
        private readonly string _endpoint;
        private readonly string _apiHost;
        private readonly string _apiKey;

        public SeasonService(ISeasonRepository seasonRepository, IConfiguration configuration, IExternalApiErrorService externalApiErrorService)
        {
            _seasonRepository = seasonRepository;
            _baseUrl = configuration["FootballApi:BaseUrl"];
            _endpoint = configuration["FootballApi:SeasonsEndpoint"];
            _apiHost = configuration["FootballApi:ApiHost"];
            _apiKey = configuration["FootballApi:ApiKey"];
            _externalApiErrorService = externalApiErrorService;
        }
        public async Task<bool> GetAllSeasons(CancellationToken cancellationToken)
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
                using (var response = await _httpClient.SendAsync(request))
                {
                    try
                    {
                        response.EnsureSuccessStatusCode();
                        var responseBody = await response.Content.ReadAsStringAsync();
                        var data = JsonSerializer.Deserialize<FootballApiBaseResponseDto<List<int>>>(responseBody);

                        if (data != null && data.Response != null && data.Response.Count > 0)
                        {
                            await _seasonRepository.AddSeasons(data.Response, cancellationToken);
                        }
                        return true;
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
            }
            catch (HttpRequestException ex)
            {
                throw;
            }
        }
    }
}
