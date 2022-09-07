using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ExternalModels
{
    public class ControllerReturnObject
    {
        public int Status { get; set; }

        public object Data { get; set; }

        public string Message { get; set; }
    }
}
