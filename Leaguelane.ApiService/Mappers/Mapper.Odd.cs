using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;

namespace Leaguelane.ApiService.Mappers
{
    public static class OddMapper
    {
        public static List<DropdownOddResponseDto> MapToOddResponseDtos(Odd odd)
        {
            return odd.OddsValues.Select(o => new DropdownOddResponseDto { OddValueId = o.Id, Value = o.Odd, Label = o.Label }).ToList();
        }
    }
}
