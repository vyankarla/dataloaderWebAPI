using DataAccess;
using DataModel.BusinessObjects;
using DataModel.ExternalModels;
using DataModel.InputModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Management
{
    public class DevGroupingsService
    {
        public static List<DevGroupingsExtnl> SelDevGroupings(string connectionString)
        {
            try
            {
                DataTable dt = DevGroupingsDataAccess.SelDevGroupings(connectionString);

                List<DevGroupingsExtnl> devGroupings = new List<DevGroupingsExtnl>();

                BusinessObjectParser.MapRowsToObject(dt, devGroupings, "DataModel.ExternalModels.DevGroupingsExtnl",
                     new string[] { "Dev_Groupings_Id", "Dev_Grouping", "Dev_Comments_1", "Dev_Comments_2", "Dev_Comments_3",
                     "Dev_Comments_4", "Asset_Area", "Row_Created_By", "Row_Created_Date", "Row_Changed_By", "Row_Changed_Date",
                     "Row_Archived_By", "Row_Archived_Date", "Manual_Override", "Active_Ind" });

                return devGroupings;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return null;
        }

        public static List<DevGroupingsExtnl> SelDevGroupingsByGroupingID(string connectionString, int Dev_Groupings_Id)
        {
            try
            {
                DataTable dt = DevGroupingsDataAccess.SelDevGroupingsByGroupingID(connectionString, Dev_Groupings_Id);

                List<DevGroupingsExtnl> devGroupings = new List<DevGroupingsExtnl>();

                BusinessObjectParser.MapRowsToObject(dt, devGroupings, "DataModel.ExternalModels.DevGroupingsExtnl",
                     new string[] { "Dev_Groupings_Id", "Dev_Grouping", "Dev_Comments_1", "Dev_Comments_2", "Dev_Comments_3",
                     "Dev_Comments_4", "Asset_Area", "Row_Created_By", "Row_Created_Date", "Row_Changed_By", "Row_Changed_Date",
                     "Row_Archived_By", "Row_Archived_Date", "Manual_Override", "Active_Ind" });

                return devGroupings;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return null;
        }

        public static int InsUpdDevGroupings(string connectionString, DevGroupingsInput devGroupingsInput)
        {
            int Dev_Groupings_Id = 0;
            try
            {
                Dev_Groupings_Id = DevGroupingsDataAccess.InsUpdDevGroupings(connectionString, devGroupingsInput, DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
                throw ex;
            }
            return Dev_Groupings_Id;
        }



    }
}
