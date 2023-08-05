using Bidder.IdentityService.Application.Interfaces.Repos;
using Bidder.IdentityService.Domain.DTOs.User.Request;
using Bidder.IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bidder.IdentityService.Api.Controllers
{
    [ApiController]
    [Route("Identity/[action]")]
    public partial class ÝdentityController : ControllerBase
    {
        public ÝdentityController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        IUnitOfWork UnitOfWork { get; set; }
         

        [HttpPost]
        [ActionName("CreateUser")]
        public async Task<Users> CreateUser([FromBody] CreateUserRequest user)
        {
            return await UnitOfWork.UserRepository.FindFirst(x => x.Email == user.Email);
        } 
    }
}