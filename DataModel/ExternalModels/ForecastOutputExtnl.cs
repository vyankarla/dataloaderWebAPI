using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ExternalModels
{
    public class ForecastOutputExtnl
    {
        public string projectName { get; set; }

        public string forecastName { get; set; }

        public string Best { get; set; }

        public DateTime? CreatedAt { get; set; }

        public Boolean? Forecasted { get; set; }

        public DateTime? ForecastedAt { get; set; }

        public string ForecastedBy { get; set; }

        public string Id { get; set; }

        public string P10 { get; set; }

        public string P50 { get; set; }

        public string P90 { get; set; }

        public string Phase { get; set; }

        public string Ratio { get; set; }

        public DateTime? ReviewedAt { get; set; }

        public string ReviewedBy { get; set; }

        public DateTime? RunDate { get; set; }

        public string Status { get; set; }

        public string TypeCurve { get; set; }

        public string TypeCurveApplySettings { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string Well { get; set; }

        //public DateTime rowinsertdate { get; set; }
    }
}
