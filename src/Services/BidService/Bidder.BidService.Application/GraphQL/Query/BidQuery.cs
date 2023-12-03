using Bidder.BidService.Application.GraphQL.Types;
using Bidder.BidService.Application.Interfaces.Services;
using Bidder.BidService.Domain.Entities;
using Bidder.Domain.Common.BaseClassess;
using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Types;
using System.Reflection.Emit;

namespace Bidder.BidService.Application.GraphQL.Query
{
    public class BidQuery : ObjectGraphType
    {
        public BidQuery(IBiddingService biddingService)
        {
            Name = "Bid_Queries";
            AddField(new FieldType()
            {
                Name = "Bids",
                Type = typeof(ListGraphType<BidGraphType>),
                Resolver = new FuncFieldResolver<IEnumerable<Bid>>(async _ => (await biddingService.GetAllBids(new CancellationToken())).Data)


            });
            //Field(type: typeof(ListGraphType<BidGraphType>), name:"Bids", resolve: ctx => biddingService.GetAllBids(new CancellationToken()).Result); 
        }
    }
}
