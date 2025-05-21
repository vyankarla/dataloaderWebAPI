using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.InputModels
{
    public class Swim_Lane_OwnershipInput
    {
        public int DSU_Swim_Lane_Id { get; set; }

        public string Producing_Zone { get; set; }

        public float? BPO_WI { get; set; }

        public float? BPO_NRI { get; set; }

        public float? APO_WI { get; set; }

        public float? APO_NRI { get; set; }
    }
}
