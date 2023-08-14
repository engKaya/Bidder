using Bidder.Notification.Application.Enum;

namespace Bidder.Notification.Application.Abstraction.EmailBase
{
    public interface IEmailFactory
    {
        IEmailService EmailService { get; }
        IEmailService GenerateEmailService(EmailClients clientType);
    }
}
