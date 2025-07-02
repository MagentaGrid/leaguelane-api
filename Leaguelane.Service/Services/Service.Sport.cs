using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public class SportService
        : ISportService
    {
        private readonly ISportRepository _sportRepository;

        public SportService(ISportRepository sportRepository)
        {
            _sportRepository = sportRepository;
        }

        public async Task<List<Sport>> GetAllSports(CancellationToken cancellationToken)
        {
            return await _sportRepository.GetAllSports(cancellationToken);
        }

        public async Task<Sport> GetSport(int id, CancellationToken cancellationToken)
        {
            return await _sportRepository.GetSport(id, cancellationToken);
        }

        public async Task<Sport> AddSport(Sport sport, CancellationToken cancellationToken)
        {
            return await _sportRepository.AddSport(sport, cancellationToken);
        }

        public async Task<Sport> UpdateSport(Sport sport, CancellationToken cancellationToken)
        {
            return await _sportRepository.UpdateSport(sport, cancellationToken);
        }
    }
}
