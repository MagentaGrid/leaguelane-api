using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record UpdateBookingCommand(BookingDto Booking, int Id) : IRequest<BookingResponse>;
    public class UpdateBookingCommandHandler: IRequestHandler<UpdateBookingCommand, BookingResponse>
    {
        private readonly IBookingService _bookingService;

        public UpdateBookingCommandHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        public async Task<BookingResponse> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingBooking = await _bookingService.GetBookingById(request.Id, cancellationToken);
                if (existingBooking != null)
                {
                    existingBooking.Status = request.Booking.Status;
                    existingBooking.CheckOutDate = request.Booking.CheckOutDate;
                    existingBooking.CheckInDate = request.Booking.CheckInDate;
                    existingBooking.TotalGuests = request.Booking.TotalGuests;
                    existingBooking.Updated = DateTime.UtcNow;

                    var result = await _bookingService.UpdateBooking(existingBooking, cancellationToken);
                    return new BookingResponse(true, "Updated successfully", result);
                }

                return new BookingResponse(false, "Booking not found", null);
            }
            catch (Exception ex) 
            {
                return new BookingResponse(false, ex.Message, null);
            }
        }
    }
}
