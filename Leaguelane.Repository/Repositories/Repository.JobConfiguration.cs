using Leaguelane.Enums.Enums;
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
    public class JobConfigurationRepository: IJobConfigurationRepository
    {
        private readonly LeaguelaneDbContext _context;
        public JobConfigurationRepository(LeaguelaneDbContext context)
        {
            _context = context;
        }

        public async Task<JobConfiguration> AddJobConfiguration(JobConfiguration jobConfiguration, CancellationToken cancellationToken)
        {
            await _context.JobConfigurations.AddAsync(jobConfiguration);
            await _context.SaveChangesAsync(cancellationToken);
            return jobConfiguration;
        }

        public async Task<JobConfiguration> UpdateJobConfiguration(JobConfiguration jobConfiguration, CancellationToken cancellationToken)
        {
            _context.JobConfigurations.Update(jobConfiguration);
            await _context.SaveChangesAsync(cancellationToken);
            return jobConfiguration;
        }

        public async Task<List<JobConfiguration>> GetJobConfiguration(CancellationToken cancellationToken)
        {
            return await _context.JobConfigurations.Where(x => x.Active == true).ToListAsync(cancellationToken);
        }

        public async Task<JobConfiguration> GetJobConfigurationById(Jobs id, CancellationToken cancellationToken)
        {
            return await _context.JobConfigurations.FirstOrDefaultAsync(x => x.JobId == id && x.Active == true, cancellationToken);
        }
    }
}
