using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Bidder.SignalR.Application.AuctionHub
{
    public class BidHub  : Hub
    {
        private readonly ILogger<BidHub> logger;

        public BidHub(ILogger<BidHub> _logger) : base()
        {
            this.logger = _logger;
        }

        public override Task OnConnectedAsync()
        {
            logger.LogInformation("Client connected", Context.ToString());
            return base.OnConnectedAsync();
        }
    }
}
