using Leaguelane.Enums.Enums;
using Leaguelane.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public interface IJobConfigurationService
    {
        Task<JobConfiguration> AddJobConfiguration(JobConfiguration jobConfiguration, CancellationToken cancellationToken);
        Task<JobConfiguration> UpdateJobConfiguration(JobConfiguration jobConfiguration, CancellationToken cancellationToken);
        Task<List<JobConfiguration>> GetAllJobConfigurations(CancellationToken cancellationToken);
        Task<JobConfiguration> GetJobConfigurationById(Jobs id, CancellationToken cancellationToken);
    }
}
