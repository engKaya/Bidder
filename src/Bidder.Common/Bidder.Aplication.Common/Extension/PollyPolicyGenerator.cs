using Polly.Retry;
using Polly;
using Microsoft.Extensions.Logging;

namespace Bidder.Application.Common.Extension
{
    public static class PollyPolicyGenerator
    { 
        public static AsyncRetryPolicy CreateExceptionPolicy(ILogger logger)
        {
            return Policy
                .Handle<Exception>()
                .RetryAsync(5, (exception, count) =>
                {
                    logger.LogError(exception, "Error while getting bid room with Grpc");
                });
        }
    }
}
