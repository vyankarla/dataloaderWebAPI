using DataAccess;
using DataModel.BusinessObjects;
using DataModel.ExternalModels;
using DataModel.InputModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using static Utilities.Enums;

namespace Management
{
    public class ProjectService
    {
        public static List<ProjectExtnl> SelProjects(string connectionString)
        {
            try
            {
                DataTable dt = ProjectDataAccess.SelProjects(connectionString);

                List<ProjectExtnl> projectExtnl = new List<ProjectExtnl>();

                BusinessObjectParser.MapRowsToObject(dt, projectExtnl, "DataModel.ExternalModels.ProjectExtnl",
                     new string[] { "ProjectID", "Name", "Description", "ProjectGUID", "DatasetTypeID", "FileTypeID", "StatusID",
                     "FileLocation", "Active", "TotalRecords", "FileTypeDesc", "FileTypeName", "DatasetTypeName", "DatasetTypeDesc", "DateCreated",
                     "initiatedBy", "StatusDesc", "StatusName" });

                return projectExtnl;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return null;
        }

        public static int ImportDataFinishStepForExcelType(string connectionString, ImportDataFinish importDataFinish, string loggedInUserName, DataTable excelData)
        {
            try
            {
                List<DatasetTypeColumnsExtnl> datasetTypeColumnsExtnls = DatasetTypeColumnsService.SelDatasetTypeColumns(connectionString);

                Project project = ProjectObjectMapping(importDataFinish.projectInput, loggedInUserName);
                List<ProjectColumnMapping> projectColumnMappings = ProjectColumnObjectMapping(importDataFinish.projectColumnMappingInputs, loggedInUserName);
                List<Dailyprod_Staging> dailyprod_Stagings = ProjectColumnObjectDataMapping(excelData, loggedInUserName, importDataFinish.projectColumnMappingInputs, datasetTypeColumnsExtnls);

                int projectID = ProjectDataAccess.InsUpdImportDataFinish(connectionString, project, projectColumnMappings, dailyprod_Stagings);

                return projectID;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return 0;
        }

        private static Project ProjectObjectMapping(ProjectInput projectInput, string loggedInUserName)
        {
            Project project = new Project();
            project.ProjectID = projectInput.ProjectID;
            project.Name = projectInput.Name;
            project.Description = projectInput.Description;
            project.ProjectGUID = Guid.NewGuid();
            project.DatasetTypeID = projectInput.DatasetTypeID;
            project.FileTypeID = projectInput.FileTypeID;
            project.StatusID = Convert.ToInt32(StatusEnum.Completed);
            project.FileLocation = projectInput.FileLocation;
            project.Active = projectInput.Active;
            project.UserID = projectInput.UserID;

            project.CreatedBy = loggedInUserName;
            project.CreatedOnDt = DateTime.Now;

            project.TotalRecords = projectInput.TotalRecords;

            return project;
        }


        private static List<ProjectColumnMapping> ProjectColumnObjectMapping(List<ProjectColumnMappingInput> projectColumnMappingInputs, string loggedInUserName)
        {
            List<ProjectColumnMapping> projectColumnMappings = new List<ProjectColumnMapping>();
            foreach (var projectColumnMappingInput in projectColumnMappingInputs)
            {
                ProjectColumnMapping projectColumnMapping = new ProjectColumnMapping();

                projectColumnMapping.MappingID = projectColumnMappingInput.MappingID;
                projectColumnMapping.SourceColumn = projectColumnMappingInput.SourceColumn;
                projectColumnMapping.TargetColumnID = projectColumnMappingInput.TargetColumnID;
                projectColumnMapping.CreatedBy = loggedInUserName;
                projectColumnMapping.CreatedOnDt = DateTime.Now;

                projectColumnMappings.Add(projectColumnMapping);
            }

            return projectColumnMappings;
        }


        //private static List<Dailyprod_Staging> ProjectColumnObjectMapping(List<Dailyprod_StagingInput> dailyprod_StagingInputs, string loggedInUserName)
        //{
        //    List<Dailyprod_Staging> dailyprod_Stagings = new List<Dailyprod_Staging>();
        //    foreach (var item in dailyprod_StagingInputs)
        //    {
        //        Dailyprod_Staging dailyprod_Staging = new Dailyprod_Staging();

        //        dailyprod_Staging.AutoID = item.AutoID;
        //        dailyprod_Staging.API = item.API;
        //        dailyprod_Staging.WELLNAME = item.WELLNAME;
        //        dailyprod_Staging.D_DATE = item.D_DATE;
        //        dailyprod_Staging.OIL = item.OIL;
        //        dailyprod_Staging.GAS = item.GAS;
        //        dailyprod_Staging.WATER = item.WATER;
        //        dailyprod_Staging.TubingPsi = item.TubingPsi;
        //        dailyprod_Staging.CasingPsi = item.CasingPsi;
        //        dailyprod_Staging.Choke = item.Choke;
        //        dailyprod_Staging.Downtime = item.Downtime;
        //        dailyprod_Staging.DowntimeReason = item.DowntimeReason;
        //        dailyprod_Staging.Row_Created_By = loggedInUserName;
        //        dailyprod_Staging.Row_Created_Date = DateTime.Now;

        //        dailyprod_Stagings.Add(dailyprod_Staging);
        //    }

        //    return dailyprod_Stagings;
        //}

        private static List<Dailyprod_Staging> ProjectColumnObjectDataMapping(DataTable excelData, string loggedInUserName, List<ProjectColumnMappingInput> projectColumnMappingInputs, List<DatasetTypeColumnsExtnl> datasetTypeColumnsExtnls)
        {   
            List<Dailyprod_Staging> dailyprod_Stagings = new List<Dailyprod_Staging>();

            Boolean isAPIIsRequired = false;
            Boolean isWellNameRequired = false;
            Boolean isDDateRequired = false;
            Boolean isOilRequired = false;
            Boolean isGasRequired = false;
            Boolean isWaterRequired = false;
            Boolean isTubingPsiRequired = false;
            Boolean isCasingPsiRequired = false;
            Boolean isDowntimeRequired = false;
            Boolean isDowntimeReasonRequired = false;
            Boolean isChokeIsRequired = false;

            foreach (var item in datasetTypeColumnsExtnls)
            {
                if (item.ColumnID == Convert.ToInt32(DatasetTypeColumnsEnum.API))
                {
                    isAPIIsRequired = item.isRequired;
                }
                else if (item.ColumnID == Convert.ToInt32(DatasetTypeColumnsEnum.WELLNAME))
                {
                    isWellNameRequired = item.isRequired;
                }
                else if (item.ColumnID == Convert.ToInt32(DatasetTypeColumnsEnum.D_DATE))
                {
                    isDDateRequired = item.isRequired;
                }
                else if (item.ColumnID == Convert.ToInt32(DatasetTypeColumnsEnum.OIL))
                {
                    isOilRequired = item.isRequired;
                }
                else if (item.ColumnID == Convert.ToInt32(DatasetTypeColumnsEnum.GAS))
                {
                    isGasRequired = item.isRequired;
                }
                else if (item.ColumnID == Convert.ToInt32(DatasetTypeColumnsEnum.WATER))
                {
                    isWaterRequired = item.isRequired;
                }
                else if (item.ColumnID == Convert.ToInt32(DatasetTypeColumnsEnum.TubingPsi))
                {
                    isTubingPsiRequired = item.isRequired;
                }
                else if (item.ColumnID == Convert.ToInt32(DatasetTypeColumnsEnum.CasingPsi))
                {
                    isCasingPsiRequired = item.isRequired;
                }
                else if (item.ColumnID == Convert.ToInt32(DatasetTypeColumnsEnum.Downtime))
                {
                    isDowntimeRequired = item.isRequired;
                }
                else if (item.ColumnID == Convert.ToInt32(DatasetTypeColumnsEnum.DowntimeReason))
                {
                    isDowntimeReasonRequired = item.isRequired;
                }
                else if (item.ColumnID == Convert.ToInt32(DatasetTypeColumnsEnum.Choke))
                {
                    isChokeIsRequired = item.isRequired;
                }
            }

            //GetSource Columns FROM Excel sheet
            string APIExcelColumnName = GetExcelSourceColumnName(projectColumnMappingInputs, Convert.ToInt32(DatasetTypeColumnsEnum.API));
            string WellNameExcelColumnName = GetExcelSourceColumnName(projectColumnMappingInputs, Convert.ToInt32(DatasetTypeColumnsEnum.WELLNAME));
            string DDateExcelColumnName = GetExcelSourceColumnName(projectColumnMappingInputs, Convert.ToInt32(DatasetTypeColumnsEnum.D_DATE));
            string OilExcelColumnName = GetExcelSourceColumnName(projectColumnMappingInputs, Convert.ToInt32(DatasetTypeColumnsEnum.OIL));
            string GasExcelColumnName = GetExcelSourceColumnName(projectColumnMappingInputs, Convert.ToInt32(DatasetTypeColumnsEnum.GAS));
            string waterExcelColumnName = GetExcelSourceColumnName(projectColumnMappingInputs, Convert.ToInt32(DatasetTypeColumnsEnum.WATER));
            string tubingPsiExcelColumnName = GetExcelSourceColumnName(projectColumnMappingInputs, Convert.ToInt32(DatasetTypeColumnsEnum.TubingPsi));
            string casingPsiExcelColumnName = GetExcelSourceColumnName(projectColumnMappingInputs, Convert.ToInt32(DatasetTypeColumnsEnum.CasingPsi));
            string downtimeExcelColumnName = GetExcelSourceColumnName(projectColumnMappingInputs, Convert.ToInt32(DatasetTypeColumnsEnum.Downtime));
            string downtimeReasonExcelColumnName = GetExcelSourceColumnName(projectColumnMappingInputs, Convert.ToInt32(DatasetTypeColumnsEnum.DowntimeReason));
            string chokeExcelColumnName = GetExcelSourceColumnName(projectColumnMappingInputs, Convert.ToInt32(DatasetTypeColumnsEnum.Choke));

            foreach (DataRow item in excelData.Rows)
            {
                Dailyprod_Staging dailyprod_Staging = new Dailyprod_Staging();

                dailyprod_Staging.AutoID = 0;

                dailyprod_Staging.API = CheckisColumnHasDataOrNot(isAPIIsRequired, item, APIExcelColumnName);
                dailyprod_Staging.WELLNAME = CheckisColumnHasDataOrNot(isWellNameRequired, item, WellNameExcelColumnName);
                dailyprod_Staging.D_DATE = CheckisColumnHasDataOrNot(isDDateRequired, item, DDateExcelColumnName);
                dailyprod_Staging.OIL = CheckisColumnHasDataOrNot(isOilRequired, item, OilExcelColumnName);
                dailyprod_Staging.GAS = CheckisColumnHasDataOrNot(isGasRequired, item, GasExcelColumnName);
                dailyprod_Staging.WATER = CheckisColumnHasDataOrNot(isWaterRequired, item, waterExcelColumnName);
                dailyprod_Staging.TubingPsi = CheckisColumnHasDataOrNot(isTubingPsiRequired, item, tubingPsiExcelColumnName);
                dailyprod_Staging.CasingPsi = CheckisColumnHasDataOrNot(isCasingPsiRequired, item, casingPsiExcelColumnName);
                dailyprod_Staging.Downtime = CheckisColumnHasDataOrNot(isDowntimeRequired, item, downtimeExcelColumnName);
                dailyprod_Staging.DowntimeReason = CheckisColumnHasDataOrNot(isDowntimeReasonRequired, item, downtimeReasonExcelColumnName);
                dailyprod_Staging.Choke = CheckisColumnHasDataOrNot(isChokeIsRequired, item, chokeExcelColumnName);

                dailyprod_Staging.Row_Created_By = loggedInUserName;
                dailyprod_Staging.Row_Created_Date = DateTime.Now;

                dailyprod_Stagings.Add(dailyprod_Staging);
            }

            return dailyprod_Stagings;
        }

        private static string GetExcelSourceColumnName(List<ProjectColumnMappingInput> projectColumnMappingInputs, int targetColumnID)
        {
            return projectColumnMappingInputs.
                                        Where(x => x.TargetColumnID == targetColumnID).ToList().Count > 0 ?
                                        projectColumnMappingInputs.Where(x => x.TargetColumnID == targetColumnID).ToList()[0].SourceColumn
                                        : "";
        }

        private static string CheckisColumnHasDataOrNot(Boolean isRequiredFlag, DataRow dataRow, string excelColumnName)
        {

            if (excelColumnName.Trim() == "")
            {
                return "";
            }
            else
            {
                if (isRequiredFlag)
                {
                    if (Convert.ToString(dataRow[excelColumnName]) != "")
                    {
                        return Convert.ToString(dataRow[excelColumnName]);
                    }
                    else
                    {
                        throw new Exception(excelColumnName + " is required in database, but there are some empty fields exists in excel. Please verify");
                    }
                }
                else
                {
                    return Convert.ToString(dataRow[excelColumnName]);
                }
            }
        }



    }
}
