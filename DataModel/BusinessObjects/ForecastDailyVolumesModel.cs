using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.BusinessObjects
{
    public class ForecastDailyVolumesModel
    {
        public string projectName { get; set; }

        public string forecastName { get; set; }

        public string wellId { get; set; }

        public string fileFormat { get; set; }
    }
}
