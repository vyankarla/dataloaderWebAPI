using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Management
{
    public class CommonServiceMethods
    {
        public static int ExecuteNonQueryWithoutParams(string connectionString, string procedureName)
        {
            int rows = 0;
            try
            {
                rows = CommonAccessMethods.ExecuteNonQueryWithoutParams(connectionString, procedureName);
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
                throw ex;
            }
            return rows;
        }
    }
}
