using Leaguelane.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public interface ISeasonRepository
    {
        Task<List<Season>> AddSeasons(List<int> seasons, CancellationToken cancellationToken);
        Task<List<Season>> GetAllSeasons(CancellationToken cancellationToken);
    }
}
