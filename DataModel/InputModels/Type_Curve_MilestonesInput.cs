using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.InputModels
{
    public class Type_Curve_MilestonesInput
    {
        public int Well_ID { get; set; }

        public string Data_Source { get; set; }

        public string Type_Curve_Milestone { get; set; }

        public string Type_Curve_Name { get; set; }

        public string Comments { get; set; }

        public string Row_Created_By { get; set; }

        public DateTime Row_Created_Date { get; set; }

        public string Row_Changed_By { get; set; }

        public DateTime Row_Changed_Date { get; set; }

        public string Active_Ind { get; set; }
    }
}
