using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ExternalModels
{
    public class ForecastDailyVolumesExtnl
    {
        public string project_id { get; set; }

        public string forecast_id { get; set; }

        public int record_count { get; set; }

        public string file_url { get; set; }
    }
}
