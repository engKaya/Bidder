using AutoMapper;
using Bidder.BidService.Application.Features.Command.Bidding.CreateBid;
using Bidder.BidService.Domain.DTOs.Bidding.CreateBid;
using Bidder.Domain.Common.BaseClassess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bidder.BidService.Api.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BidController : BaseController
    {
        private readonly IMapper map; 
        private readonly ILogger<BidController> logger;
        public BidController(IMapper map)
        {
            this.map = map;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage<CreateBidResponse>), 200)]
        [ProducesResponseType(typeof(ResponseMessage<CreateBidResponse>), 500)]
        public async Task<ResponseMessage<CreateBidResponse>> CreateBid([FromBody] CreateBidRequest request)
        {
            try
            {
                var command = map.Map<CreateBidCommand>(request);
                return await mediator.Send(command);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.StackTrace);
                return ResponseMessage<CreateBidResponse>.Fail("AUCTION_CREATION_FAILED", 500, new List<string>() { ex.Message, ex.StackTrace });
            }
        }
    }
}
