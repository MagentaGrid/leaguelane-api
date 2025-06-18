using Leaguelane.Api.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record GetBookingsQuery(): IRequest<BookingsResponse>;

    public class GetBookingsQueryHandler: IRequestHandler<GetBookingsQuery, BookingsResponse>
    {
        private readonly IBookingService _bookingService;

        public GetBookingsQueryHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<BookingsResponse> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
        {
            var result = await _bookingService.GetAllBookings(cancellationToken);
            return new BookingsResponse
            {
                IsSuccess = true,
                ErrorMessage = "Bookings fetched successfully",
                Bookings = BookingMapper.MapBookingToBookingResponseDto(result)
            };
        }
    }
}
