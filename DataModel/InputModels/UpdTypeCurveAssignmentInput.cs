using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.InputModels
{
    public class UpdTypeCurveAssignmentInput
    {
        public int DSU_TC_Assignment_ID { get; set; }

        public decimal Risk_Factor { get; set; }

        public string Type_Curve_Name { get; set; }

        public string Row_Changed_By { get; set; }
    }
}
