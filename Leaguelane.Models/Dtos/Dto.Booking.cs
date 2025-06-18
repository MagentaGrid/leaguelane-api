using Leaguelane.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Models.Dtos
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int TotalGuests { get; set; }
        public decimal BasePrice { get; set; }
        public decimal ExtraCharges { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }

    public record BookingResponse
    (
        bool IsSuccess,
        string? Message,
        Booking? Booking
    );

    public class BookingsResponse : Response
    {
        public List<BookingDto>? Bookings { get; set; }
    }

    public class BookingResponseDto
    {
        public int BookingID { get; set; }
        public int RoomID { get; set; }
        public int UserID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int TotalGuests { get; set; }
        public decimal BasePrice { get; set; }
        public decimal ExtraCharges { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }

    public class BookingAvailabilityDto
    {
        public DateTime CheckInDate { get; set; } = DateTime.UtcNow;
        public DateTime CheckOutDate { get; set; } = DateTime.UtcNow.AddDays(1);
        public int NumberOfGuests { get; set; } = 2;
    }

    public class BookingAvailabilityResponse
    {
        public int RoomId { get; set; }
        public int RoomNumber { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomName { get; set; }
    }

}
