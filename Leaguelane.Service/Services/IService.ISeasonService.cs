using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public interface ISeasonService
    {
        Task<bool> GetAllSeasons(CancellationToken cancellationToken);
    }
}
