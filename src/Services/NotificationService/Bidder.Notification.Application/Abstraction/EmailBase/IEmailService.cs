using Bidder.Notification.Application.DTOs;
using Bidder.Notification.Application.Response;

namespace Bidder.Notification.Application.Abstraction.EmailBase
{
    public interface IEmailService
    {
        Task<EmailResponse> SendEmailAsync(MailParameters parameters);
    }
}
