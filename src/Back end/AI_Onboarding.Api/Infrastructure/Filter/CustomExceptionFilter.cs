using Microsoft.AspNetCore.Mvc.Filters;

namespace AI_Onboarding.Api.Filter
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger) { _logger = logger; }

        public void OnException(ExceptionContext filterContext)
        {
            var exceptionMessage = filterContext.Exception.Message;
            var stackTrace = filterContext.Exception.StackTrace;
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();
            var logDate = DateTime.Now;

            _logger.LogError("Date: {LogDate}, Controller: { ControllerName}, Action: {ActionName}, Error Message: {ExceptionMessage}, Stack Trace: {StackTrace}", logDate, controllerName, actionName, exceptionMessage, stackTrace);
        }
    }
}



