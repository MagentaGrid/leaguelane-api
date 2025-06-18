using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;

namespace Leaguelane.Api.Mappers
{
    public static class RoomMapper
    {
        public static Room MapDtoToRoom(RoomDto roomDto)
        {
            return new Room
            {
                RoomNumber = roomDto.RoomNumber,
                Active = true,
                Created = DateTime.UtcNow,
                RoomTypeId = roomDto.RoomTypeId,
                Status = roomDto.Status,
                RoomName = roomDto.RoomName
            };
        }

        public static List<RoomDto> MapRoomToRoomResponseDto(List<Room> rooms)
        {
            return rooms.Select(room => new RoomDto
            {
                RoomNumber = room.RoomNumber,
                Active = room.Active,
                Created = room.Created,
                RoomType = room.RoomType.Name,
                Status = room.Status,
                RoomName = room.RoomName,
                RoomId = room.RoomId
            }).ToList();
        }
    }
}
