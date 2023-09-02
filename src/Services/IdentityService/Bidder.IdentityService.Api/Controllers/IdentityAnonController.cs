using Bidder.Domain.Common.BaseClassess;
using Bidder.IdentityService.Application.Features.Commands.User.CreateUser;
using Bidder.IdentityService.Application.Features.Queries.User.Login;
using Bidder.IdentityService.Domain.DTOs.User.Request;
using Bidder.IdentityService.Domain.DTOs.User.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Bidder.IdentityService.Api.Controllers
{ 
    public partial class IdentityController : BaseController
    {  
        [HttpPost]
        [ActionName("CreateUser")]
        [ProducesResponseType(typeof(ResponseMessageNoContent), 200)] 
        [ProducesResponseType(typeof(ResponseMessageNoContent), 500)]
        public async Task<ActionResult<ResponseMessageNoContent>> CreateUser([FromBody] CreateUserRequest req)
        {
            var command = new CreateUserCommand(req);
            var result = await mediator.Send(command);
            return Custom(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage<LoginResponse>), 200)]
        [ProducesResponseType(typeof(ResponseMessage<LoginResponse>), 403)]
        [ProducesResponseType(typeof(ResponseMessage<LoginResponse>), 500)]
        [ActionName("Login")]
        public async Task<ActionResult<ResponseMessage<LoginResponse>>> Login([FromBody] LoginRequest req)
        {
            var query = new LoginQuery(req);
            var result = await mediator.Send(query); 
            return Custom(result);
        }
    }
}