using Bidder.IdentityService.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bidder.IdentityService.Api.Extensions
{
    public class ValidatorFilterAttr : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errs = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                if (errs.Any())
                {
                    context.Result = new BadRequestObjectResult(ResponseMessage<ResponseMessageNoContent>.Fail("Bad Request", 400, errs));
                }
            }
        }
    }
}
