using Leaguelane.Enums.Enums;

namespace Leaguelane.Models.Dtos
{
    public class JobConfigurationsDto
    {
        public Jobs JobId { get; set; }
        public string JobName { get; set; }
        public string JobType { get; set; }
        public string? JobParameter { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int SportId { get; set; }
        public bool Active { get; set; }
    }

    public class JobConfigurationResponse : Response
    {
        public JobConfigurationResponseDto JobConfiguration { get; set; }
    }

    public class JobConfigurationsResponse : Response
    {
        public List<JobConfigurationResponseDto> JobConfigurations { get; set; }
    }

    public class JobConfigurationResponseDto
    {
        public Jobs JobId { get; set; }
        public string JobName { get; set; }
        public string JobType { get; set; }
        public string? JobParameter { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int SportId { get; set; }
        public bool Active { get; set; }
    }
}
