using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.InputModels
{
    public class ImportDataFinish
    {
        public ProjectInput projectInput { get; set; }

        public List<ProjectColumnMappingInput> projectColumnMappingInputs { get; set; }

        //public List<Dailyprod_StagingInput> dailyprod_StagingInputs { get; set; }
    }
}
