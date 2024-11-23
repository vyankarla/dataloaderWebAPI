using DataAccess;
using DataModel.BusinessObjects;
using DataModel.ExternalModels;
using DataModel.InputModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Management
{
    public class UsersService
    {
        public static List<LoginExtnl> ValidateUserCredentials(string connectionString, string Username, string Password)
        {
            try
            {
                DataTable dt = UsersDataAccess.ValidateUserCredentials(connectionString, Username, Password);

                List<LoginExtnl> loginExtnl = new List<LoginExtnl>();

                BusinessObjectParser.MapRowsToObject(dt, loginExtnl, "DataModel.ExternalModels.LoginExtnl",
                     new string[] { "UserID", "Firstname", "Lastname", "Username", "isAdmin" });

                return loginExtnl;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return null;
        }

        public static int UpdChangePassword(string connectionString, ChangePasswordInput changePasswordInput)
        {
            int rows = 0;
            try
            {
                rows = UsersDataAccess.UpdChangePassword(connectionString, changePasswordInput);
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
