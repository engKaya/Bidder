using Bidder.Infastructure.Common.Grpc;
using Bidder.Infastructure.Common.Protos.Client;

namespace Grpc.UnitTests
{
    [TestClass]
    public class GrpcTest
    {
        [TestMethod]
        public void Bid_Room_Grpc_Should_Return_Least_Empty()
        {
            using var channel = GrpcClientFactory.GrpcChannelFactory(GrpcServerType.BiddingGrpcService);
            var client = new BidGrpcService.BidGrpcServiceClient(channel);

            var response = client.GetActiveBidRoomAsync(new Google.Protobuf.WellKnownTypes.Empty());
            Assert.IsNotNull(response);
        }
    }
}