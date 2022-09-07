using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ExternalModels
{
    public class DatasetTypesExtnl
    {
        public int DatasetTypeID { get; set; }

        public string DatasetTypeName { get; set; }

        public string DatasetTypeDesc { get; set; }

        public string TargetTableName { get; set; }
    }
}
