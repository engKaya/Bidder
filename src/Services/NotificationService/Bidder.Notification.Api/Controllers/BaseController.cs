﻿using Bidder.IdentityService.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bidder.IdentityService.Api.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IMediator mediator => (IMediator)HttpContext.RequestServices.GetService(typeof(IMediator));

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
    }
}