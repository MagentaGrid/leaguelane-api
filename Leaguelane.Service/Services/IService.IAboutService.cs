using Leaguelane.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public interface IAboutService
    {
        Task<About> CreateAbout(About about, CancellationToken cancellationToken);
        Task<About> UpdateAbout(About about, CancellationToken cancellationToken);
        Task<About> GetAbout(int id, CancellationToken cancellationToken);
        Task<List<About>> GetAllAbouts(CancellationToken cancellationToken);
    }
}
