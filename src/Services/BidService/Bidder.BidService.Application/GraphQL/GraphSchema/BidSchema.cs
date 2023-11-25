using Bidder.BidService.Application.GraphQL.Query;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Bidder.BidService.Application.GraphQL.GraphSchema
{
    public class BidSchema : Schema
    {
        public BidSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetService<BidQuery>();
        }
    }
}
