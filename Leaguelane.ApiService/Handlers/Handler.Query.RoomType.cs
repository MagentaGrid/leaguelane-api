using Leaguelane.Api.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record GetRoomTypesQuery : IRequest<RoomTypesResponse>;
    public class GetRoomTypeQueryHandler: IRequestHandler<GetRoomTypesQuery, RoomTypesResponse>
    {
        private readonly IRoomTypeService _roomTypeService;

        public GetRoomTypeQueryHandler(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }

        public async Task<RoomTypesResponse> Handle(GetRoomTypesQuery request, CancellationToken cancellationToken)
        {
            var result = await _roomTypeService.GetAllRoomTypes(cancellationToken);
            return new RoomTypesResponse
            {
                ErrorMessage = "Room types fetched successfully",
                IsSuccess = true,
                RoomTypes = RoomTypeMapper.MapRoomTypeToRoomTypeResponseDto(result)
            };
        }
    }
}
