using Bidder.IdentityService.Application.Features.Commands.User.CreateUser;
using Bidder.IdentityService.Application.Interfaces.Repos;
using Bidder.IdentityService.Domain.DTOs;
using Bidder.IdentityService.Domain.DTOs.User.Request;
using Bidder.IdentityService.Domain.Entities;
using Bidder.IdentityService.Infastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bidder.IdentityService.Api.Controllers
{
    [ApiController]
    [Route("Identity/[action]")]
    public partial class IdentityController : ControllerBase
    {
        public IdentityController(IMediator mediator)
        {
            this.mediator = mediator; 
        }

        IMediator mediator { get; set; } 

        [HttpPost]
        [ActionName("CreateUser")]
        public async Task<ResponseMessageNoContent> CreateUser([FromBody] CreateUserRequest req)
        {
            var command = new CreateUserCommand(req);
            var result = await mediator.Send(command);
            return result;
        }

        //[HttpGet]
        //[ActionName("Test")]
        //public async void Test()
        //{
        //    Users user = new("ibrahimsabitkaya2@gmail","asdasd","ibo","kaya");
        //    using (var context = dbContext)
        //    {
        //        await context.Set<Users>().AddAsync(user);
        //        await context.SaveChangesAsync();
        //    }
        //}
    }
}