using Leaguelane.Enums.Enums;

namespace Leaguelane.Persistence.Entities
{
    public class JobSchedueler: Entity
    {
        public Jobs JobScheduelerId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string RunStatus { get; set; } = string.Empty;

        public DateTime? LastRun { get; set; }

        public DateTime? NextRun { get; set; }

        public string RunBy { get; set; } = "System";

        public string Status { get; set; } = string.Empty;
    }
}
