using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data.SqlClient;
using AI_Onboarding.Api.Filter;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Core;


namespace AI_Onboarding.Api.Filter
{



    public class CustomExceptionFilter : IExceptionFilter.IExceptionFilter
    {

        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger) {  _logger = logger; }

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



