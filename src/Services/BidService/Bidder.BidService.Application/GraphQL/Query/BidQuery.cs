using Bidder.BidService.Application.GraphQL.Types;
using Bidder.BidService.Application.Interfaces.Services;
using GraphQL;
using GraphQL.Types;

namespace Bidder.BidService.Application.GraphQL.Query
{
    public class BidQuery : ObjectGraphType
    {
        public BidQuery(IBiddingService biddingService)
        {
            Name = "Bid_Queries";
            //TODO: Graphql endpointlerini belirliyoruz.
            Field<ListGraphType<BidGraphType>>("Bids", resolve: ctx => biddingService.GetAllBids(new CancellationToken()).Result);




            //Field<ListGraphType<MaterialType>>("MeterialByBrandId",
            //arguments: new QueryArguments
            //{
            // new QueryArgument<IntGraphType>{
            //     Name="Id",
            //     Description="Brand Id"
            // }
            //},
            // resolve: ctx => _marketingContext.GetMaterialsByBrandId(ctx.GetArgument<int>("Id")));
            //Field<ListGraphType<BrandType>>("Brands", resolve: ctx => _marketingContext.GetBrands());

        }
    }
}
