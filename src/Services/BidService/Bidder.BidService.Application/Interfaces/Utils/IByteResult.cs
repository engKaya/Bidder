namespace Bidder.BidService.Application.Interfaces.Utils
{
    public interface IByteResult : IDisposable
    {
        ArraySegment<byte> Result { get; }
    }
}
