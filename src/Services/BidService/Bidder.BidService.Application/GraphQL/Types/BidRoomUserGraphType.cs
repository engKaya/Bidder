using Bidder.BidService.Domain.Entities;
using GraphQL.Types;

namespace Bidder.BidService.Application.GraphQL.Types
{
    public class BidRoomUserGraphType :  ObjectGraphType<BidRoomUser>
    {
        public BidRoomUserGraphType()
        {
            Name = "BidRoomUser";
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from the bid object.");
            Field(x => x.BidRoomId).Description("BidRoomId property from the bid object.");
            Field(x => x.UserId).Description("UserId property from the bid object.");
            //Field(x => x.BidRoom, type: typeof(BidRoomGraphType)).Description("BidRoom property from the bid object."); 
        }
    }
}
