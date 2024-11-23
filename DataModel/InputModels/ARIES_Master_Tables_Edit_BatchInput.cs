using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.InputModels
{
    public class ARIES_Master_Tables_Edit_BatchInput
    {
        public int Batch_id { get; set; }

        public DateTime Batch_Timestamp { get; set; }

        public string Row_Created_By { get; set; }

        public DateTime Row_Created_Date { get; set; }
    }
}
