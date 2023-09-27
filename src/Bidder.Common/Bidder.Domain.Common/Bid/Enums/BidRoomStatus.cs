namespace Bidder.Domain.Common.Bid.Enums
{
    public enum BidRoomStatus
    {
        NeverCreated = 0,
        BidCreatedButBidRoomNotCreated= 1,
        Created = 2,
        Started = 3,
        Finished = 4,
        Canceled = 5
    }
}
