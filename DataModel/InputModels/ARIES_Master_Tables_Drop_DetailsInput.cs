using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.InputModels
{
    public class ARIES_Master_Tables_Drop_DetailsInput
    {
        public int Well_ID { get; set; }

        public string PROPNUM { get; set; }

        public string Comments { get; set; }

        public string Row_Created_By { get; set; }
    }
}
