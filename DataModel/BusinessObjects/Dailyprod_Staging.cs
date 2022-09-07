using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.BusinessObjects
{
    public class Dailyprod_Staging
    {
        public int AutoID { get; set; }

        public int ProjectID { get; set; }

        public string API { get; set; }

        public string WELLNAME { get; set; }

        public string D_DATE { get; set; }

        public string OIL { get; set; }

        public string GAS { get; set; }

        public string WATER { get; set; }

        public string TubingPsi { get; set; }

        public string CasingPsi { get; set; }

        public string Choke { get; set; }

        public string Downtime { get; set; }

        public string DowntimeReason { get; set; }

        public DateTime Row_Created_Date { get; set; }

        public string Row_Created_By { get; set; }
    }
}
