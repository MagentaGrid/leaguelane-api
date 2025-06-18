using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record GetRoomDetailsQuery(int RoomId): IRequest<RoomResponse>;
    public class GetRoomDetailsQueryHandler : IRequestHandler<GetRoomDetailsQuery, RoomResponse>
    {
        private readonly IRoomService _roomService;

        public GetRoomDetailsQueryHandler(IRoomService roomService)
        {
            _roomService = roomService;
        }

        public async Task<RoomResponse> Handle(GetRoomDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _roomService.GetRoomById(request.RoomId, cancellationToken);
                if (result != null)
                {
                    return new RoomResponse(true, "Room details fetched successfully", result);
                }

                return new RoomResponse(false, "Room not found", null);
            }
            catch (Exception ex)
            {
                return new RoomResponse(false, ex.Message, null);
            }
        }
    }
}
