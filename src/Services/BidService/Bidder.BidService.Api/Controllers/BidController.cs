using AutoMapper;
using Bidder.BidService.Application.Features.Command.Bidding.CreateBid;
using Bidder.BidService.Domain.DTOs.Bidding.CreateBid;
using Bidder.Domain.Common.BaseClassess;
using Microsoft.AspNetCore.Mvc;

namespace Bidder.BidService.Api.Controllers
{
    public class BidController : BaseController
    {
        private readonly IMapper map; 
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

                throw;
            }
        }
    }
}
