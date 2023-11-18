using Bidder.Domain.Common.BaseClassess;
using Bidder.IdentityService.Application.IntegrationEvents;
using Bidder.IdentityService.Application.Services.Interfaces;
using Bidder.IdentityService.Domain.Entities;
using EventBus.Base.Abstraction;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bidder.IdentityService.Application.Features.Commands.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseMessageNoContent>
    {
        private readonly IUserService userService;
        private readonly ILogger<CreateUserCommandHandler> logger;
        private readonly IEventBus eventBus;

        public CreateUserCommandHandler(IUserService userService, IEventBus eventBus, ILogger<CreateUserCommandHandler> logger)
        {
            this.userService = userService;
            this.logger = logger;
            this.eventBus = eventBus;
        }

        public async Task<ResponseMessageNoContent> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool userMailCheck = await userService.IsEmailUnique(request.Email);
                if (!userMailCheck)
                    return ResponseMessageNoContent.Fail("User Already Exists", 400);

                var user = new Users(request.Email, request.Password, request.Name, request.Surname);

                await userService.SaveUser(user, cancellationToken);
                eventBus.Publish(new NewUserIntegrationEvent(user.Email, user.Username));
                return ResponseMessageNoContent.Success("User Successfully Created", 200);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.StackTrace);
                return ResponseMessageNoContent.Fail("User Creation Failed", 500);
            }
        }
    }
}
