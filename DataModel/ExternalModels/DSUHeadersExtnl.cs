using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ExternalModels
{
    public class DSUHeadersExtnl
    {
        public int DSU_Header_Id { get; set; }

        public string Data_Source { get; set; }

        public string Drilling_Spacing_Unit { get; set; }

        public string Edited_By_Name { get; set; }

        public int Confidence_Level { get; set; }

        public string Comments { get; set; }

        public string Row_Created_By { get; set; }

        public DateTime Row_Created_Date { get; set; }

        public string Row_Changed_By { get; set; }

        public DateTime? Row_Changed_Date { get; set; }

        public string Row_Archived_By { get; set; }

        public DateTime? Row_Archived_Date { get; set; }

        public string Active_Ind { get; set; }
    }

    public class DSUHeadersExtnlHistory : DSUHeadersExtnl
    {
        public int Version { get; set; }
    }
}
