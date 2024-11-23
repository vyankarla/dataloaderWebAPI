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
    public class TypeCurveAssignmentDataAccess
    {

        public static DataTable SelTypeCurveAssignments(string ConnectionString)
        {
            try
            {
                DataSet ds = null;

                ds = SQLHelper.SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "[tc].[SelTypeCurveAssignments]");
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
            }
            return null;
        }

        public static DataTable SelDistinctTypeCurves(string ConnectionString)
        {
            try
            {
                DataSet ds = null;

                ds = SQLHelper.SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "[tc].[SelDistinctTypeCurves]");
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
            }
            return null;
        }

        public static int UpdTypeCurveAssignmentByAssignmentID(string connectionString, UpdTypeCurveAssignmentInput updTypeCurveAssignmentInput)
        {

            int rows = 0;

            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@DSU_TC_Assignment_ID", updTypeCurveAssignmentInput.DSU_TC_Assignment_ID),
                                                new SqlParameter("@Type_Curve_Name", updTypeCurveAssignmentInput.Type_Curve_Name),
                                                new SqlParameter("@Risk_Factor", updTypeCurveAssignmentInput.Risk_Factor),
                                                new SqlParameter("@Row_Changed_By", updTypeCurveAssignmentInput.Row_Changed_By)
                                                };

                rows = SQLHelper.SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, "[tc].[UpdTypeCurveAssignmentByAssignmentID]", paramsArray);
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
