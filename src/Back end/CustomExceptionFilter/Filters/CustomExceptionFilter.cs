using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data.SqlClient;
using Custom_Exception_Filter.Filters;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;


namespace Custom_Exception_Filter.CustomExceptionFilter
{
    public class CustomExceptionFilter : Filters.IExceptionFilter
    {
        private const string TableName = "Logs";

        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                var exceptionMessage = filterContext.Exception.Message;
                var stackTrace = filterContext.Exception.StackTrace;
                var controllerName = filterContext.RouteData.Values["controller"].ToString();
                var actionName = filterContext.RouteData.Values["action"].ToString();


                LogExceptionToDatabase(DateTime.Now, controllerName, actionName, exceptionMessage, stackTrace);


                filterContext.ExceptionHandled = true;
            }
        }

        private void LogExceptionToDatabase(DateTime logDate, string controllerName, string actionName, string exceptionMessage, string stackTrace)
        {

            try
            {
               
                Log.Error("Date: {Logdate}, Controller: { ControllerName}, Action: {ActionName}, Error Message: {ExceptionMessage}, Stack Trace: {StackTrace}", logDate, controllerName, actionName, exceptionMessage, stackTrace);
                
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error while logging exception data to the database!");
            }
            
        }

        

    }
}
