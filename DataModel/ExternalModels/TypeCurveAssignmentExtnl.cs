using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ExternalModels
{
    public class TypeCurveAssignmentExtnl
    {
        public int DSU_TC_Assignment_ID { get; set; }

        public string Drilling_Spacing_Unit { get; set; }

        public string Producing_Zone { get; set; }

        public string Type_Curve_Name { get; set; }

        public decimal? Risk_Factor { get; set; }

        public string Row_Changed_By { get; set; }

        public DateTime Row_Changed_Date { get; set; }
    }
}
