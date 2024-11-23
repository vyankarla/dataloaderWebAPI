using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ExternalModels
{
    public class DevGroupingsExtnl
    {
        public int Dev_Groupings_Id { get; set; }

        public string Dev_Grouping { get; set; }

        public string Dev_Comments_1 { get; set; }

        public string Dev_Comments_2 { get; set; }

        public string Dev_Comments_3 { get; set; }

        public string Dev_Comments_4 { get; set; }

        public string Asset_Area { get; set; }

        public string Row_Created_By { get; set; }

        public DateTime? Row_Created_Date { get; set; }

        public string Row_Changed_By { get; set; }

        public DateTime? Row_Changed_Date { get; set; }

        public string Row_Archived_By { get; set; }

        public DateTime? Row_Archived_Date { get; set; }

        public string Manual_Override { get; set; }

        public string Active_Ind { get; set; }
    }

}
