using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.InputModels
{
    public class ARIES_Master_Tables_Edit_DetailsInput
    {
        public int Well_ID { get; set; }

        public string PROPNUM { get; set; }

        public string LEASE_CURRENT { get; set; }

        public string LEASE_NEW { get; set; }

        public string SCHEDULED_CURRENT { get; set; }

        public string SCHEDULED_NEW { get; set; }

        public string ONLINE_GROUPING_CURRENT { get; set; }

        public string ONLINE_GROUPING_NEW { get; set; }

        public string TC_AREA_CURRENT { get; set; }

        public string TC_AREA_NEW { get; set; }

        public string TYPECURVE_CURRENT { get; set; }

        public string TYPECURVE_NEW { get; set; }

        public string TC_CODE_CURRENT { get; set; }

        public string TC_CODE_NEW { get; set; }

        public decimal TYPECURVE_RISK_CURRENT { get; set; }

        public decimal TYPECURVE_RISK_NEW { get; set; }

        public string PLANNED_DLL_CURRENT { get; set; }

        public string PLANNED_DLL_NEW { get; set; }

        public string PLANNED_CLL_CURRENT { get; set; }

        public string PLANNED_CLL_NEW { get; set; }

        public string Row_Created_By { get; set; }
    }
}
