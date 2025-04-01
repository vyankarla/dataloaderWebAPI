using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ExternalModels
{
    public class HeaderInfoExtnl
    {
        public int Well_ID { get; set; }

        public string Data_Source { get; set; }

        public string Operator { get; set; }

        public string Original_Operator { get; set; }

        public string Well_Report_Name { get; set; }

        public string Well_Report_Name_Short { get; set; }

        public string Well_Number { get; set; }

        public string Well_Type { get; set; }

        public string Acquisition_Well { get; set; }

        public string Acquired_From { get; set; }

        public DateTime? Acquisition_Date { get; set; }

        public string Sold { get; set; }

        public string Sold_To { get; set; }

        public DateTime? Sold_Date { get; set; }

        public string Current_Well_Status { get; set; }

        public DateTime? Current_Well_Status_Date { get; set; }

        public float? Lateral_Length { get; set; }

        public string Lateral_Length_Method { get; set; }

        public string Reservoir_Area { get; set; }

        public string Well_Class { get; set; }

        public string Well_Subclass { get; set; }

        public string Drilling_Spacing_Unit { get; set; }

        public string Type_Curve_Override { get; set; }

        public DateTime? Date_Drill_Start { get; set; }

        public DateTime? Date_Drill_End { get; set; }

        public DateTime? Date_Frac_Start { get; set; }

        public DateTime? Date_Frac_End { get; set; }

        public DateTime? Date_First_Prod { get; set; }

        public string Drilling_Rig { get; set; }

        public string Completions_Crew { get; set; }

        public string Elev_Datum { get; set; }

        public double? Elevation { get; set; }

        public float? Depth_Max_Md { get; set; }

        public float? Depth_Avg_Tvd { get; set; }

        public float? Depth_Max_Tvd { get; set; }

        public string Pad_Name { get; set; }

        public int? Pad_Pop_Order { get; set; }

        public string Facility_Name { get; set; }

        public string Well_Data_Category { get; set; }

        public int? Lease_Well_Count { get; set; }

        public string Reserves_Project_Id { get; set; }

        public string Reservoir_Engineer { get; set; }

        public string Area_Team { get; set; }

        public string Comments { get; set; }

        public string Row_Created_By { get; set; }

        public DateTime? Row_Created_Date { get; set; }

        public string Row_Changed_By { get; set; }

        public DateTime? Row_Changed_Date { get; set; }

        public string Row_Archived_By { get; set; }

        public DateTime? Row_Archived_Date { get; set; }




        public string Active_Ind { get; set; }

        public string Type_Curve_Milestone { get; set; }

        public string Type_Curve_Name { get; set; }

        public string Inventory_Milestone { get; set; }

        public string Package_review_Milestone { get; set; }

        public string Final_Tech_Review { get; set; }

        public string Pre_frac_Milestone { get; set; }

        public string Type_Curve_Assignment { get; set; }

        public string DSU_Prod_Zone_Assignment { get; set; }

        public  string Producing_Zone { get; set; }
    }
}
