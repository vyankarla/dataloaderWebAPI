using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ExternalModels
{
    public class HeaderInfoForEditStickSheetExtnl
    {

        public HeaderInfoForEditStickSheetExtnl()
        {
            Clilds = new List<HeaderInfoForEditStickSheetExtnl>();
        }

        public int Well_ID { get; set; }

        public string PROPNUM { get; set; }

        public string Well_Report_Name { get; set; }

        public string Drilling_Spacing_Unit { get; set; }

        public string Development_Group { get; set; }

        public double Type_Curve_Risk { get; set; }

        public Single Planned_Drilled_Lateral_Length { get; set; }

        public Single Planned_Completed_Lateral_Length { get; set; }

        public string Producing_Zone { get; set; }

        public string Well_Type { get; set; }

        public string Data_Source { get; set; }

        public string Study_Area { get; set; }

        public List<HeaderInfoForEditStickSheetExtnl> Clilds { get; set; }
    }

    public class HeaderInfoForEditStickSheetForApprovalExtnl
    {

        public HeaderInfoForEditStickSheetForApprovalExtnl()
        {
            Clilds = new List<HeaderInfoForEditStickSheetForApprovalExtnl>();
        }

        public int Well_ID { get; set; }

        public string PROPNUM { get; set; }

        public string Well_Report_Name { get; set; }

        public string Drilling_Spacing_Unit { get; set; }

        public string Development_Group { get; set; }

        public double? Type_Curve_Risk { get; set; }

        public Single? Planned_Drilled_Lateral_Length { get; set; }

        public Single? Planned_Completed_Lateral_Length { get; set; }

        public string Producing_Zone { get; set; }

        public string Well_Type { get; set; }

        public string Data_Source { get; set; }

        public string Study_Area { get; set; }

        public List<HeaderInfoForEditStickSheetForApprovalExtnl> Clilds { get; set; }
    }
}
