
using Bidder.BidService.Infastructure.Context;
using Bidder.Domain.Common.Entity; 
using MediatR;

namespace Bidder.BidService.Infastructure.Extensions
{
    public static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, BidDbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<BaseEntity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());


            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);

        }
    }
}
