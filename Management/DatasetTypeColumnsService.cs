using DataAccess;
using DataModel.BusinessObjects;
using DataModel.ExternalModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Management
{
    public class DatasetTypeColumnsService
    {

        public static List<DatasetTypeColumnsExtnl> SelDatasetTypeColumns(string connectionString)
        {
            try
            {
                DataTable dt = DatasetTypeColumnsDataAccess.SelDatasetTypeColumns(connectionString);

                List<DatasetTypeColumnsExtnl> fileTypesExtnl = new List<DatasetTypeColumnsExtnl>();

                BusinessObjectParser.MapRowsToObject(dt, fileTypesExtnl, "DataModel.ExternalModels.DatasetTypeColumnsExtnl",
                     new string[] { "ColumnID", "ColumnName", "ColumnDataType", "ColumnDescription", "AllowNull", "isRequired", "DatasetTypeID" });

                return fileTypesExtnl;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return null;
        }


    }
}
