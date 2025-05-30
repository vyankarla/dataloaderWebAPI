using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Management
{
    public class ForecastDailyVolumesService
    {
        public static DataTable SelWellsForForecastVolumes(string ccStagingDBConn, string projectName, string forecastName)
        {
            try
            {
                DataTable dt = ForecastDailyVolumesAccess.SelWellsForForecastVolumes(ccStagingDBConn, projectName, forecastName);

                return dt;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return null;
        }

        public static int InsUpdForecastDailyVolumes(string ccStagingDBConn, DataTable dtForecastDailyVolumes)
        {
            try
            {
                return ForecastDailyVolumesAccess.InsUpdForecastDailyVolumes(ccStagingDBConn, dtForecastDailyVolumes);
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
                throw ex;
            }
        }

        public static int InsUpdapiForecastOutputOnDemand(string ccStagingDBConn, DataTable dtForecastDailyVolumes)
        {
            try
            {
                return ForecastDailyVolumesAccess.InsUpdapiForecastOutputOnDemand(ccStagingDBConn, dtForecastDailyVolumes);
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
                throw ex;
            }
        }



    }
}
