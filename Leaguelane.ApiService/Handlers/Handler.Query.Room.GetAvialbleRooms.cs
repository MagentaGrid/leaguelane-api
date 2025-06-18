using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using Leaguelane.Service.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Leaguelane.Api.Handlers;
public record GetAvailableRoomsQuery(BookingAvailabilityDto bookingAvilabilityDto) : IRequest<List<BookingAvailabilityResponse>>;
public class GetAvailableRoomsQueryHandler : IRequestHandler<GetAvailableRoomsQuery, List<BookingAvailabilityResponse>>
{
    private readonly IBookingService _bookingService;
    private readonly IRoomService _roomService;

    public GetAvailableRoomsQueryHandler( IBookingService bookingService, IRoomService roomService)
    {
        _bookingService = bookingService;
        _roomService = roomService;
    }

    public async Task<List<BookingAvailabilityResponse>> Handle(GetAvailableRoomsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var availableRooms = await _roomService.GetAvailableRooms(request.bookingAvilabilityDto.CheckInDate, request.bookingAvilabilityDto.CheckOutDate, cancellationToken);

            return availableRooms.Select(room => new BookingAvailabilityResponse
            {
                RoomId = room.RoomId,
                RoomNumber = room.RoomNumber,
                RoomName = room.RoomName,
                RoomTypeId = room.RoomTypeId
            }).ToList();
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}


