using ComboCurve.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class MonthlyExportResultsProperties : MonthlyExportOutput
    {
        public string ProjectID { get; set; }

        public string ScenarioID { get; set; }

        public string EconRunID { get; set; }

        public string MonthlyExportJobID { get; set; }

        public DateTime MonthlyExportJobDate { get; set; }

        public string ComboName { get; set; }

        public string WellName { get; set; }

        public DateTime? Date { get; set; }
    }
}
