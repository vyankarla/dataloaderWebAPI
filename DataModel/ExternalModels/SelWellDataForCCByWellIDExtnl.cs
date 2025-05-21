using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ExternalModels
{
    public class SelWellDataForCCByWellIDExtnl
    {
        public string Well_Official_Name { get; set; }

        public string DSU_Prod_Zone_Assignment { get; set; }

        public string Dev_Grouping { get; set; }

        public decimal Type_Curve_Risk { get; set; }

        public decimal Planned_Drilled_Lateral_Length { get; set; }

        public decimal Planned_Completed_Lateral_Length { get; set; }

        public decimal PerfLateralLength { get; set; }

        public decimal CustomNumber3 { get; set; }

        public decimal CustomNumber4 { get; set; }

        public string Well_type { get; set; }
    }
}
