using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Service.Services
{
    public interface IEmailService
    {
        Task SendEmailGmailAsync(string userEmail, string userPhone, string userMessage);
        Task SendEmailOutlookAsync(string userEmail, string userPhone, string userMessage);
    }
}
