using Leaguelane.Models.Dtos;
using Leaguelane.Persistence.Entities;

namespace Leaguelane.Api.Mappers
{
    public static class BookingMapper
    {
        public static Booking MapBookingDtoToBooking(BookingDto bookingDto)
        {
            return new Booking
            {
                Status = bookingDto.Status,
                Created = DateTime.UtcNow,
                TotalGuests = bookingDto.TotalGuests,
                RoomID = bookingDto.RoomId,
                UserID = bookingDto.UserId,
                BasePrice = bookingDto.BasePrice,
                TotalAmount = bookingDto.TotalAmount,
                CheckInDate = bookingDto.CheckInDate,
                CheckOutDate = bookingDto.CheckOutDate,
                Active = true,
                ExtraCharges = bookingDto.ExtraCharges,
            };
        }

        public static List<BookingDto> MapBookingToBookingResponseDto(List<Booking> bookings)
        {
            return bookings.Select(booking => new BookingDto
            {
                BookingId = booking.BookingID,
                Status = booking.Status,
                TotalGuests = booking.TotalGuests,
                RoomId = booking.RoomID,
                UserId = booking.UserID,
                BasePrice = booking.BasePrice,
                TotalAmount = booking.TotalAmount,
                CheckInDate = booking.CheckInDate.ToUniversalTime(),
                CheckOutDate = booking.CheckOutDate.ToUniversalTime(),
                ExtraCharges = booking.ExtraCharges,
            }).ToList();
        }
    }
}
