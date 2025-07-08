using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Leaguelane.Service.Services
{
    public class CountryService: ICountryService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly ICountryRepository _countryRepository;

        private readonly string _baseUrl;
        private readonly string _endpoint;
        private readonly string _apiHost;
        private readonly string _apiKey;
        public CountryService(IConfiguration configuration, ICountryRepository countryRepository)
        {
            _baseUrl = configuration["FootballApi:BaseUrl"];
            _endpoint = configuration["FootballApi:SeasonsEndpoint"];
            _apiHost = configuration["FootballApi:ApiHost"];
            _apiKey = configuration["FootballApi:ApiKey"];
            _countryRepository = countryRepository;
        }
        public async Task<bool> GetAllCountriesAsync(CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_baseUrl}countries"),
            };

            request.Headers.Add("x-rapidapi-host", _apiHost);
            request.Headers.Add("x-rapidapi-key", _apiKey);

            try
            {
                using (var response = await _httpClient.SendAsync(request, cancellationToken))
                {
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

                    var data = JsonSerializer.Deserialize<FootballApiBaseResponseDto<List<CountryDto>>>(responseBody);

                    var countries = data.Response.Select(c => new Country 
                    { 
                        Name = c.Name, 
                        Active = true,
                        Code = c.Code,
                        Created = DateTime.UtcNow,
                        FlagUrl = c.Flag
                    }).ToList();

                    await _countryRepository.AddCountries(countries, cancellationToken);

                    return true;
                }
            }
            catch (HttpRequestException ex)
            {
                // Log exception here if needed
                return false;
            }
        }

    }
}
