using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ExternalModels
{
    public class DSUSwimLanesExtnl
    {
        public int DSU_Swim_Lane_Id { get; set; }

        public string Data_Source { get; set; }

        public int DSU_Header_Id { get; set; }

        public int Swim_Lane_Number { get; set; }

        public double Lateral_Length { get; set; }

    }
}
