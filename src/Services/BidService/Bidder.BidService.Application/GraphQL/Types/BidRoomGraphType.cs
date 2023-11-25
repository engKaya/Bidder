using Bidder.BidService.Domain.Entities;
using GraphQL.Types;

namespace Bidder.BidService.Application.GraphQL.Types
{
    public class BidRoomGraphType :   ObjectGraphType<BidRoom>
    {
        public BidRoomGraphType()
        {
            Name = "BidRoom";
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from the bid object.");
            Field(x => x.BidId).Description("BidId property from the bid object.");
            Field(x => x.Bid, type: typeof(BidGraphType)).Description("Bid property from the bid object."); 
            Field(x=>x.BidRoomUsers, type: typeof(ListGraphType<BidRoomUserGraphType>)).Description("");
        }
    }
}
