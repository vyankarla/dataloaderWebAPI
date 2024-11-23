using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.InputModels
{
    public class UpdTypeCurveOverrideInput
    {
        public int WellID { get; set; }

        public string Type_Curve_Override { get; set; }

        public string Row_Changed_By { get; set; }

        public string Type_Curve_Milestone { get; set; }

        public string Type_Curve_Name { get; set; }

        public string Comments { get; set; }
    }
}
