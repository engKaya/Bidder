using Bidder.BidService.Application.Interfaces.Utils;
using GraphQL;
using Newtonsoft.Json;
using System.Buffers;
using System.Text;

namespace Bidder.BidService.Infastructure.Utils
{
    public class DocumentWriter : IDocumentWriter
    {
        private readonly ArrayPool<byte> _pool = ArrayPool<byte>.Shared;
        private readonly int _maxArrayLength = 1048576;
        private readonly JsonSerializer _serializer;
        private static readonly Encoding Utf8Encoding = new UTF8Encoding(false);

        public DocumentWriter()
            : this(indent: false)
        {
        }

        public DocumentWriter(bool indent)
            : this(
                indent ? Formatting.Indented : Formatting.None,
                new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                })
        {
        }

        public DocumentWriter(Formatting formatting, JsonSerializerSettings settings)
        {
            _serializer = JsonSerializer.CreateDefault(settings);
            _serializer.Formatting = formatting;
        }

        public Task WriteAsync<T>(Stream stream, T value)
        {
            using (var writer = new StreamWriter(stream, Utf8Encoding, 1024, true))
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                _serializer.Serialize(jsonWriter, value);
            }

            return Task.CompletedTask;
        }

        public async Task<IByteResult> WriteAsync<T>(T value)
        {
            var pooledDocumentResult = new PooledByteResult(_pool, _maxArrayLength);
            var stream = pooledDocumentResult.Stream;
            try
            {
                await WriteAsync(stream, value).ConfigureAwait(false);
                pooledDocumentResult.InitResponseFromCurrentStreamPosition();
                return pooledDocumentResult;
            }
            catch (Exception)
            {
                pooledDocumentResult.Dispose();
                throw;
            }
        }

        public string Write(object value)
        {
            return this.WriteAsync((ExecutionResult)value)?.GetAwaiter().GetResult()?.ToString();
        }
    }
}
