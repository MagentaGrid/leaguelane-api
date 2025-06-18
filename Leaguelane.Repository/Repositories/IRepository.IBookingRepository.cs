using Leaguelane.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAllBookings(CancellationToken cancellationToken);
        Task<Booking> GetBookingById(int id, CancellationToken cancellationToken);
        Task<Booking> CreateBooking(Booking booking, CancellationToken cancellationToken);
        Task<Booking> UpdateBooking(Booking booking, CancellationToken cancellationToken);
        Task<bool> IsRoomBooked(int roomId, DateTime checkInDate, DateTime checkOutDate, CancellationToken cancellationToken);
    }
}
