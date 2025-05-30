using DataModel.ExternalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.InputModels
{
    public class SwimLaneInterestsInput
    {
        public SwimLaneInterestsInput()
        {
            swimLaneOwnershipPivotViewExtnls = new List<SWIMLaneOwnershipPivotViewInput>();
        }

        public int Confidence_Level { get; set; }

        public string Comments { get; set; }

        public int LoggedInUserID { get; set; }

        public string LoggedInUserName { get; set; }

        public List<SWIMLaneOwnershipPivotViewInput> swimLaneOwnershipPivotViewExtnls { get; set; }
    }
}
