using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<List<Room>> GetAllRooms(CancellationToken cancellationToken)
        {
            return await _roomRepository.GetAllRooms(cancellationToken);
        }

        public async Task<Room> GetRoomById(int id, CancellationToken cancellationToken)
        {
            return await _roomRepository.GetRoomById(id, cancellationToken);
        }

        public async Task<Room> CreateRoom(Room room, CancellationToken cancellationToken)
        {
            return await _roomRepository.CreateRoom(room, cancellationToken);
        }

        public async Task<Room> UpdateRoom(Room room, CancellationToken cancellationToken)
        {
            return await _roomRepository.UpdateRoom(room, cancellationToken);
        }

        public async Task<bool> IsRoomNumberExists(int roomNumber, CancellationToken cancellationToken)
        {
            return await _roomRepository.IsRoomNumberExists(roomNumber, cancellationToken);
        }

        public async Task<bool> IsRoomNumberExistsForUpdate(int roomNumber, int roomId, CancellationToken cancellationToken)
        {
            return await _roomRepository.IsRoomNumberExistsForUpdate(roomNumber, roomId, cancellationToken);
        }

        public async Task<List<Room>> GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate, CancellationToken cancellationToken)
        {
            return await _roomRepository.GetAvailableRooms(checkInDate, checkOutDate, cancellationToken);
        }
    }
}
