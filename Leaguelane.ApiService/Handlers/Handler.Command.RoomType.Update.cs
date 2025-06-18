using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record UpdateRoomTypeCommand(RoomTypeDto roomTypeDto, int id) : IRequest<RoomTypeResponse>;
    public class UpdateRoomTypeCommandHandler : IRequestHandler<UpdateRoomTypeCommand, RoomTypeResponse>
    {
        private readonly IRoomTypeService _roomTypeService;

        public UpdateRoomTypeCommandHandler(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }

        public async Task<RoomTypeResponse> Handle(UpdateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if(await _roomTypeService.IsRoomTypeNameExistsForUpdate(request.roomTypeDto.Name, request.id, cancellationToken))
                {
                    return new RoomTypeResponse(false, "Room type name already exists", null);
                }

                var roomType = await _roomTypeService.GetRoomTypeById(request.id, cancellationToken);   

                if (roomType == null)
                {
                    return new RoomTypeResponse(false, "Room type not found", null);
                }

                roomType.Name = request.roomTypeDto.Name;
                roomType.MaxOccupancy = request.roomTypeDto.MaxOccupancy;
                roomType.Price = request.roomTypeDto.Price;
                roomType.BaseOccupancy = request.roomTypeDto.BaseOccupancy;
                roomType.MaxAdults = request.roomTypeDto.MaxAdults;
                roomType.MaxChildren = request.roomTypeDto.MaxChildren;
                roomType.Active = request.roomTypeDto.Active;

                var result = await _roomTypeService.UpdateRoomType(roomType, cancellationToken);
                return new RoomTypeResponse(true, "Room type updated successfully", result);
            }
            catch (Exception ex) 
            {
                return new RoomTypeResponse(false, ex.Message, null);
            }
        }
    }
}
