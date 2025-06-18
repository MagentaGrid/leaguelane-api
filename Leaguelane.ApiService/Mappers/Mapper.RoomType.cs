using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;

namespace Leaguelane.Api.Mappers
{
    public static class RoomTypeMapper
    {
        public static List<RoomTypeDto> MapRoomTypeToRoomTypeResponseDto(List<RoomType> roomTypes)
        {
            return roomTypes.Select(roomType => new RoomTypeDto
            {
                BaseOccupancy = roomType.BaseOccupancy,
                MaxOccupancy = roomType.MaxOccupancy,
                Name = roomType.Name,
                MaxAdults = roomType.MaxAdults,
                MaxChildren = roomType.MaxChildren,
                RoomTypeId = roomType.RoomTypeID,
                Price = roomType.Price,
            }).ToList();
        }
        public static RoomType MapDtoToRoomType(RoomTypeDto roomTypeDto)
        {
            return new RoomType
            {
                Name = roomTypeDto.Name,
                Active = true,
                BaseOccupancy = roomTypeDto.BaseOccupancy,
                MaxAdults = roomTypeDto.MaxAdults,
                MaxChildren = roomTypeDto.MaxChildren,
                MaxOccupancy = roomTypeDto.MaxOccupancy,
                Price = roomTypeDto.Price,
            };
        }
    }
}
