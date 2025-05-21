using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ExternalModels
{
    public class SwimLaneInterests
    {
        public int? LoggedInUserID { get; set; }

        public string LoggedInUserName { get; set; }

        public int? Confidence_Level { get; set; }

        public string Comments { get; set; }

        public List<BPOAPOColumnDetails> BPOAPOColumns { get; set; }

        public DataTable BeforePayoutTableData { get; set; }

        public DataTable AfterPayoutTableData { get; set; }
    }

    public class BPOAPOColumnDetails
    {
        public int DSU_Swim_Lane_Id { get; set; }

        public string ColumnName { get; set; }

        public double LateralLength { get; set; }
    }
}
