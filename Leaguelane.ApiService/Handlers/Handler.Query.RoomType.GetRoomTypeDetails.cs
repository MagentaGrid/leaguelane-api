using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record GetRoomTypeDetailsQuery(int RoomTypeId) : IRequest<RoomTypeResponse>;
    public class GetRoomTypeDetailsQueryHandler : IRequestHandler<GetRoomTypeDetailsQuery, RoomTypeResponse>
    {
        private readonly IRoomTypeService _roomTypeService;

        public GetRoomTypeDetailsQueryHandler(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }

        public async Task<RoomTypeResponse> Handle(GetRoomTypeDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roomTypeService.GetRoomTypeById(request.RoomTypeId, cancellationToken);

                if (result == null) 
                {
                    return new RoomTypeResponse(false, "Room Type not found", null);
                }

                return new RoomTypeResponse(true, "Room Type details fetched successfully", result);
            }
            catch (Exception ex) 
            {
                return new RoomTypeResponse(false, ex.Message, null);
            }
        }
    }
}
