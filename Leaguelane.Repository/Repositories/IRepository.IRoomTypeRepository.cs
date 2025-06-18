using Leaguelane.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public interface IRoomTypeRepository
    {
        Task<RoomType> GetRoomTypeById(int id, CancellationToken cancellationToken);
        Task<List<RoomType>> GetAllRoomTypes(CancellationToken cancellationToken);
        Task<RoomType> CreateRoomType(RoomType roomType, CancellationToken cancellationToken);
        Task<RoomType> UpdateRoomType(RoomType roomType, CancellationToken cancellationToken);
        Task<bool> IsRoomTypeNameExists(string roomTypeName, CancellationToken cancellationToken);
        Task<bool> IsRoomTypeNameExistsForUpdate(string roomTypeName, int roomTypeId, CancellationToken cancellationToken);
    }
}
