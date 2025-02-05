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
    public class TypeCurveOverrideDataAccess
    {

        public static DataTable SelWellHeadersInfo(string ConnectionString)
        {
            try
            {
                DataSet ds = null;

                ds = SQLHelper.SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "[Well].[SelWellHeadersInfo]");
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
            }
            return null;
        }

        public static int UpdTypeCurveOverrideByWellID(string connectionString, UpdTypeCurveOverrideInput updTypeCurveOverrideInput,
            Type_Curve_MilestonesInput type_Curve_MilestonesInput)
        {

            int rows = 0;

            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@WellID", updTypeCurveOverrideInput.WellID),
                                                new SqlParameter("@Type_Curve_Override", updTypeCurveOverrideInput.Type_Curve_Override),
                                                new SqlParameter("@Row_Changed_By", updTypeCurveOverrideInput.Row_Changed_By)
                                                };

                InsUpdType_Curve_Milestones(connectionString, type_Curve_MilestonesInput);

                rows = SQLHelper.SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "[Well].[UpdTypeCurveOverrideByWellID]", paramsArray);
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
                throw ex;
            }

            return rows;
        }

        public static int UpdTypeCurveOverrideByWellIDList(string connectionString, UpdTypeCurveOverrideInput updTypeCurveOverrideInput,
            List<Type_Curve_MilestonesInput> type_Curve_MilestonesInputs)
        {

            int rows = 0;

            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@WellID", updTypeCurveOverrideInput.WellID),
                                                new SqlParameter("@Type_Curve_Override", updTypeCurveOverrideInput.Type_Curve_Override),
                                                new SqlParameter("@Row_Changed_By", updTypeCurveOverrideInput.Row_Changed_By)
                                                };
                foreach (var type_Curve_MilestonesInput in type_Curve_MilestonesInputs)
                {
                    InsUpdType_Curve_Milestones(connectionString, type_Curve_MilestonesInput);
                }


                rows = SQLHelper.SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "[Well].[UpdTypeCurveOverrideByWellID]", paramsArray);
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
                throw ex;
            }

            return rows;
        }

        private static int InsUpdType_Curve_Milestones(string connectionString, Type_Curve_MilestonesInput type_Curve_MilestonesInput)
        {

            int rows = 0;

            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@Well_ID", type_Curve_MilestonesInput.Well_ID),
                                                new SqlParameter("@Data_Source", type_Curve_MilestonesInput.Data_Source),
                                                new SqlParameter("@Type_Curve_Milestone", type_Curve_MilestonesInput.Type_Curve_Milestone),
                                                new SqlParameter("@Type_Curve_Name", type_Curve_MilestonesInput.Type_Curve_Name),
                                                new SqlParameter("@Comments", type_Curve_MilestonesInput.Comments),
                                                new SqlParameter("@Row_Created_By", type_Curve_MilestonesInput.Row_Changed_By),
                                                new SqlParameter("@Row_Created_Date", type_Curve_MilestonesInput.Row_Created_Date),
                                                new SqlParameter("@Row_Changed_By", type_Curve_MilestonesInput.Row_Changed_By),
                                                new SqlParameter("@Row_Changed_Date", type_Curve_MilestonesInput.Row_Changed_Date),
                                                new SqlParameter("@Active_Ind", type_Curve_MilestonesInput.Active_Ind)
                                                };

                rows = SQLHelper.SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "[Well].[InsUpdType_Curve_Milestones]", paramsArray);
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
