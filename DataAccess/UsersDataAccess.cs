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


        public static DataTable SelUsersList(string ConnectionString)
        {
            try
            {
                DataSet ds = null;

                ds = SQLHelper.SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "[dataloader].[SelUsersList]");
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
            }
            return null;
        }


        public static int InsUpdUsers(string connectionString, UsersInput usersInput, DateTime currentDateTime, Boolean isAdmin)
        {
            int UserID = 0;
            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@UserID", usersInput.UserID),
                                                new SqlParameter("@Firstname", usersInput.Firstname),
                                                new SqlParameter("@Lastname", usersInput.Lastname),
                                                new SqlParameter("@Username", usersInput.Username),
                                                new SqlParameter("@Password", usersInput.Password),
                                                new SqlParameter("@isAdmin", isAdmin),
                                                new SqlParameter("@CreatedOnDt", currentDateTime),
                                                new SqlParameter("@CreatedBy", usersInput.LoggedInUserID)
                                                };

                UserID = Convert.ToInt32(SQLHelper.SqlHelper.ExecuteScalar(connectionString, CommandType.StoredProcedure, "[dataloader].[InsUpdUsers]", paramsArray));

            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
            }
            return UserID;
        }


    }
}
