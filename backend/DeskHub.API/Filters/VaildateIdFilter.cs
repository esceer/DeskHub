using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DeskHub.Backend.Api.Filters
{
    public class IdExistsFilterAttribute : ActionFilterAttribute
    {
        private readonly string _parameterName = "id";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.ContainsKey(_parameterName))
            {
                var id = context.ActionArguments[_parameterName] as Guid?;
                if (id == null)
                {
                    context.ModelState.AddModelError(_parameterName, $"{_parameterName} cannot be null or empty.");
                    context.Result = new BadRequestObjectResult(context.ModelState);
                }
            }
            else
            {
                // If the action doesn't have the parameter at all
                context.ModelState.AddModelError(_parameterName, $"Missing required parameter '{_parameterName}'.");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
