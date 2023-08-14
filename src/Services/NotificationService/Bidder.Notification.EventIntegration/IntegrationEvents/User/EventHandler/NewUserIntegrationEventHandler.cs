using Bidder.Notification.Application.Abstraction;
using Bidder.Notification.EventIntegration.IntegrationEvents.User.Events;
using EventBus.Base.Abstraction;
using Microsoft.Extensions.Logging;

namespace Bidder.Notification.EventIntegration.IntegrationEvents.User.EventHandlers
{
    internal class NewUserIntegrationEventHandler : IIntegrationEventHandler<NewUserIntegrationEvent>
    {
        private readonly ILogger<NewUserIntegrationEventHandler> _logger;
        private readonly IEmailService _emailService;

        public NewUserIntegrationEventHandler(ILogger<NewUserIntegrationEventHandler> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public Task Handle(NewUserIntegrationEvent @event)
        {
            try
            {
                _logger.LogInformation($"New User: Name: {@event.UserName}");
                _emailService.WelcomeNewUserMail(@event.Email, @event.UserName);
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
