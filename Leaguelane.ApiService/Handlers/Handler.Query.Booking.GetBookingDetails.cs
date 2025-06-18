using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record GetBookingDetailsQuery(int Id): IRequest<BookingResponse>;
    public class GetBookingDetailsQueryHandler: IRequestHandler<GetBookingDetailsQuery, BookingResponse>
    {
        private readonly IBookingService _bookingService;

        public GetBookingDetailsQueryHandler(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public async Task<BookingResponse> Handle(GetBookingDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _bookingService.GetBookingById(request.Id, cancellationToken);
                if (result == null)
                {
                    return new BookingResponse(false, "Booking not found", null);
                }
                return new BookingResponse(true, "Booking details fetched successfully", result);
            }
            catch (Exception ex)
            {
                return new BookingResponse(false, ex.Message, null);
            }
        }
    }
}
