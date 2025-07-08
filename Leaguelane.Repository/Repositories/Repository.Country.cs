using Leaguelane.Persistence.Context;
using Leaguelane.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public class CountryRepository: ICountryRepository
    {
        private readonly LeaguelaneDbContext _context;

        public CountryRepository(LeaguelaneDbContext context)
        {
            _context = context;
        }

        public async Task<Country> AddCountry(Country country, CancellationToken cancellationToken)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync(cancellationToken);
            return country;
        }

        public async Task<bool> AddCountries(List<Country> countries, CancellationToken cancellationToken)
        {
            var existingCountries = _context.Countries.Where(x => x.Active == true).ToList();

            var countriesToBeAdded = countries.Where(x => !existingCountries.Any(y => y.Name == x.Name)).ToList();

            _context.Countries.AddRange(countriesToBeAdded);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
