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
    public class DSUSwimLanesAccess
    {
        public static DataTable SelDSUSwimLanesByDSUHeaderID(string ConnectionString, int DSUHeaderID)
        {
            try
            {
                DataSet ds = null;

                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@DSU_Header_Id", DSUHeaderID)
                                                };

                ds = SQLHelper.SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "[nriwi].[SelDSUSwimLanesByDSUHeaderID]", paramsArray);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
            }
            return null;
        }

        public static DataTable SelSWIMLaneOwnershipPivotView(string ConnectionString, int DSUHeaderID)
        {
            try
            {
                DataSet ds = null;

                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@DSU_Header_Id", DSUHeaderID)
                                                };

                ds = SQLHelper.SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "[nriwi].[SelSWIMLaneOwnershipPivotView]", paramsArray);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
            }
            return null;
        }


        public static DataTable SelDSUProducinigZonesByDSUHeaderID(string ConnectionString, int DSUHeaderID)
        {
            try
            {
                DataSet ds = null;

                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@DSU_Header_Id", DSUHeaderID)
                                                };

                ds = SQLHelper.SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "[nriwi].[SelDSUProducinigZonesByDSUHeaderID]", paramsArray);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
            }
            return null;
        }

        public static int UpdConfidenceLevelDSUHeaderByDSUHeaderID(string connectionString, int DSU_Header_Id, int Confidence_Level, string Comments,
            int LoggedInUserID, string LoggedInUserName)
        {

            int rows = 0;

            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@DSU_Header_Id", DSU_Header_Id),
                                                new SqlParameter("@Confidence_Level", Confidence_Level),
                                                new SqlParameter("@Comments", Comments),
                                                new SqlParameter("@LoggedInUserID", LoggedInUserID),
                                                new SqlParameter("@LoggedInUserName", LoggedInUserName)
                                                };

                rows = SQLHelper.SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "[nriwi].[UpdConfidenceLevelDSUHeaderByDSUHeaderID]", paramsArray);
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
                throw ex;
            }

            return rows;
        }

        public static int UpdDSU_Swim_Lane_OwnershipByProducingZoneAndSwimLaneID(string connectionString, List<Swim_Lane_OwnershipInput> swim_Lane_OwnershipInput,
            int LoggedInUserID, string LoggedInUserName)
        {

            int rows = 0;

            try
            {
                foreach (var item in swim_Lane_OwnershipInput)
                {
                    SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@DSU_Header_ID", item.DSU_Header_ID),
                                                new SqlParameter("@DSU_Swim_Lane_Number", item.DSU_Swim_Lane_Number),
                                                new SqlParameter("@Producing_Zone", item.Producing_Zone),
                                                new SqlParameter("@BPO_WI", item.BPO_WI == null ? Convert.DBNull : item.BPO_WI),
                                                new SqlParameter("@BPO_NRI", item.BPO_NRI == null ? Convert.DBNull : item.BPO_NRI),
                                                new SqlParameter("@APO_WI", item.APO_WI == null ? Convert.DBNull : item.APO_WI),
                                                new SqlParameter("@APO_NRI", item.APO_NRI == null ? Convert.DBNull : item.APO_NRI),
                                                new SqlParameter("@LoggedInUserID", LoggedInUserID),
                                                new SqlParameter("@LoggedInUserName", LoggedInUserName)
                                                };

                    rows = rows + SQLHelper.SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "[nriwi].[UpdDSU_Swim_Lane_OwnershipByProducingZoneAndSwimLaneID]", paramsArray);
                }
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
