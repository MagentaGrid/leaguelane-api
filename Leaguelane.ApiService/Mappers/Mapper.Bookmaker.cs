using Leaguelane.Models.Dtos;

namespace Leaguelane.ApiService.Mappers
{
    public static class BookmakerMapper
    {
        public static BookmakerResponseDto MapToDto(Persistence.Entities.Bookmaker bookmaker)
        {
            return new BookmakerResponseDto
            {
                BookmakerId = bookmaker.BookmakerId,
                Name = bookmaker.Name,
                ApiBookmakerId = bookmaker.ApiBookMakerId
            };
        }
    }
}
