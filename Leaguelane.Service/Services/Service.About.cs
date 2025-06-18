using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public class AboutService : IAboutService
    {
        private readonly IAboutRepository _aboutRepository;

        public AboutService(IAboutRepository aboutRepository)
        {
            _aboutRepository = aboutRepository;
        }

        public async Task<About> CreateAbout(About about, CancellationToken cancellationToken)
        {
            return await _aboutRepository.CreateAbout(about, cancellationToken);
        }
        public async Task<About> UpdateAbout(About about, CancellationToken cancellationToken)
        {
            return await _aboutRepository.UpdateAbout(about, cancellationToken);
        }

        public async Task<About> GetAbout(int id, CancellationToken cancellationToken)
        {
            return await _aboutRepository.GetAbout(id, cancellationToken);
        }

        public async Task<List<About>> GetAllAbouts(CancellationToken cancellationToken)
        {
            return await _aboutRepository.GetAllAbouts(cancellationToken);
        }
    }
}
