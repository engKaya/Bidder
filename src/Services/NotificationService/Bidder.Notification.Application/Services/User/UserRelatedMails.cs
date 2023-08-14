using Bidder.Notification.Application.Abstraction.EmailBase;
using Bidder.Notification.Application.Abstraction.User;
using Bidder.Notification.Application.DTOs;

namespace Bidder.Notification.Application.Services.User
{
    public sealed class UserRelatedMails : IUserRelatedMails
    {
        private readonly IEmailFactory mailFactory; 

        public UserRelatedMails(IEmailFactory mailFactory)
        {
            this.mailFactory = mailFactory;
        }

        public void WelcomeNewUserMail(string email, string fullname)
        {
            string body = $@"
                Hello {fullname}, 
                Welcome to Bidder            
            ";

            MailParameters parameters = new MailParameters(new List<string>() { email }, "Welcome to Bidder", body);
            mailFactory.EmailService.SendEmailAsync(parameters);
        }

    }
}
