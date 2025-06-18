using Leaguelane.Api.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record CreateRoomTypeCommand(RoomTypeDto roomTypeDto) : IRequest<RoomTypeResponse>;
    public class CreateRoomTypeCommandHandler : IRequestHandler<CreateRoomTypeCommand, RoomTypeResponse>
    {
        private readonly IRoomTypeService _roomTypeService;

        public CreateRoomTypeCommandHandler(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }

        public async Task<RoomTypeResponse> Handle(CreateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if(await _roomTypeService.IsRoomTypeNameExists(request.roomTypeDto.Name, cancellationToken))
                {
                    return new RoomTypeResponse(false, "Room type name already exists", null);
                }

                var roomType = await _roomTypeService.CreateRoomType(RoomTypeMapper.MapDtoToRoomType(request.roomTypeDto), cancellationToken);
                return new RoomTypeResponse(true, "Room type created successfully", roomType);
            }
            catch (Exception ex) 
            {
                return new RoomTypeResponse(false, ex.Message, null);
            }
        }
    }
}
