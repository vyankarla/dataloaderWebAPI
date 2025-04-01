using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.InputModels
{
    public class UpdDataSourceByWellIDInput
    {
        public int Well_ID { get; set; }

        public string LoggedInUserName { get; set; }

        public int LoggedInUserID { get; set; }
    }
}
