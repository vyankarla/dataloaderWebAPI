using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.InputModels
{
    public class UpdTypeCurveOverrideNewInput
    {
        public int Well_ID { get; set; }

        public string Inventory_Milestone { get; set; }

        public string Package_review_Milestone { get; set; }

        public string Final_Tech_Review { get; set; }

        public string Pre_frac_Milestone { get; set; }

        public string Type_Curve_Override { get; set; }

        public string Row_Changed_By { get; set; }
    }
}
