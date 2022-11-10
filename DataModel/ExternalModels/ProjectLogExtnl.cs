using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ExternalModels
{
    public class ProjectLogExtnl
    {
        public int LogID { get; set; }

        public int ProjectID { get; set; }

        public string ProjectName { get; set; }

        public int RowsInStaging { get; set; }

        public int WellsInStaging { get; set; }

        public int RowsProcessed { get; set; }

        public int WellsProcessed { get; set; }

        public string MissingWellsInTargetDB { get; set; }
    }
}
