namespace Bidder.BidService.Application.Interfaces.Utils
{
    public interface IDocumentWriter
    {
        Task WriteAsync<T>(Stream stream, T value);

        Task<IByteResult> WriteAsync<T>(T value);

        [Obsolete("This method is obsolete and will be removed in the next major version.  Use WriteAsync instead.")]
        string Write(object value);
    }
}
