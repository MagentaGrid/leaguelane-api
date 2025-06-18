using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _roomTypeRepository;

        public RoomTypeService(IRoomTypeRepository roomTypeRepository)
        {
            _roomTypeRepository = roomTypeRepository;
        }

        public async Task<List<RoomType>> GetAllRoomTypes(CancellationToken cancellationToken)
        {
            return await _roomTypeRepository.GetAllRoomTypes(cancellationToken);
        }

        public async Task<RoomType> GetRoomTypeById(int id, CancellationToken cancellationToken)
        {
            return await _roomTypeRepository.GetRoomTypeById(id, cancellationToken);
        }

        public async Task<RoomType> CreateRoomType(RoomType roomType, CancellationToken cancellationToken)
        {
            return await _roomTypeRepository.CreateRoomType(roomType, cancellationToken);
        }

        public async Task<RoomType> UpdateRoomType(RoomType roomType, CancellationToken cancellationToken)
        {
            return await _roomTypeRepository.UpdateRoomType(roomType, cancellationToken);
        }

        public async Task<bool> IsRoomTypeNameExists(string roomTypeName, CancellationToken cancellationToken)
        {
            return await _roomTypeRepository.IsRoomTypeNameExists(roomTypeName, cancellationToken);
        }

        public async Task<bool> IsRoomTypeNameExistsForUpdate(string roomTypeName, int roomTypeId, CancellationToken cancellationToken)
        {
            return await _roomTypeRepository.IsRoomTypeNameExistsForUpdate(roomTypeName, roomTypeId, cancellationToken);
        }
    }
}
