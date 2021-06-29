using Microsoft.AspNetCore.Mvc.Filters;
using PackageChallenge.API.Filters.ValidationModels;

namespace PackageChallenge.API.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid) context.Result = new ValidationFailedResult(context.ModelState);
        }
    }
}