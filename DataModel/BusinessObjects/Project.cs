using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.BusinessObjects
{
    public class Project
    {
        public int ProjectID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Guid ProjectGUID { get; set; }

        public int DatasetTypeID { get; set; }

        public int FileTypeID { get; set; }

        public int StatusID { get; set; }

        public string FileLocation { get; set; }

        public bool? Active { get; set; }

        public int UserID { get; set; }

        public int DatasourceID { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOnDt { get; set; }

        public int? TotalRecords { get; set; }

    }
}
