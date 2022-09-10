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
    public class LookupService
    {
        public static List<FileTypesExtnl> SelFileTypes(string connectionString)
        {
            try
            {
                DataTable dt = LookupDataAccess.SelFileTypes(connectionString);

                List<FileTypesExtnl> fileTypesExtnl = new List<FileTypesExtnl>();

                BusinessObjectParser.MapRowsToObject(dt, fileTypesExtnl, "DataModel.ExternalModels.FileTypesExtnl",
                     new string[] { "FileTypeID", "FileTypeName", "FileTypeDesc" });

                return fileTypesExtnl;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return null;
        }


        public static List<DatasetTypesExtnl> SelDatasetTypes(string connectionString)
        {
            try
            {
                DataTable dt = LookupDataAccess.SelDatasetTypes(connectionString);

                List<DatasetTypesExtnl> datasetTypesExtnl = new List<DatasetTypesExtnl>();

                BusinessObjectParser.MapRowsToObject(dt, datasetTypesExtnl, "DataModel.ExternalModels.DatasetTypesExtnl",
                     new string[] { "DatasetTypeID", "DatasetTypeName", "DatasetTypeDesc", "TargetTableName" });

                return datasetTypesExtnl;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return null;
        }


        public static List<DatasourceTypesExtnl> SelDatasourceTypes(string connectionString)
        {
            try
            {
                DataTable dt = LookupDataAccess.SelDatasourceTypes(connectionString);

                List<DatasourceTypesExtnl> datasourceTypesExtnl = new List<DatasourceTypesExtnl>();

                BusinessObjectParser.MapRowsToObject(dt, datasourceTypesExtnl, "DataModel.ExternalModels.DatasourceTypesExtnl",
                     new string[] { "DatasourceID", "Datasource", "Active" });

                return datasourceTypesExtnl;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return null;
        }

    }
}
