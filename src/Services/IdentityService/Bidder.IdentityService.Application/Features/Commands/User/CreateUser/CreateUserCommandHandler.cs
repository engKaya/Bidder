using Bidder.Domain.Common.BaseClassess;
using Bidder.IdentityService.Application.IntegrationEvents;
using Bidder.IdentityService.Application.Interfaces.Repos;
using Bidder.IdentityService.Domain.Entities;
using EventBus.Base.Abstraction;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Bidder.IdentityService.Application.Features.Commands.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseMessageNoContent>
    {
        private readonly IUnitOfWork uof;
        private readonly ILogger<CreateUserCommandHandler> logger;
        private readonly IEventBus eventBus;

        public CreateUserCommandHandler(IUnitOfWork uof, IEventBus eventBus, ILogger<CreateUserCommandHandler> logger)
        {
            this.uof = uof;
            this.logger = logger;
            this.eventBus = eventBus;
        }

        public async Task<ResponseMessageNoContent> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Users userMailCheck = await uof.UserRepository.FindFirst(x => x.Email == request.Email);
                if (userMailCheck is not null)
                    return ResponseMessageNoContent.Fail("User Already Exists", 400);

                var user = new Users(request.Email, request.Password, request.Name, request.Surname);

                await uof.UserRepository.Add(user);
                await uof.SaveChangesAsync();
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
