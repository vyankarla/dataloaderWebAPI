using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public enum ProjectType
    {
        DAL,
        BLL,
        Services,
        WebAPI
    }

    public class IRExceptionHandler
    {
        /// <summary>
        /// Handles exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static void HandleException(ProjectType pType, Exception ex)
        {
            try
            {
                IRFileLogger.WriteEntry(pType.ToString(), ex.Message + Environment.NewLine + ex.StackTrace);
            }
            catch { }

            if (ex is System.Data.SqlClient.SqlException) // Sql Exceptions
            {
                // Handle more specific SqlException exception here.
                System.Data.SqlClient.SqlException exSql = (System.Data.SqlClient.SqlException)ex;
                switch (exSql.Errors[0].Number)
                {
                    case 547: // Foreign Key violation
                        throw new InvalidOperationException("Some helpful description", exSql);
                    //break;
                    case 2601: // Primary key violation
                        throw new Exception("Duplicate", exSql);
                    //break;
                    case 50000: // Primary key violation
                        throw new DuplicateDataException("Duplicate Record", exSql.Message);
                    default:
                        throw new Exception("Data Access Exception :" + exSql.Message, exSql);
                }
            }


            // On final production consume this error, do appropriate action
            throw ex;
        }

        public static string ProcessError(Exception ex, string location)
        {
            /*if (ex is DuplicateDataException)  // service throwing fault exception , need to review this
            {
                DuplicateDataException dex = (DuplicateDataException)ex;
                return dex.MessageInfo;
            }*/
            if (ex.Message.IndexOf("Duplicate") >= 0)
            {
                return "Duplicate Data"; // configurable
            }
            else
            {
                // log error
                IRFileLogger.WriteEntry(ProjectType.WebAPI.ToString(), location + ":" + ex.Message + Environment.NewLine + ex.StackTrace);
            }
            return ex.Message;
        }
    }

    public class DuplicateDataException : Exception
    {
        public string MessageInfo { get; set; }
        public string ErrorInfo { get; set; }
        public DuplicateDataException(string message, string errorInfo)
        {
            MessageInfo = message;
            ErrorInfo = errorInfo;
        }
    }
}
