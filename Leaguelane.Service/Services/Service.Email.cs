using Leaguelane.Enums.Enums;
using Leaguelane.Persistence.Entities;
using Leaguelane.Repository.Repositories;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Leaguelane.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly IRepository _repository;

        public EmailService(IConfiguration config, IRepository repository)
        {
            _config = config;
            _repository = repository;
        }

        public async Task SendEmailAsync(NotificationTypes notificationType, string email, object parameters)
        {
            var template = await _repository.FirstOrDefaultAsync<NotificationTemplate>(
                x => x.NotificationType == notificationType && x.Status == "Active" && x.IsEmail);

            if (template == null || template.Body == null)
                return;

            var processedBody = ApplyTemplate(template.Body, parameters);

            await SendAsync(
                email,
                template.Subject!,
                processedBody,
                true
            );
        }

        private async Task SendAsync(string to, string subject, string body, bool isHtml)
        {
            var smtpServer = _config["EmailSettings:SmtpServer"];
            var port = int.Parse(_config["EmailSettings:Port"]!);
            var senderName = _config["EmailSettings:SenderName"];
            var senderEmail = _config["EmailSettings:SenderEmail"];
            var username = _config["EmailSettings:Username"];
            var password = _config["EmailSettings:Password"];

            var message = new MailMessage
            {
                From = new MailAddress(senderEmail!, senderName),
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml
            };

            message.To.Add(to);

            using var smtp = new SmtpClient(smtpServer, port)
            {
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true
            };

            await smtp.SendMailAsync(message);
        }

        private static string ApplyTemplate(string template, object parameters)
        {
            if (parameters == null)
                return template;

            var props = parameters.GetType().GetProperties();

            foreach (var prop in props)
            {
                var placeholder = $"{{{{{prop.Name}}}}}";
                var value = prop.GetValue(parameters)?.ToString() ?? string.Empty;

                template = template.Replace(placeholder, value);
            }

            return template;
        }

    }
}
