using Leaguelane.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public interface IFixtureRepository
    {
        Task<Fixture> AddFixtureAsync(Fixture fixture, CancellationToken cancellationToken);
        Task AddFixturesBatchAsync(List<Fixture> fixture, CancellationToken cancellationToken);
    }
}
