using Bidder.Notification.Application.Abstraction.EmailBase;
using Bidder.Notification.Application.Abstraction.User;
using Bidder.Notification.EventIntegration.IntegrationEvents.User.Events;
using EventBus.Base.Abstraction;
using Microsoft.Extensions.Logging;

namespace Bidder.Notification.EventIntegration.IntegrationEvents.User.EventHandlers
{
    internal class NewUserIntegrationEventHandler : IIntegrationEventHandler<NewUserIntegrationEvent>
    {
        private readonly ILogger<NewUserIntegrationEventHandler> _logger;
        private readonly IUserRelatedMails mailHelper;

        public NewUserIntegrationEventHandler(ILogger<NewUserIntegrationEventHandler> logger, IUserRelatedMails mailHelper)
        {
            _logger = logger;
            this.mailHelper = mailHelper;
        }

        public Task Handle(NewUserIntegrationEvent @event)
        {
            try
            {
                _logger.LogInformation($"New User: Name: {@event.UserName}");
                mailHelper.WelcomeNewUserMail(@event.Email, @event.UserName);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error has occured at {typeof(NewUserIntegrationEvent)}, \n {ex.StackTrace}");
                throw;
            }
        }
    }
}
