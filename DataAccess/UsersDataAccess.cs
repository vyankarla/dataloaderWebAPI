using DataModel.InputModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Utilities;

namespace DataAccess
{
    public class UsersDataAccess
    {
        public static DataTable ValidateUserCredentials(string ConnectionString, string Username, string Password)
        {
            try
            {
                DataSet ds = null;


                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@Username", Username),
                                                new SqlParameter("@Password", Password)
                                                };

                ds = SQLHelper.SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "[dataloader].[ValidateUserCredentials]", paramsArray);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
            }
            return null;
        }

        public static int UpdChangePassword(string connectionString, ChangePasswordInput changePasswordInput)
        {

            int rows = 0;

            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@Username", changePasswordInput.Username),
                                                new SqlParameter("@NewPassword", changePasswordInput.NewPassword)
                                                };

                rows = SQLHelper.SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "[dataloader].[UpdChangePassword]", paramsArray);
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
                throw ex;
            }

            return rows;
        }


    }
}
