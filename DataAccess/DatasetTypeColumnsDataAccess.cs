using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Utilities;

namespace DataAccess
{
    public class DatasetTypeColumnsDataAccess
    {

        public static DataTable SelDatasetTypeColumns(string ConnectionString)
        {
            try
            {
                DataSet ds = null;

                ds = SQLHelper.SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "[dataloader].[SelDatasetTypeColumns]");
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
            }
            return null;
        }


    }
}
