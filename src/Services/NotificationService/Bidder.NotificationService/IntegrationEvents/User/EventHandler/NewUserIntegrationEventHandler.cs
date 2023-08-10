using Bidder.NotificationService.IntegrationEvents.User.Events;
using EventBus.Base.Abstraction;
using Microsoft.Extensions.Logging;

namespace Bidder.NotificationService.IntegrationEvents.User.EventHandler
{
    internal class NewUserIntegrationEventHandler : IIntegrationEventHandler<NewUserIntegrationEvent>
    {
        private readonly ILogger<NewUserIntegrationEventHandler> _logger;
        public NewUserIntegrationEventHandler(ILogger<NewUserIntegrationEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(NewUserIntegrationEvent @event)
        {
            try
            {
                _logger.LogInformation($"New User: Name: {@event.UserName}");
                throw new Exception("Test Exception");
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
