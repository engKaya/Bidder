using EventBus.Base.Events;

namespace Bidder.NotificationService.IntegrationEvents.User.Events
{
    public class NewUserIntegrationEvent : IntegrationEvent
    {
        public string Email { get; set; }
        public string UserName { get; set; }

        public NewUserIntegrationEvent(string email, string userName)
        {
            Email = email;
            UserName = userName;
        }
    }
}
