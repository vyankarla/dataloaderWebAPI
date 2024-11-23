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
    public class TypeCurveOverrideService
    {
        public static List<HeaderInfoExtnl> SelWellHeadersInfo(string connectionString)
        {
            try
            {
                DataTable dt = TypeCurveOverrideDataAccess.SelWellHeadersInfo(connectionString);

                List<HeaderInfoExtnl> headerInfoExtnls = new List<HeaderInfoExtnl>();

                BusinessObjectParser.MapRowsToObject(dt, headerInfoExtnls, "DataModel.ExternalModels.HeaderInfoExtnl",
                     new string[] { "Well_ID", "Data_Source", "Operator", "Original_Operator", "Well_Report_Name",
                     "Well_Report_Name_Short", "Well_Number", "Well_Type", "Acquisition_Well", "Acquired_From", "Acquisition_Date", "Sold",
                     "Sold_To", "Sold_Date", "Current_Well_Status", "Current_Well_Status_Date", "Lateral_Length", "Lateral_Length_Method",
                     "Reservoir_Area", "Well_Class", "Well_Subclass", "Drilling_Spacing_Unit", "Type_Curve_Override", "Date_Drill_Start",
                     "Date_Drill_End", "Date_Frac_Start", "Date_Frac_End", "Date_First_Prod", "Drilling_Rig", "Completions_Crew",
                     "Elev_Datum", "Elevation", "Depth_Max_Md", "Depth_Avg_Tvd", "Depth_Max_Tvd", "Pad_Name", "Pad_Pop_Order", "Facility_Name",
                     "Well_Data_Category", "Lease_Well_Count", "Reserves_Project_Id", "Reservoir_Engineer", "Area_Team", "Comments",
                     "Row_Created_By", "Row_Created_Date", "Row_Changed_By", "Row_Changed_Date", "Row_Archived_By", "Row_Archived_Date", "Active_Ind",
                     "Type_Curve_Milestone", "Type_Curve_Name" });

                return headerInfoExtnls;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return null;
        }

        public static int UpdTypeCurveOverrideByWellID(string connectionString, UpdTypeCurveOverrideInput updTypeCurveOverrideInput)
        {
            int rows = 0;
            try
            {
                Type_Curve_MilestonesInput type_Curve_MilestonesInput = new Type_Curve_MilestonesInput();
                type_Curve_MilestonesInput.Well_ID = updTypeCurveOverrideInput.WellID;
                type_Curve_MilestonesInput.Data_Source = "Web App";
                type_Curve_MilestonesInput.Type_Curve_Milestone = updTypeCurveOverrideInput.Type_Curve_Milestone;
                type_Curve_MilestonesInput.Type_Curve_Name = updTypeCurveOverrideInput.Type_Curve_Name;
                type_Curve_MilestonesInput.Comments = updTypeCurveOverrideInput.Comments;
                type_Curve_MilestonesInput.Row_Created_By = updTypeCurveOverrideInput.Row_Changed_By;
                type_Curve_MilestonesInput.Row_Created_Date = DateTime.UtcNow;
                type_Curve_MilestonesInput.Row_Changed_By = updTypeCurveOverrideInput.Row_Changed_By;
                type_Curve_MilestonesInput.Row_Changed_Date = DateTime.UtcNow;
                type_Curve_MilestonesInput.Active_Ind = "Y";

                rows = TypeCurveOverrideDataAccess.UpdTypeCurveOverrideByWellID(connectionString, updTypeCurveOverrideInput, type_Curve_MilestonesInput);
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
                throw ex;
            }
            return rows;
        }


    }
}
