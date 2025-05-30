using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ExternalModels
{
    public class DSUHeaderWells
    {
        public int Well_ID { get; set; }

        public string Well_Official_Name { get; set; }

        public string Operator { get; set; }

        public string API_10 { get; set; }

        public string PROPNUM { get; set; }

        public string Reserves_Category { get; set; }

        public string Producing_Zone { get; set; }

        public string Missing_Zone_In_Swim_Lane { get; set; }

        public string Drilling_Spacing_Unit { get; set; }

        public int Swim_Lane_Number { get; set; }

        public float? Lateral_Length { get; set; }

        public double? BPO_WI { get; set; }

        public double? BPO_NRI { get; set; }

        public double? APO_WI { get; set; }

        public double? APO_NRI { get; set; }

        public int DSU_Header_Id { get; set; }
    }
}
