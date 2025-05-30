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
    public class ForecastDailyVolumesAccess
    {
        public static DataTable SelWellsForForecastVolumes(string ccStagingDBConn, string projectName, string forecastName)
        {
            try
            {
                DataSet ds = null;

                SqlParameter[] paramsArray = new SqlParameter[]{
                                                    new SqlParameter("@projectName", projectName),
                                                    new SqlParameter("@forecastName", forecastName)
                                                    };

                ds = SQLHelper.SqlHelper.ExecuteDataset(ccStagingDBConn, CommandType.StoredProcedure, "[ComboCurve].[SelWellsForForecastVolumes]", paramsArray);
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
            }
            return null;
        }

        public static int InsUpdForecastDailyVolumes(string ccStagingDBConn, DataTable dtForecastDailyVolumes)
        {
            int rows = 0;
            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                    new SqlParameter("@Type_apiDailyForecastVolumes", dtForecastDailyVolumes)
                                                    };

                rows = SQLHelper.SqlHelper.ExecuteNonQuery(ccStagingDBConn, CommandType.StoredProcedure, "[ComboCurve].[InsUpdForecastDailyVolumes]", paramsArray);
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
            }

            return rows;
        }

        public static int InsUpdapiForecastOutputOnDemand(string ccStagingDBConn, DataTable dtForecastOutputExtnls)
        {
            int rows = 0;
            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                    new SqlParameter("@udt_apiForecastOutputOnDemand", dtForecastOutputExtnls)
                                                    };

                rows = SQLHelper.SqlHelper.ExecuteNonQuery(ccStagingDBConn, CommandType.StoredProcedure, "[ComboCurve].[InsUpdForecastOutputData]", paramsArray);
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
            }

            return rows;
        }

    }
}
