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
    public class ARIESMasterTablesAccess
    {
        public static DataTable SelARIESMasterTables(string ConnectionString)
        {
            try
            {
                DataSet ds = null;

                ds = SQLHelper.SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "[budget_input].[SelARIESMasterTables]");
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
            }
            return null;
        }

        public static int InsARIESMasterTablesEditBatchAndDetails(string connectionString,
            ARIES_Master_Tables_Edit_BatchInput aries_Master_Tables_Edit_BatchInput, DateTime CurrentUtcDateTime,
            List<ARIES_Master_Tables_Edit_DetailsInput> updARIESMasterTablesInputs, string Batch_Type, Boolean ExportFlag)
        {

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();

            int Batch_id = 0;

            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@Batch_id", aries_Master_Tables_Edit_BatchInput.Batch_id),
                                                new SqlParameter("@Batch_Timestamp", aries_Master_Tables_Edit_BatchInput.Batch_Timestamp),
                                                new SqlParameter("@Row_Created_By", aries_Master_Tables_Edit_BatchInput.Row_Created_By),
                                                new SqlParameter("@Row_Created_Date", aries_Master_Tables_Edit_BatchInput.Row_Created_Date),
                                                new SqlParameter("@Batch_Type", Batch_Type),
                                                new SqlParameter("@ExportFlag", ExportFlag)
                                                };

                Batch_id = Convert.ToInt32(SQLHelper.SqlHelper.ExecuteScalar(trans, CommandType.StoredProcedure, "[budget_input].[InsARIES_Master_Tables_Edit_Batch]", paramsArray));

                foreach (ARIES_Master_Tables_Edit_DetailsInput item in updARIESMasterTablesInputs)
                {
                    SqlParameter[] paramsArrayRow = new SqlParameter[]{
                                                new SqlParameter("@Well_ID", item.Well_ID),
                                                new SqlParameter("@PROPNUM", item.PROPNUM),
                                                new SqlParameter("@LEASE_CURRENT", item.LEASE_CURRENT),
                                                new SqlParameter("@LEASE_NEW", item.LEASE_NEW),
                                                new SqlParameter("@SCHEDULED_CURRENT", item.SCHEDULED_CURRENT),
                                                new SqlParameter("@SCHEDULED_NEW", item.SCHEDULED_NEW),
                                                new SqlParameter("@ONLINE_GROUPING_CURRENT", item.ONLINE_GROUPING_CURRENT),
                                                new SqlParameter("@ONLINE_GROUPING_NEW", item.ONLINE_GROUPING_NEW),
                                                new SqlParameter("@TC_AREA_CURRENT", item.TC_AREA_CURRENT),
                                                new SqlParameter("@TC_AREA_NEW", item.TC_AREA_NEW),
                                                new SqlParameter("@TYPECURVE_CURRENT", item.TYPECURVE_CURRENT),
                                                new SqlParameter("@TYPECURVE_NEW", item.TYPECURVE_NEW),
                                                new SqlParameter("@TC_CODE_CURRENT", item.TC_CODE_CURRENT),
                                                new SqlParameter("@TC_CODE_NEW", item.TC_CODE_NEW),
                                                new SqlParameter("@TYPECURVE_RISK_CURRENT", item.TYPECURVE_RISK_CURRENT),
                                                new SqlParameter("@TYPECURVE_RISK_NEW", item.TYPECURVE_RISK_NEW),
                                                new SqlParameter("@PLANNED_DLL_CURRENT", item.PLANNED_DLL_CURRENT),
                                                new SqlParameter("@PLANNED_DLL_NEW", item.PLANNED_DLL_NEW),
                                                new SqlParameter("@PLANNED_CLL_CURRENT", item.PLANNED_CLL_CURRENT),
                                                new SqlParameter("@PLANNED_CLL_NEW", item.PLANNED_CLL_NEW),
                                                new SqlParameter("@Batch_id", Batch_id),
                                                new SqlParameter("@Row_Created_By", item.Row_Created_By),
                                                new SqlParameter("@Row_Created_Date", CurrentUtcDateTime)
                                                };

                    int rows = SQLHelper.SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "[budget_input].[InsARIES_Master_Tables_Edit_Details]", paramsArrayRow);

                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
                throw ex;
            }

            return Batch_id;
        }

        public static int InsARIESMasterTablesDropBatchAndDetails(string connectionString,
           ARIES_Master_Tables_Edit_BatchInput aries_Master_Tables_Edit_BatchInput, DateTime CurrentUtcDateTime,
           List<ARIES_Master_Tables_Drop_DetailsInput> updARIESMasterTablesInputs, string Batch_Type, Boolean ExportFlag)
        {

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();

            int Batch_id = 0;

            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@Batch_id", aries_Master_Tables_Edit_BatchInput.Batch_id),
                                                new SqlParameter("@Batch_Timestamp", aries_Master_Tables_Edit_BatchInput.Batch_Timestamp),
                                                new SqlParameter("@Row_Created_By", aries_Master_Tables_Edit_BatchInput.Row_Created_By),
                                                new SqlParameter("@Row_Created_Date", aries_Master_Tables_Edit_BatchInput.Row_Created_Date),
                                                new SqlParameter("@Batch_Type", Batch_Type),
                                                new SqlParameter("@ExportFlag", ExportFlag)
                                                };

                Batch_id = Convert.ToInt32(SQLHelper.SqlHelper.ExecuteScalar(trans, CommandType.StoredProcedure, "[budget_input].[InsARIES_Master_Tables_Edit_Batch]", paramsArray));

                foreach (ARIES_Master_Tables_Drop_DetailsInput item in updARIESMasterTablesInputs)
                {
                    SqlParameter[] paramsArrayRow = new SqlParameter[]{
                                                new SqlParameter("@Well_ID", item.Well_ID),
                                                new SqlParameter("@PROPNUM", item.PROPNUM),
                                                new SqlParameter("@DropComments", item.Comments),
                                                new SqlParameter("@Batch_id", Batch_id),
                                                new SqlParameter("@Row_Created_By", item.Row_Created_By),
                                                new SqlParameter("@Row_Created_Date", CurrentUtcDateTime)
                                                };

                    int rows = SQLHelper.SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "[budget_input].[InsARIES_Master_Tables_Drop_Details]", paramsArrayRow);

                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
                throw ex;
            }

            return Batch_id;
        }

        public static DataTable SelARIES_Master_Tables_Batchs(string connectionString, string Batch_Type)
        {
            try
            {
                DataSet ds = null;

                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@Batch_Type", Batch_Type == string.Empty ? Convert.DBNull : Batch_Type)
                                                };

                ds = SQLHelper.SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, "[budget_input].[SelARIES_Master_Tables_Batchs]", paramsArray);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
            }
            return null;
        }

        public static DataTable SelARIES_Master_Tables_Edit_DetailsByBatchId(string connectionString, int Batch_id)
        {
            try
            {
                DataSet ds = null;

                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@Batch_id", Batch_id)
                                                };

                ds = SQLHelper.SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, "[budget_input].[SelARIES_Master_Tables_Edit_DetailsByBatchId]", paramsArray);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
            }
            return null;
        }

        public static int UpdExportFlagByBatch_id(string connectionString, UpdExportFlag updExportFlag, Boolean ExportFlag)
        {

            int rows = 0;

            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@Batch_id", updExportFlag.Batch_id),
                                                new SqlParameter("@ExportFlag", ExportFlag)
                                                };

                rows = SQLHelper.SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "[budget_input].[UpdExportFlagByBatch_id]", paramsArray);
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
                throw ex;
            }

            return rows;
        }

        public static DataTable SelARIESDataForEditBatchNew(string connectionString)
        {
            try
            {
                DataSet ds = null;
                ds = SQLHelper.SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, "[well].[SelARIESDataForEditBatchNew]");
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
            }
            return null;
        }

        public static DataTable SelARIESDataForEditBatchNewForApproval(string connectionString)
        {
            try
            {
                DataSet ds = null;

                ds = SQLHelper.SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, "[well].[SelARIESDataForEditBatchNewForApproval]");
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
            }
            return null;
        }

        public static int UpdHeaderForEditBatchNew(string connectionString, List<HeaderInfoForEditStickSheetInput> updARIESMasterTablesInputs, DateTime CurrentUtcDateTime)
        {

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            int rows = 0;

            try
            {
                foreach (HeaderInfoForEditStickSheetInput item in updARIESMasterTablesInputs)
                {
                    SqlParameter[] paramsArrayRow = new SqlParameter[]{
                                                new SqlParameter("@Well_ID", item.Well_ID),
                                                new SqlParameter("@Well_Report_Name", item.Well_Report_Name),
                                                new SqlParameter("@Drilling_Spacing_Unit", item.Drilling_Spacing_Unit),
                                                new SqlParameter("@Development_Group", item.Development_Group),
                                                new SqlParameter("@Type_Curve_Risk", item.Type_Curve_Risk),
                                                new SqlParameter("@Planned_Drilled_Lateral_Length", item.Planned_Drilled_Lateral_Length),
                                                new SqlParameter("@Planned_Completed_Lateral_Length", item.Planned_Completed_Lateral_Length),
                                                new SqlParameter("@LoggedInUserName", item.LoggedInUserName),
                                                new SqlParameter("@Well_Type", item.Well_Type)
                                                };

                    rows = rows + SQLHelper.SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "[well].[UpdHeader]", paramsArrayRow);

                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
                throw ex;
            }

            return rows;
        }

        public static int UpdDataSourceToIMByWellID(string connectionString, List<UpdDataSourceByWellIDInput> updDataSourceByWellIDInputs, DateTime CurrentUtcDateTime)
        {

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();
            int rows = 0;

            try
            {
                foreach (UpdDataSourceByWellIDInput item in updDataSourceByWellIDInputs)
                {
                    SqlParameter[] paramsArrayRow = new SqlParameter[]{
                                                new SqlParameter("@Well_ID", item.Well_ID),
                                                new SqlParameter("@CurrentUtcDateTime", CurrentUtcDateTime),
                                                new SqlParameter("@LoggedInUserID", item.LoggedInUserID),
                                                new SqlParameter("@LoggedInUserName", item.LoggedInUserName)
                                                };

                    rows = rows + SQLHelper.SqlHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "[well].[UpdDataSourceToIMByWellID]", paramsArrayRow);

                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
                throw ex;
            }

            return rows;
        }

        public static DataTable SelWellDataForCCByWellID(string connectionString, int Well_ID)
        {
            try
            {
                DataSet ds = null;
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@Well_ID", Well_ID)
                                                };

                ds = SQLHelper.SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, "[well].[SelWellDataForCCByWellID]", paramsArray);
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

