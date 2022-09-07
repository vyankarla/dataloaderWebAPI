using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.InputModels
{
    public class ProjectColumnMappingInput
    {
        public int MappingID { get; set; }

        public string SourceColumn { get; set; }

        public int? TargetColumnID { get; set; }

    }
}
