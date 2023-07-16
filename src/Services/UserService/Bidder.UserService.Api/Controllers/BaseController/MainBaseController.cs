using Bidder.UserService.Api.Helpers.Request;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Bidder.UserService.Api.Controllers.CustomBaseController
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public partial class MainBaseController : ControllerBase
    {
    }
}
