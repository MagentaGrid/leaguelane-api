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
    public class RoomRepository : IRoomRepository
    {
        private readonly LeaguelaneDbContext _context;

        public RoomRepository(LeaguelaneDbContext context)
        {
            _context = context;
        }

        public async Task<Room> CreateRoom(Room room, CancellationToken cancellationToken)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync(cancellationToken);
            return room;
        }

        public async Task<Room> UpdateRoom(Room room, CancellationToken cancellationToken)
        {
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync(cancellationToken);
            return room;
        }

        public async Task<Room> GetRoomById(int id, CancellationToken cancellationToken)
        {
            return await _context.Rooms.Where(x => x.RoomId == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Room>> GetAllRooms(CancellationToken cancellationToken)
        {
            return await _context.Rooms.Include(x => x.RoomType).ToListAsync(cancellationToken);
        }

        public async Task<bool> IsRoomNumberExists(int roomNumber, CancellationToken cancellationToken)
        {
            return await _context.Rooms.AnyAsync(x => x.RoomNumber == roomNumber, cancellationToken);
        }

        public async Task<bool> IsRoomNumberExistsForUpdate(int roomNumber, int roomId, CancellationToken cancellationToken)
        {
            return await _context.Rooms.AnyAsync(x => x.RoomNumber == roomNumber && x.RoomId != roomId, cancellationToken);
        }

        public async Task<List<Room>> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate, CancellationToken cancellationToken)
        {
            return await _context.Set<Room>()
                .Where(room => !_context.Set<Booking>()
                    .Any(booking => booking.RoomID == room.RoomId &&
                                    booking.CheckInDate <= checkInDate &&
                                    booking.CheckOutDate >= checkOutDate))
                .Select(room => new Room
                {
                    RoomId = room.RoomId,
                    RoomNumber = room.RoomNumber,
                    RoomTypeId = room.RoomTypeId,
                    RoomName = room.RoomName
                })
                .ToListAsync(cancellationToken);
        }
    }
}
