using Grpc.Net.Client;

namespace Bidder.Infastructure.Common.Grpc
{
    public static class GrpcClientFactory
    {
        private static GrpcChannel _channel;
        public static GrpcChannel GrpcChannelFactory(GrpcServerType grpcServers)
        {
            _channel = GrpcChannel.ForAddress(grpcServers.Name); 
            return _channel;
        }
    }
}
