using Leaguelane.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public interface ISportService
    {
        Task<List<Sport>> GetAllSports(CancellationToken cancellationToken);
        Task<Sport> GetSport(int id, CancellationToken cancellationToken);
        Task<Sport> AddSport(Sport sport, CancellationToken cancellationToken);
        Task<Sport> UpdateSport(Sport sport, CancellationToken cancellationToken);
    }
}
