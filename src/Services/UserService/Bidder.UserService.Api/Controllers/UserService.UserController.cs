using Bidder.UserService.Api.Controllers.CustomBaseController;
using Bidder.UserService.Api.Helpers.Request;
using Bidder.UserService.Api.Objects;
using Microsoft.AspNetCore.Mvc;

namespace Bidder.UserService.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : MainBaseController
    {
        [HttpPost]
        [ActionName("SignUp")]

        public Task<string> SignUp(RequestMessage message)
        {
            SignUpRequest signUpRequest = message.GetData<SignUpRequest>();

        }
    }
}
