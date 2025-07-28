using Leaguelane.Enums.Enums;
using Leaguelane.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Repository.Repositories
{
    public interface IJobConfigurationRepository
    {
        Task<JobConfiguration> AddJobConfiguration(JobConfiguration jobConfiguration, CancellationToken cancellationToken);
        Task<JobConfiguration> UpdateJobConfiguration(JobConfiguration jobConfiguration, CancellationToken cancellationToken);
        Task<List<JobConfiguration>> GetJobConfiguration(CancellationToken cancellationToken);
        Task<JobConfiguration> GetJobConfigurationById(Jobs id, CancellationToken cancellationToken);
    }
}
