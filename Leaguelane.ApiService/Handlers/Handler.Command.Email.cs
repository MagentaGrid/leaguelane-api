using Leaguelane.Models.Dtos;
using Leaguelane.Service.Services;
using MediatR;

namespace Leaguelane.Api.Handlers
{
    public record SendEmailCommand(EmailDto email) : IRequest<EmailResponse>;
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, EmailResponse>
    {
        private readonly IEmailService _emailService;
        public SendEmailCommandHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task<EmailResponse> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _emailService.SendEmailGmailAsync(request.email.FromEmail, request.email.PhoneNumber, request.email.Body);
                return new EmailResponse
                {
                    IsSuccess = true,
                    ErrorMessage = "Email sent successfully",
                };
            }
            catch (Exception ex)
            {
                return new EmailResponse
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message,
                };
            }

        }
    }
}
