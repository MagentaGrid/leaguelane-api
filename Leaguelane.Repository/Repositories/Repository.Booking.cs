using Leaguelane.Persistence.Context;
using Leaguelane.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public class BookingRepository: IBookingRepository
    {
        private readonly LeaguelaneDbContext _context;

        public BookingRepository(LeaguelaneDbContext context)
        {
            _context = context;
        }


        public async Task<List<Booking>> GetAllBookings(CancellationToken cancellationToken)
        {
            return await _context.Bookings.ToListAsync(cancellationToken);
        }

        public async Task<Booking> GetBookingById(int id, CancellationToken cancellationToken)
        {
            return await _context.Bookings.Where(x => x.BookingID == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Booking> CreateBooking(Booking booking, CancellationToken cancellationToken)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync(cancellationToken);
            return booking;
        }

        public async Task<Booking> UpdateBooking(Booking booking, CancellationToken cancellationToken)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync(cancellationToken);
            return booking;
        }

        public async Task<bool> IsRoomBooked(int roomId, DateTime checkInDate, DateTime checkOutDate, CancellationToken cancellationToken)
        {
            return await _context.Bookings.AnyAsync(x =>
                x.RoomID == roomId 
                && x.CheckInDate < checkOutDate 
                && checkInDate < x.CheckOutDate,
                cancellationToken);
        }

    }
}
