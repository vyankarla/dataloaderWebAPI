using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.InputModels
{
    public class HeaderInfoForEditStickSheetInput
    {
        public int Well_ID { get; set; }

        public string PROPNUM { get; set; }

        public string Well_Report_Name { get; set; }

        public string Drilling_Spacing_Unit { get; set; }

        public string Development_Group { get; set; }

        public decimal Type_Curve_Risk { get; set; }

        public decimal Planned_Drilled_Lateral_Length { get; set; }

        public decimal Planned_Completed_Lateral_Length { get; set; }

        public decimal PerfLateralLength { get; set; }

        public string LoggedInUserName { get; set; }

        public decimal? CustomNumber3 { get; set; }

        public decimal? CustomNumber4 { get; set; }

        public int LoggedInUserID { get; set; }

        public string Well_Type { get; set; }
    }
}
