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
    public class DSUHeaderAccess
    {
        public static DataTable SelDSUHeaders(string ConnectionString)
        {
            try
            {
                DataSet ds = null;

                ds = SQLHelper.SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "[nriwi].[SelDSUHeaders]");
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
            }
            return null;
        }

        public static DataTable SelDSUHeaderHistoryByDSUHeaderId(string ConnectionString, int DSU_Header_Id)
        {
            try
            {
                DataSet ds = null;

                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@DSU_Header_Id", DSU_Header_Id)
                                                };

                ds = SQLHelper.SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "[nriwi].[SelDSUHeaderHistoryByDSUHeaderId]", paramsArray);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
            }
            return null;
        }


        public static DataTable SelWellsInDSUByDSUHeaderID(string ConnectionString, int DSU_Header_Id)
        {
            try
            {
                DataSet ds = null;

                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@DSU_Header_Id", DSU_Header_Id)
                                                };

                ds = SQLHelper.SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "[nriwi].[SelWellsInDSUByDSUHeaderID]", paramsArray);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
            }
            return null;
        }


    }
}
