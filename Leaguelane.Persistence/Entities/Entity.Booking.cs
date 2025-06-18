using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Persistence.Entities
{
    public class Booking: Entity
    {
        public int BookingID { get; set; }

        [ForeignKey("Room")]
        public int RoomID { get; set; }

        //Navigation Property
        public Room Room { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        //Navigation Property
        public User User { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public int TotalGuests { get; set; }

        public decimal BasePrice { get; set; }

        public decimal ExtraCharges { get; set; } = 0; // Default to 0 if no extra charges

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "Pending"; // Default status
    }
}
