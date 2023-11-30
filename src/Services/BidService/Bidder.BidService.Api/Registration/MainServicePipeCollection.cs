using Bidder.BidService.Api.Registration.InternalServices;

namespace Bidder.BidService.Api.Registration
{
    public static class MainServicePipeCollection
    {
        public static void StartServicePipe(this IServiceCollection services, IConfiguration configuration)
        { 
            services.StartInternalServicePipe(configuration); 
        }
    }
}
