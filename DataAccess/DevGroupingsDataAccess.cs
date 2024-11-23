using DataModel.InputModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace DataAccess
{
    public class DevGroupingsDataAccess
    {

        public static DataTable SelDevGroupings(string ConnectionString)
        {
            try
            {
                DataSet ds = null;

                ds = SQLHelper.SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "[budget_input].[SelDevGroupings]");
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
            }
            return null;
        }

        public static DataTable SelDevGroupingsByGroupingID(string ConnectionString, int Dev_Groupings_Id)
        {
            try
            {
                DataSet ds = null;

                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@Dev_Groupings_Id", Dev_Groupings_Id)
                                                };

                ds = SQLHelper.SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "[budget_input].[SelDevGroupingsByGroupingID]", paramsArray);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
            }
            return null;
        }

        public static int InsUpdDevGroupings(string connectionString, DevGroupingsInput devGroupingsInput, DateTime currentDatetime)
        {

            int Dev_Groupings_Id = 0;

            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@Dev_Groupings_Id", devGroupingsInput.Dev_Groupings_Id),
                                                new SqlParameter("@Dev_Grouping", devGroupingsInput.Dev_Grouping),
                                                new SqlParameter("@Dev_Comments_1", devGroupingsInput.Dev_Comments_1),
                                                new SqlParameter("@Dev_Comments_2", devGroupingsInput.Dev_Comments_2),
                                                new SqlParameter("@Dev_Comments_3", devGroupingsInput.Dev_Comments_3),
                                                new SqlParameter("@Dev_Comments_4", devGroupingsInput.Dev_Comments_4),
                                                new SqlParameter("@Asset_Area", devGroupingsInput.Asset_Area),
                                                new SqlParameter("@Row_Created_By", devGroupingsInput.LoggedInUserName),
                                                new SqlParameter("@Row_Created_Date", currentDatetime),
                                                new SqlParameter("@Row_Changed_By", devGroupingsInput.LoggedInUserName),
                                                new SqlParameter("@Row_Changed_Date", currentDatetime),
                                                new SqlParameter("@Manual_Override", string.IsNullOrEmpty(devGroupingsInput.Manual_Override) ? "N" : devGroupingsInput.Manual_Override),
                                                new SqlParameter("@Active_Ind", string.IsNullOrEmpty(devGroupingsInput.Active_Ind) ? "Y" : devGroupingsInput.Active_Ind)
                                                };

                Dev_Groupings_Id = Convert.ToInt32(SQLHelper.SqlHelper.ExecuteScalar(connectionString, CommandType.StoredProcedure, "[budget_input].[InsUpdDev_Groupings]", paramsArray));

                return Dev_Groupings_Id;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
                throw ex;
            }
        }


    }
}
