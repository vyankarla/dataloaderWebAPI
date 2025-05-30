using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.InputModels
{
    public class apiDailyForecastVolumesInput
    {
        public string projectId { get; set; }

        public string forecastId { get; set; }

        public string forecastType { get; set; }

        public string resolution { get; set; }

        public string wellId { get; set; }

        public string projectName { get; set; }

        public string forecastName { get; set; }

        public string phase { get; set; }

        public string forecastOutputId { get; set; }

        public string series { get; set; }

        public DateTime? startDate { get; set; }

        public DateTime? endDate { get; set; }

        public float? eur { get; set; }

        public int? dayOffset { get; set; }

        public float? volume { get; set; }

        public DateTime? volumeDate { get; set; }

        public DateTime? rowinsertdate { get; set; }

    }
}
