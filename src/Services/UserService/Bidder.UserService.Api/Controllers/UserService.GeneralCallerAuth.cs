using Bidder.UserService.Api.Controllers.CustomBaseController;
using Bidder.UserService.Api.Helpers.Request;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Bidder.UserService.Api.Controllers
{
    public class GeneralCallerController : MainBaseController
    {
        [HttpPost]
        public object? CallerPost(RequestMessage message)
        {
            if (message == null || string.IsNullOrEmpty(message.Operation)) return null;

            var actionInfo = GetActionInfo(message);
            Type type = Type.GetType(actionInfo[1]);

            Object obj = Activator.CreateInstance(type);


            MethodInfo methodInfo = type.GetMethod(actionInfo[0]);

            if (methodInfo == null)
                return null;

            // Invoke the method on the instance we created above
            return methodInfo.Invoke(obj, new object[] { message });

        }

        private string[] GetActionInfo(RequestMessage message)
        {
            string action = string.Empty;
            string controllerClass = string.Empty;
            string[] stringArray = message.Operation.Split('.');

            action = stringArray[stringArray.Length - 1];
            controllerClass = string.Join(".", stringArray.SkipLast(1));

            return new string[] { action, controllerClass };
        }
    }
}
