using Bidder.BidService.Application.Interfaces.Repos;
using Bidder.BidService.Application.Interfaces.Services;
using Bidder.BidService.Domain.Entities;
using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Bidder.BidService.Application.GraphQL.Types
{
    [Serializable]
    public class BidRoomGraphType :   ObjectGraphType<BidRoom>
    {
        public BidRoomGraphType(IServiceProvider provider)
        {
            var bidRoomUserService = provider.GetService<IBidRoomUserService>();


            Name = "BidRoom";
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from the bid object.");
            Field(x => x.BidId).Description("BidId property from the bid object.");
            Field(x => x.Bid, type: typeof(BidGraphType)).Description("Bid property from the bid object."); 
            Field(x => x.RoomStatus).Description("Room status of bid");
            //AddField(new FieldType()
            //{
            //    Name = "BidRoomUsers",
            //    Type = typeof(ListGraphType<BidRoomUserGraphType>),
            //    Resolver = new FuncFieldResolver<IEnumerable<BidRoomUser>>(_ => (bidRoomUserService.GetBidRoomUsers()).Result)
            //}) ; 
        }
    }
}
