using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ExternalModels
{
    public class DatasetTypeColumnsExtnl
    {
        public int ColumnID { get; set; }

        public string ColumnName { get; set; }

        public string ColumnDataType { get; set; }

        public string ColumnDescription { get; set; }

        public Boolean AllowNull { get; set; }

        public Boolean isRequired { get; set; }

        public int DatasetTypeID { get; set; }
    }
}
