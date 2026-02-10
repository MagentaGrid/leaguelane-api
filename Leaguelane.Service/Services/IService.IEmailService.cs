using Leaguelane.Enums.Enums;

namespace Leaguelane.Service.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(NotificationTypes notificationType, string email, object parameters);
    }
}
