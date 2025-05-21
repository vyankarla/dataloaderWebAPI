using DataAccess;
using DataModel.BusinessObjects;
using DataModel.ExternalModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Management
{
    public class DSUHeaderService
    {
        public static List<DSUHeadersExtnl> SelDSUHeaders(string connectionString)
        {
            try
            {
                DataTable dt = DSUHeaderAccess.SelDSUHeaders(connectionString);

                List<DSUHeadersExtnl> DSUHeadersExtnl = new List<DSUHeadersExtnl>();

                BusinessObjectParser.MapRowsToObject(dt, DSUHeadersExtnl, "DataModel.ExternalModels.DSUHeadersExtnl",
                     new string[] { "DSU_Header_Id", "Data_Source", "Drilling_Spacing_Unit", "Edited_By_Name", "Confidence_Level", "Comments",
                     "Row_Created_By", "Row_Created_Date", "Row_Changed_By", "Row_Changed_Date", "Row_Archived_By", "Row_Archived_Date", "Active_Ind"});

                return DSUHeadersExtnl;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return null;
        }

        public static List<DSUHeadersExtnlHistory> SelDSUHeaderHistoryByDSUHeaderId(string connectionString, int DSU_Header_Id)
        {
            try
            {
                DataTable dt = DSUHeaderAccess.SelDSUHeaderHistoryByDSUHeaderId(connectionString, DSU_Header_Id);

                List<DSUHeadersExtnlHistory> DSUHeadersExtnl = new List<DSUHeadersExtnlHistory>();

                BusinessObjectParser.MapRowsToObject(dt, DSUHeadersExtnl, "DataModel.ExternalModels.DSUHeadersExtnlHistory",
                     new string[] { "DSU_Header_Id", "Data_Source", "Drilling_Spacing_Unit", "Edited_By_Name", "Confidence_Level", "Comments",
                     "Version", "Row_Created_By", "Row_Created_Date", "Row_Changed_By", "Row_Changed_Date", "Row_Archived_By",
                     "Row_Archived_Date", "Active_Ind" });

                return DSUHeadersExtnl;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return null;
        }


    }
}
