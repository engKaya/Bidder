namespace Bidder.Notification.Application.Abstraction.User
{
    public interface IUserRelatedMails
    {
        void WelcomeNewUserMail(string email, string fullname);
    }
}
