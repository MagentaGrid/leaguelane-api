using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record UpdateRoomCommand(RoomDto room, int id) : IRequest<RoomResponse>;
    public class UpdateRoomCommandHandler: IRequestHandler<UpdateRoomCommand, RoomResponse>
    {
        private readonly IRoomService _roomService;

        public UpdateRoomCommandHandler(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public async Task<RoomResponse> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var room = await _roomService.GetRoomById(request.id, cancellationToken);
                if (room == null)
                {
                    return new RoomResponse(false, "Room not found", null);
                }

                room.RoomTypeId = request.room.RoomTypeId;
                room.Status = request.room.Status;
                room.RoomNumber = request.room.RoomNumber;
                room.Active = request.room.Active;
                room.Updated= DateTime.UtcNow;
                room.RoomName = request.room.RoomName;

                var updatedRoom = await _roomService.UpdateRoom(room, cancellationToken);
                return new RoomResponse(true, "Room updated successfully", updatedRoom);
            }
            catch (Exception ex)
            {
                return new RoomResponse(false, ex.Message, null);
            }
        }
    }
}
