using Leaguelane.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public interface ICountryRepository
    {
        Task<Country> AddCountry(Country country, CancellationToken cancellationToken);
        Task<bool> AddCountries(List<Country> countries, CancellationToken cancellationToken);
    }
}
