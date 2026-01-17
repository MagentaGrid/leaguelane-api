using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Text.Json;

namespace Leaguelane.Service.Services
{
    public class BookmakerService : IBookmakerService
    {
        private readonly IBookmakerRepository _bookmakerRepository;
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _baseUrl;
        private readonly string _endpoint;
        private readonly string _apiHost;
        private readonly string _apiKey;

        public BookmakerService(IBookmakerRepository bookmakerRepository, IConfiguration configuration)
        {
            _bookmakerRepository = bookmakerRepository;
            _baseUrl = configuration["FootballApi:BaseUrl"];
            _endpoint = configuration["FootballApi:BookmakersEndpoint"];
            _apiHost = configuration["FootballApi:ApiHost"];
            _apiKey = configuration["FootballApi:ApiKey"];
        }

        public async Task<List<BookmakerDto>> GetActiveBookmakersAsync(CancellationToken cancellationToken)
        {
            var entities = await _bookmakerRepository.GetActiveBookmakersAsync(cancellationToken);
            return entities.Select(MapToDto).ToList();
        }

        public async Task<List<BookmakerDto>> GetAllBookmakersAsync(CancellationToken cancellationToken)
        {
            var entities = await _bookmakerRepository.GetAllBookmakersAsync(cancellationToken);
            return entities.Select(MapToDto).ToList();
        }

        public async Task<BookmakerDto?> GetBookmakerByIdAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await _bookmakerRepository.GetBookmakerByIdAsync(id, cancellationToken);
            return entity == null ? null : MapToDto(entity);
        }

        public async Task<BookmakerDto> CreateBookmakerAsync(BookmakerDto bookmaker, CancellationToken cancellationToken)
        {
            var entity = MapToEntity(bookmaker);
            var created = await _bookmakerRepository.CreateBookmakerAsync(entity, cancellationToken);
            return MapToDto(created);
        }

        public async Task<BookmakerDto> UpdateBookmakerAsync(BookmakerDto bookmaker, CancellationToken cancellationToken)
        {
            var entity = MapToEntity(bookmaker);
            var updated = await _bookmakerRepository.UpdateBookmakerAsync(entity, cancellationToken);
            return MapToDto(updated);
        }

        public async Task SoftDeleteBookmakerAsync(int id, CancellationToken cancellationToken)
        {
            await _bookmakerRepository.SoftDeleteBookmakerAsync(id, cancellationToken);
        }

        public async Task RestoreBookmakerAsync(int id, CancellationToken cancellationToken)
        {
            await _bookmakerRepository.RestoreBookmakerAsync(id, cancellationToken);
        }

        public async Task<List<BookmakerDto>> GetDeletedBookmakersAsync(CancellationToken cancellationToken)
        {
            var entities = await _bookmakerRepository.GetDeletedBookmakersAsync(cancellationToken);
            return entities.Select(MapToDto).ToList();
        }

        public async Task<bool> ImportBookmakersFromApiAsync(CancellationToken cancellationToken)
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
                    response.EnsureSuccessStatusCode();
                    var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
                    var data = JsonSerializer.Deserialize<FootballApiBaseResponseDto<List<BookmakerDto>>>(responseBody);

                    if (data != null && data.Response != null && data.Response.Count > 0)
                    {
                        var bookmakerNames = data.Response.Select(b => b.Name).ToList();
                        await _bookmakerRepository.AddBookmakers(bookmakerNames, cancellationToken);
                    }
                    return true;
                }
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        private BookmakerDto MapToDto(Bookmaker entity)
        {
            return new BookmakerDto
            {
                Id = entity.BookmakerId,
                ApiBookMakerId = entity.ApiBookMakerId,
                Name = entity.Name,
                AffiliateLink = entity.AffiliateLink,
                BookieLogo = entity.BookieLogo,
                Active = entity.Active
            };
        }

        private Bookmaker MapToEntity(BookmakerDto dto)
        {
            return new Bookmaker
            {
                BookmakerId = dto.Id,
                ApiBookMakerId = (int)dto.ApiBookMakerId,
                Name = dto.Name,
                AffiliateLink = dto.AffiliateLink,
                BookieLogo = dto.BookieLogo,
                Active = dto.Active
            };
        }
    }
}
