using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.BusinessObjects
{
    public class ProjectColumnMapping
    {
        public int MappingID { get; set; }

        public int? ProjectID { get; set; }

        public string SourceColumn { get; set; }

        public int? TargetColumnID { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedOnDt { get; set; }
    }
}
