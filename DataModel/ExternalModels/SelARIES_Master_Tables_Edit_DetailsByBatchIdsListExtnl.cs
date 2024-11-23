using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ExternalModels
{
    public class SelARIES_Master_Tables_Edit_DetailsByBatchIdsListExtnl
    {
        public int Batch_id { get; set; }
        public List<SelARIES_Master_Tables_Edit_DetailsByBatchId> BatchDetails { get; set; }
    }
}
