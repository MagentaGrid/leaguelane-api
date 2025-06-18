using Leaguelane.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetAllRooms(CancellationToken cancellationToken);
        Task<Room> GetRoomById(int id, CancellationToken cancellationToken);
        Task<Room> CreateRoom(Room room, CancellationToken cancellationToken);
        Task<Room> UpdateRoom(Room room, CancellationToken cancellationToken);
        Task<bool> IsRoomNumberExists(int roomNumber, CancellationToken cancellationToken);
        Task<bool> IsRoomNumberExistsForUpdate(int roomNumber, int roomId, CancellationToken cancellationToken);
        Task<List<Room>> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate, CancellationToken cancellationToken);
    }
}
