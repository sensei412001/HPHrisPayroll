using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HPHrisPayroll.API.Helper
{
    public class ValidateMyModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            if (!context.ModelState.IsValid)
            {

                context.Result = new BadRequestObjectResult(context.ModelState); // it returns 400 with the error

            }

        }
    }
}