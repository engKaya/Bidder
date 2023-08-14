using Bidder.Notification.Application.DTOs;
using Bidder.Notification.Application.Response;

namespace Bidder.Notification.Application.Abstraction
{
    public interface IEmailService
    { 
        Task<EmailResponse> SendEmailAsync(MailParameters parameters);
        void WelcomeNewUserMail(string email, string fullname);
    }
}
