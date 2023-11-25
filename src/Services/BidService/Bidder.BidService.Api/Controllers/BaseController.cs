using Bidder.Domain.Common.BaseClassess;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Bidder.BidService.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public partial class BaseController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator mediator => _mediator ?? HttpContext.RequestServices.GetRequiredService<IMediator>();  
        private IHttpContextAccessor httpContextAccessor => HttpContext?.RequestServices?.GetService<IHttpContextAccessor>();
        protected ActionResult Custom(ResponseMessageNoContent response)
        {
            if (response.StatusCode == (int)HttpStatusCode.OK)
                return new OkObjectResult(response);
            else if (response.StatusCode == (int)HttpStatusCode.NotFound)
                return new NotFoundObjectResult(response);
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
                return new UnauthorizedObjectResult(response);
            else if (response.StatusCode == (int)HttpStatusCode.InternalServerError)
                return StatusCode((int)HttpStatusCode.InternalServerError, response);
            else
                return new OkObjectResult(response);
        }

        protected Guid GetUserId()
        {
            var data = httpContextAccessor.HttpContext.User.FindFirst(x=>x.Type == ClaimTypes.UserData)?.Value;
            return data == null ? Guid.Empty : Guid.Parse(data);
        }

        protected string GetUserName()
        {
            var data = httpContextAccessor.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return data;
        }

        protected string GetUserEmail()
        {
            var data = httpContextAccessor.HttpContext.User.FindFirst(x => x.Type == ClaimTypes.Email)?.Value;
            return data;
        }
    }
}
