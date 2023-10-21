using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Bidder.SignalR.Api.Controllers
{
    public class HealthController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public string Get()
        {
            return $"{Assembly.GetExecutingAssembly().GetName()} is running";
        }
    }
}
