using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;

namespace Leaguelane.ApiService.Mappers
{
    public static class TipMapper
    {
        public static FixtureTip MapToEntity(TipRequestDto tipDto)
        {
            return new FixtureTip
            {
                FixtureId = tipDto.FixtureId,
                BetId = tipDto.BetId,
                BookmakerId = tipDto.BookmakerId,
                IsSaved = tipDto.IsSaved,
                Active = true,
                IsVisible = tipDto.IsVisible,
                OddsValueId = tipDto.OddsId,
                Reasoning = tipDto.Reasoning,
                Title = tipDto.Title,
            };
        }
    }
}
