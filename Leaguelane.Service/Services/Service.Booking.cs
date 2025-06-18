using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public class BookingService: IBookingService
    {
        public readonly IBookingRepository _bookingRepository;
        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<List<Booking>> GetAllBookings(CancellationToken cancellationToken)
        {
            return await _bookingRepository.GetAllBookings(cancellationToken);
        }

        public async Task<Booking> GetBookingById(int id, CancellationToken cancellationToken)
        {
            return await _bookingRepository.GetBookingById(id, cancellationToken);
        }

        public async Task<Booking> CreateBooking(Booking booking, CancellationToken cancellationToken)
        {
            return await _bookingRepository.CreateBooking(booking, cancellationToken);
        }

        public async Task<Booking> UpdateBooking(Booking booking, CancellationToken cancellationToken)
        {
            return await _bookingRepository.UpdateBooking(booking, cancellationToken);
        }

        public async Task<bool> IsRoomBooked(int roomId, DateTime checkInDate, DateTime checkOutDate, CancellationToken cancellationToken)
        {
            return await _bookingRepository.IsRoomBooked(roomId, checkInDate, checkOutDate, cancellationToken);
        }
    }
}
