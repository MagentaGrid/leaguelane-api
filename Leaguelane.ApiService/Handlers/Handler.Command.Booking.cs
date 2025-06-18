using Leaguelane.Api.Mappers;
using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record CreateBookingCommand(BookingDto BookingDto):IRequest<BookingResponse>;
    public class CreateBookingCommandHandler: IRequestHandler<CreateBookingCommand, BookingResponse>
    {
        private readonly IBookingService _bookingService;

        public CreateBookingCommandHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<BookingResponse> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if(await _bookingService.IsRoomBooked(request.BookingDto.RoomId, request.BookingDto.CheckInDate, request.BookingDto.CheckOutDate, cancellationToken))
                {
                    return new BookingResponse(false, "Room is already booked", null);
                }

                var mappedRequest = BookingMapper.MapBookingDtoToBooking(request.BookingDto);
                var result = await _bookingService.CreateBooking(mappedRequest, cancellationToken);
                return new BookingResponse(true, "Booking created successfully", result);
            }
            catch (Exception ex)
            {
                return new BookingResponse(false, ex.Message, null);
            }
        }
    }
}
