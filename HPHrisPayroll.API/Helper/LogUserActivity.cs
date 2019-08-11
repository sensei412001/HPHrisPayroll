using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HPHrisPayroll.API.Helper
{
    public class LogUserActivity: IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            string username = resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // var logRepo = resultContext.HttpContext.RequestServices.GetService<ILogActivityRepo>();
            // var user = await repo.get(appId);

            string controllerName = resultContext.RouteData.Values["controller"].ToString();
            string methodName = resultContext.RouteData.Values["action"].ToString();

            string ip = resultContext.HttpContext.Connection.RemoteIpAddress.ToString();
            
            // AuditLogs auditLog = new AuditLogs() {
            //     UserId = userId,
            //     Ipaddress = ip,
            //     ControllerName = controllerName,
            //     Method = methodName,
            //     DateAccessed = DateTime.Now
            // };
            // logRepo.Add(auditLog);
            // await logRepo.SaveAll();
        }
        
    }
}