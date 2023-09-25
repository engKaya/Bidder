using Bidder.Infastructure.Common.CustomEnum;

namespace Bidder.Infastructure.Common.Grpc
{
    public class GrpcServerType
    : Enumeration
    {
        public static GrpcServerType BiddingGrpcService = new(1, "https://localhost:5012"); 

        public GrpcServerType(int id, string name)
            : base(id, name)
        {
        }
    }
}
