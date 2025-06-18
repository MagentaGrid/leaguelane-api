using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailGmailAsync(string userEmail, string userPhone, string userMessage)
        {
            var smtpClient = new SmtpClient(_config["EmailSettings:Gmail:SmtpServer"])
            {
                Port = int.Parse(_config["EmailSettings:Gmail:Port"]),
                Credentials = new NetworkCredential(
                    _config["EmailSettings:Gmail:SenderEmail"],
                    _config["EmailSettings:Gmail:SenderPassword"]
                ),
                EnableSsl = true
            };

            string companyEmail = _config["EmailSettings:Gmail:ReceiverEmail"];

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_config["EmailSettings:Gmail:SenderEmail"]),
                Subject = "New Contact Us Query",
                Body = $"<p><strong>Email:</strong> {userEmail}</p>" +
                       $"<p><strong>Phone:</strong> {userPhone}</p>" +
                       $"<p><strong>Message:</strong> {userMessage}</p>",
                IsBodyHtml = true
            };

            mailMessage.To.Add(companyEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }

        public Task SendEmailOutlookAsync(string userEmail, string userPhone, string userMessage)
        {
            // Configure the SmtpClient
            var smtpClient = new SmtpClient(_config["EmailSettings:Outlook:SmtpServer"])
            {
                Port = int.Parse(_config["EmailSettings:Outlook:Port"]),
                Credentials = new NetworkCredential(
                    _config["EmailSettings:Outlook:SenderEmail"],
                    _config["EmailSettings:Outlook:SenderPassword"]
                ),
                EnableSsl = true, // Or false for non-TLS
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            // Create the MailMessage
            var mail = new MailMessage
            {
                From = new MailAddress(_config["EmailSettings:Outlook:SenderEmail"]),
                To = { _config["EmailSettings:Gmail:ReceiverEmail"] },
                Subject = "New Contact Us Query",
                Body = $"<p><strong>Email:</strong> {userEmail}</p>" +
                       $"<p><strong>Phone:</strong> {userPhone}</p>" +
                       $"<p><strong>Message:</strong> {userMessage}</p>",
                IsBodyHtml = true
            };

            // Send the email
            smtpClient.Send(mail);

            return Task.CompletedTask;
        }
    }

}
