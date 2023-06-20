using Bidder.UserService.Domain.Extensions;
using Bidder.UserService.Infastructure.Context;
using MediatR;

namespace Bidder.UserService.Infastructure.Extensions
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEvents(this IMediator mediator, UserDbContext context)
        {
            var domainEntites = context.ChangeTracker
                .Entries<BaseEntity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntites.SelectMany(x => x.Entity.DomainEvents).ToList();
            domainEntites.ToList().ForEach(e => { e.Entity.ClearDomainEvents(); });

            foreach (var doevent in domainEvents)
            {
                await mediator.Publish(doevent);
            }
        }
    }
}
