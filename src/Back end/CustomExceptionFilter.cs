using System;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace AI_Onboarding
{
    public class CustomExceptionFilter : FilterAttribute, IExceptionFilter
    {
        private const string TableName = "ErrorLog";

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
                string connectionString = "";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    
                    bool tableExists = CheckIfTableExists(connection, TableName);

                    if (!tableExists)
                    {
                        
                        CreateTable(connection, TableName);
                    }

                    
                    string sqlQuery = "INSERT INTO ErrorLog (LogDate, ControllerName, ActionName, ExceptionMessage, StackTrace) " +
                                      "VALUES (@LogDate, @ControllerName, @ActionName, @ExceptionMessage, @StackTrace)";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@LogDate", logDate);
                        command.Parameters.AddWithValue("@ControllerName", controllerName);
                        command.Parameters.AddWithValue("@ActionName", actionName);
                        command.Parameters.AddWithValue("@ErrorMessage", exceptionMessage);
                        command.Parameters.AddWithValue("@StackTrace", stackTrace);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while logging exception data to the table!");
            }
        }

        private bool CheckIfTableExists(SqlConnection connection, string tableName)
        {
            string sqlQuery = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @TableName";
            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@TableName", tableName);
                int tableCount = (int)command.ExecuteScalar();
                return (tableCount > 0);
            }
        }

        private void CreateTable(SqlConnection connection, string tableName)
        {
            string sqlQuery = @"CREATE TABLE " + tableName + @" (
                                LogId INT IDENTITY(1,1) PRIMARY KEY,
                                LogDate DATETIME,
                                ControllerName NVARCHAR(255),
                                ActionName NVARCHAR(255),
                                ErrorMessage NVARCHAR(MAX),
                                StackTrace NVARCHAR(MAX)
                            )";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}

