using Leaguelane.Enums.Enums;
using System.ComponentModel.DataAnnotations;

namespace Leaguelane.Persistence.Entities
{
    public class NotificationTemplate
    {
        [Key]
        public int NotificationTemplateId { get; set; }
        public NotificationTypes NotificationType { get; set; }
        public bool IsSms { get; set; }
        public bool IsEmail { get; set; }
        public string Status { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
    }
}
