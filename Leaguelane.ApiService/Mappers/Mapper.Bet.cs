using Leaguelane.Models.Dtos;

namespace Leaguelane.ApiService.Mappers
{
    public static class BetMapper
    {
        public static BetResponseDto MapToDto(Persistence.Entities.Bet bet)
        {
            return new BetResponseDto
            {
                Id = bet.Id,
                Name = bet.Name,
                ApiBetId = bet.ApiBetId ?? 0
            };
        }
    }
}
