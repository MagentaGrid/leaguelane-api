using Leaguelane.Api.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record CreateRoomCommand(RoomDto RoomDto): IRequest<RoomResponse>;
    public class CreateRoomCommandHandler: IRequestHandler<CreateRoomCommand, RoomResponse>
    {
        private readonly IRoomService _roomService;

        public CreateRoomCommandHandler(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public async Task<RoomResponse> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if(await _roomService.IsRoomNumberExists(request.RoomDto.RoomNumber, cancellationToken))
                {
                    return new RoomResponse(false, "Room number already exists", null);
                }

                var room = await _roomService.CreateRoom(RoomMapper.MapDtoToRoom(request.RoomDto), cancellationToken);

                return new RoomResponse(true, "Room created successfully", room);
            }
            catch (Exception ex)
            {
                return new RoomResponse(false, ex.Message, null);
            }
        }
    }
}
