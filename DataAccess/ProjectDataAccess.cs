using DataModel.BusinessObjects;
using DataModel.InputModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace DataAccess
{
    public class ProjectDataAccess
    {

        public static DataTable SelProjects(string ConnectionString)
        {
            try
            {
                DataSet ds = null;

                ds = SQLHelper.SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure, "[dataloader].[SelProjects]");
                if (ds != null && ds.Tables.Count > 0)
                    return ds.Tables[0];
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
            }
            return null;
        }

        public static int InsUpdImportDataFinish(string connectionString, Project project, List<ProjectColumnMapping> projectColumnMappings, List<Dailyprod_Staging> dailyprod_Stagings)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction();

            int projectID = 0;

            try
            {
                projectID = InsUpdProject(trans, project);
                InsUpdProjectColumnMapping(trans, projectColumnMappings, projectID);
                InsUpdDailyprod_Staging(trans, dailyprod_Stagings, projectID);

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
                throw ex;
            }

            return projectID;
        }

        private static int InsUpdProject(SqlTransaction trans, Project project)
        {

            int ProjectID = 0;

            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@ProjectID", project.ProjectID),
                                                new SqlParameter("@Name", project.Name),
                                                new SqlParameter("@Description", project.Description),
                                                new SqlParameter("@ProjectGUID", project.ProjectGUID),
                                                new SqlParameter("@DatasetTypeID", project.DatasetTypeID),
                                                new SqlParameter("@FileTypeID", project.FileTypeID),
                                                new SqlParameter("@StatusID", project.StatusID),
                                                new SqlParameter("@FileLocation", project.FileLocation),
                                                new SqlParameter("@Active", project.Active),
                                                new SqlParameter("@CreatedBy", project.CreatedBy),
                                                new SqlParameter("@CreatedOnDt", project.CreatedOnDt),
                                                new SqlParameter("@TotalRecords", project.TotalRecords),
                                                new SqlParameter("@UserID", project.UserID)
                                                };

                ProjectID = Convert.ToInt32(SQLHelper.SqlHelper.ExecuteScalar(trans, CommandType.StoredProcedure, "[dataloader].[InsUpdProject]", paramsArray));

                return ProjectID;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
                throw ex;
            }
        }


        private static int InsUpdProjectColumnMapping(SqlTransaction trans, List<ProjectColumnMapping> projectColumnMappings, int ProjectID)
        {

            int MappingID = 0;

            try
            {
                foreach (var item in projectColumnMappings)
                {
                    SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@MappingID", item.MappingID),
                                                new SqlParameter("@ProjectID", ProjectID),
                                                new SqlParameter("@SourceColumn", item.SourceColumn),
                                                new SqlParameter("@TargetColumnID", item.TargetColumnID),
                                                new SqlParameter("@CreatedBy", item.CreatedBy),
                                                new SqlParameter("@CreatedOnDt", item.CreatedOnDt)
                                                };

                    MappingID = Convert.ToInt32(SQLHelper.SqlHelper.ExecuteScalar(trans, CommandType.StoredProcedure, "[dataloader].[InsUpdProjectColumnMapping]", paramsArray));
                }

                return MappingID;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
                throw ex;
            }
        }


        private static int InsUpdDailyprod_Staging(SqlTransaction trans, List<Dailyprod_Staging> dailyprod_Stagings, int ProjectID)
        {

            int AutoID = 0;

            try
            {
                foreach (var item in dailyprod_Stagings)
                {
                    SqlParameter[] paramsArray = new SqlParameter[]{
                                                new SqlParameter("@AutoID", item.AutoID),
                                                new SqlParameter("@ProjectID", ProjectID),
                                                new SqlParameter("@API", item.API),
                                                new SqlParameter("@WELLNAME", item.WELLNAME),
                                                new SqlParameter("@D_DATE", item.D_DATE),
                                                new SqlParameter("@OIL", item.OIL),
                                                new SqlParameter("@GAS", item.GAS),
                                                new SqlParameter("@WATER", item.WATER),
                                                new SqlParameter("@TubingPsi", item.TubingPsi),
                                                new SqlParameter("@CasingPsi", item.CasingPsi),
                                                new SqlParameter("@Choke", item.Choke),
                                                new SqlParameter("@Downtime", item.Downtime),
                                                new SqlParameter("@DowntimeReason", item.DowntimeReason),
                                                new SqlParameter("@Row_Created_Date", item.Row_Created_Date),
                                                new SqlParameter("@Row_Created_By", item.Row_Created_By)
                                                };

                    AutoID = Convert.ToInt32(SQLHelper.SqlHelper.ExecuteScalar(trans, CommandType.StoredProcedure, "[dataloader].[InsUpdDailyprod_Staging]", paramsArray));
                }

                return AutoID;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.DAL, ex);
                throw ex;
            }
        }


    }
}
