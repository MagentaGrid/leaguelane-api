using Leaguelane.Enums.Enums;
using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public class JobConfigurationService: IJobConfigurationService
    {
        private readonly IJobConfigurationRepository _jobConfigurationRepository;

        public JobConfigurationService(IJobConfigurationRepository jobConfigurationRepository)
        {
            _jobConfigurationRepository = jobConfigurationRepository;
        }

        public async Task<JobConfiguration> AddJobConfiguration(JobConfiguration jobConfiguration, CancellationToken cancellationToken)
        {
            return await _jobConfigurationRepository.AddJobConfiguration(jobConfiguration, cancellationToken);
        }

        public async Task<List<JobConfiguration>> GetAllJobConfigurations(CancellationToken cancellationToken)
        {
            return await _jobConfigurationRepository.GetJobConfiguration(cancellationToken);
        }

        public async Task<JobConfiguration> GetJobConfigurationById(Jobs id, CancellationToken cancellationToken)
        {
            return await _jobConfigurationRepository.GetJobConfigurationById(id, cancellationToken);
        }

        public async Task<JobConfiguration> UpdateJobConfiguration(JobConfiguration jobConfiguration, CancellationToken cancellationToken)
        {
            return await _jobConfigurationRepository.UpdateJobConfiguration(jobConfiguration, cancellationToken);
        }
    }
}
