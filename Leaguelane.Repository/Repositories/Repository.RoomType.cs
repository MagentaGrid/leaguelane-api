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
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly LeaguelaneDbContext _context;
        public RoomTypeRepository(LeaguelaneDbContext context)
        {
            _context = context;
        }

        public async Task<RoomType> GetRoomTypeById(int id, CancellationToken cancellationToken)
        {
            return await _context.RoomTypes.AsNoTracking().Where(x => x.RoomTypeID == id && x.Active == true).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<RoomType>> GetAllRoomTypes(CancellationToken cancellationToken)
        {
            return await _context.RoomTypes.AsNoTracking().Where(x => x.Active == true).ToListAsync(cancellationToken);
        }

        public async Task<RoomType> CreateRoomType(RoomType roomType, CancellationToken cancellationToken)
        {
            _context.RoomTypes.Add(roomType);
            await _context.SaveChangesAsync(cancellationToken);
            return roomType;
        }

        public async Task<RoomType> UpdateRoomType(RoomType roomType, CancellationToken cancellationToken)
        {
            _context.RoomTypes.Update(roomType);
            await _context.SaveChangesAsync(cancellationToken);
            return roomType;
        }

        public async Task<bool> IsRoomTypeNameExists(string roomTypeName, CancellationToken cancellationToken)
        {
            return await _context.RoomTypes.AnyAsync(x => x.Name == roomTypeName, cancellationToken);
        }

        public async Task<bool> IsRoomTypeNameExistsForUpdate(string roomTypeName, int roomTypeId, CancellationToken cancellationToken)
        {
            return await _context.RoomTypes.AnyAsync(x => x.Name == roomTypeName && x.RoomTypeID != roomTypeId, cancellationToken);
        }
    }
}
