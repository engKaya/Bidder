using Bidder.BidService.Application.Features.Command.Bidding.CreateBid;
using MediatR;

namespace Bidder.BidService.Api.Registration.InternalServices
{
    public static class MediatRRegistrationExtension
    {
        public static void AddMediatRRegistrations(this IServiceCollection services)
        { 
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(CreateBidCommand)));
        }
    }
}
