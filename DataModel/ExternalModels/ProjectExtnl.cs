using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ExternalModels
{
    public class ProjectExtnl
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

        public int? TotalRecords { get; set; }

        public string FileTypeDesc { get; set; }

        public string FileTypeName { get; set; }

        public string DatasetTypeName { get; set; }

        public string DatasetTypeDesc { get; set; }

        public DateTime DateCreated { get; set; }

        public string initiatedBy { get; set; }

        public string StatusDesc { get; set; }

        public string StatusName { get; set; }
    }
}
