using Leaguelane.Api.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.ApiService.Handlers;

public record GetRoomsQuery() : IRequest<RoomsResponse>;

public class GetRoomsQueryHandler : IRequestHandler<GetRoomsQuery, RoomsResponse>
{
    private readonly IRoomService _roomService;
    public GetRoomsQueryHandler(IRoomService roomService)
    {
        _roomService = roomService;
    }

    public async Task<RoomsResponse> Handle(GetRoomsQuery query, CancellationToken cancellationToken)
    {
        var roomList = await _roomService.GetAllRooms(cancellationToken);
        var roomsResponse = new RoomsResponse
        {
            ErrorMessage = null,
            IsSuccess = true,

            Rooms = RoomMapper.MapRoomToRoomResponseDto(roomList)
        };

        return roomsResponse;
    }
}