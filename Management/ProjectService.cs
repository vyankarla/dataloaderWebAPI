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
                     "initiatedBy", "StatusDesc", "StatusName", "DatasourceID", "Datasource" });

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
                int projectID = 0;
                if (project.DatasetTypeID == Convert.ToInt32(DatasetTypesEnum.DailyProd))
                {
                    List<Dailyprod_Staging> dailyprod_Stagings = ProjectColumnObjectDataMapping(excelData, loggedInUserName, importDataFinish.projectColumnMappingInputs, datasetTypeColumnsExtnls);
                    projectID = ProjectDataAccess.InsUpdImportDataFinish(connectionString, project, projectColumnMappings, dailyprod_Stagings);
                }
                else if (project.DatasetTypeID == Convert.ToInt32(DatasetTypesEnum.Monthly))
                {
                    List<MonthlyProd_Staging> monthlyProd_Staging = ProjectColumnObjectDataMappingForMonthlyDaya(excelData, loggedInUserName, importDataFinish.projectColumnMappingInputs, datasetTypeColumnsExtnls);
                    projectID = ProjectDataAccess.InsUpdImportDataFinishForMonthlyData(connectionString, project, projectColumnMappings, monthlyProd_Staging);
                }

                return projectID;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return 0;
        }

        public static int ImportDataFinishStepForAccessDB(string connectionString, ImportDataFinish importDataFinish, string loggedInUserName, List<Dailyprod_Staging> prodStagingData)
        {
            try
            {
                //List<DatasetTypeColumnsExtnl> datasetTypeColumnsExtnls = DatasetTypeColumnsService.SelDatasetTypeColumns(connectionString);

                //Project project = ProjectObjectMapping(importDataFinish.projectInput, loggedInUserName);
                //List<ProjectColumnMapping> projectColumnMappings = new List<ProjectColumnMapping>();

                //int projectID = ProjectDataAccess.InsUpdImportDataFinishTest(connectionString, project, projectColumnMappings, prodStagingData);


                List<DatasetTypeColumnsExtnl> datasetTypeColumnsExtnls = DatasetTypeColumnsService.SelDatasetTypeColumns(connectionString);

                Project project = ProjectObjectMapping(importDataFinish.projectInput, loggedInUserName);
                List<ProjectColumnMapping> projectColumnMappings = new List<ProjectColumnMapping>();
                //int projectID = ProjectDataAccess.InsUpdImportDataFinish(connectionString, project, projectColumnMappings, prodStagingData);

                int projectID = 0;
                if (project.DatasetTypeID == Convert.ToInt32(DatasetTypesEnum.DailyProd))
                {
                    projectID = ProjectDataAccess.InsUpdImportDataFinish(connectionString, project, projectColumnMappings, prodStagingData);
                }
                //else if (project.DatasetTypeID == Convert.ToInt32(DatasetTypesEnum.Monthly))
                //{
                //    projectID = ProjectDataAccess.InsUpdImportDataFinishForMonthlyData(connectionString, project, projectColumnMappings, prodStagingDataForMonthly);
                //}

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
            project.StatusID = Convert.ToInt32(StatusEnum.Initiated);
            project.FileLocation = projectInput.FileLocation;
            project.Active = projectInput.Active;
            project.UserID = projectInput.UserID;
            project.DatasourceID = projectInput.DatasourceID;

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

            //foreach (var item in datasetTypeColumnsExtnls)
            //{
            //    if (item.ColumnID == Convert.ToInt32(DatasetTypeColumnsEnum.API))
            //    {
            //        isAPIIsRequired = item.isRequired;
            //    }
            //    else if (item.ColumnID == Convert.ToInt32(DatasetTypeColumnsEnum.WELLNAME))
            //    {
            //        isWellNameRequired = item.isRequired;
            //    }
            //    else if (item.ColumnID == Convert.ToInt32(DatasetTypeColumnsEnum.D_DATE))
            //    {
            //        isDDateRequired = item.isRequired;
            //    }
            //    else if (item.ColumnID == Convert.ToInt32(DatasetTypeColumnsEnum.OIL))
            //    {
            //        isOilRequired = item.isRequired;
            //    }
            //    else if (item.ColumnID == Convert.ToInt32(DatasetTypeColumnsEnum.GAS))
            //    {
            //        isGasRequired = item.isRequired;
            //    }
            //    else if (item.ColumnID == Convert.ToInt32(DatasetTypeColumnsEnum.WATER))
            //    {
            //        isWaterRequired = item.isRequired;
            //    }
            //    else if (item.ColumnID == Convert.ToInt32(DatasetTypeColumnsEnum.TubingPsi))
            //    {
            //        isTubingPsiRequired = item.isRequired;
            //    }
            //    else if (item.ColumnID == Convert.ToInt32(DatasetTypeColumnsEnum.CasingPsi))
            //    {
            //        isCasingPsiRequired = item.isRequired;
            //    }
            //    else if (item.ColumnID == Convert.ToInt32(DatasetTypeColumnsEnum.Downtime))
            //    {
            //        isDowntimeRequired = item.isRequired;
            //    }
            //    else if (item.ColumnID == Convert.ToInt32(DatasetTypeColumnsEnum.DowntimeReason))
            //    {
            //        isDowntimeReasonRequired = item.isRequired;
            //    }
            //    else if (item.ColumnID == Convert.ToInt32(DatasetTypeColumnsEnum.Choke))
            //    {
            //        isChokeIsRequired = item.isRequired;
            //    }
            //}

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

        private static List<MonthlyProd_Staging> ProjectColumnObjectDataMappingForMonthlyDaya(DataTable excelData, string loggedInUserName, List<ProjectColumnMappingInput> projectColumnMappingInputs, List<DatasetTypeColumnsExtnl> datasetTypeColumnsExtnls)
        {
            List<MonthlyProd_Staging> monthlyProd_Stagings = new List<MonthlyProd_Staging>();

            Boolean isAPIIsRequired = true;
            Boolean isYearRequired = false;
            Boolean isMonthRequired = false;
            Boolean isOilRequired = false;
            Boolean isGasRequired = false;
            Boolean isWaterRequired = false;
            Boolean isDaysOnRequired = false;

            //GetSource Columns FROM Excel sheet
            string APIExcelColumnName = GetExcelSourceColumnName(projectColumnMappingInputs, Convert.ToInt32(DatasetTypeColumnsEnum.API_Monthly));
            string YearExcelColumnName = GetExcelSourceColumnName(projectColumnMappingInputs, Convert.ToInt32(DatasetTypeColumnsEnum.Year));
            string MonthExcelColumnName = GetExcelSourceColumnName(projectColumnMappingInputs, Convert.ToInt32(DatasetTypeColumnsEnum.Month));
            string OilExcelColumnName = GetExcelSourceColumnName(projectColumnMappingInputs, Convert.ToInt32(DatasetTypeColumnsEnum.Oil_Monthly));
            string GasExcelColumnName = GetExcelSourceColumnName(projectColumnMappingInputs, Convert.ToInt32(DatasetTypeColumnsEnum.Gas_Monthly));
            string waterExcelColumnName = GetExcelSourceColumnName(projectColumnMappingInputs, Convert.ToInt32(DatasetTypeColumnsEnum.Water_Monthly));
            string DaysOnExcelColumnName = GetExcelSourceColumnName(projectColumnMappingInputs, Convert.ToInt32(DatasetTypeColumnsEnum.DaysOn));

            foreach (DataRow item in excelData.Rows)
            {
                MonthlyProd_Staging monthlyProd_Staging = new MonthlyProd_Staging();

                monthlyProd_Staging.AutoID = 0;

                monthlyProd_Staging.API = CheckisColumnHasDataOrNot(isAPIIsRequired, item, APIExcelColumnName);
                monthlyProd_Staging.YEAR = CheckisColumnHasDataOrNot(isYearRequired, item, YearExcelColumnName);
                monthlyProd_Staging.MONTH = CheckisColumnHasDataOrNot(isMonthRequired, item, MonthExcelColumnName);
                monthlyProd_Staging.OIL = CheckisColumnHasDataOrNot(isOilRequired, item, OilExcelColumnName);
                monthlyProd_Staging.GAS = CheckisColumnHasDataOrNot(isGasRequired, item, GasExcelColumnName);
                monthlyProd_Staging.WATER = CheckisColumnHasDataOrNot(isWaterRequired, item, waterExcelColumnName);
                monthlyProd_Staging.DAYSON = CheckisColumnHasDataOrNot(isDaysOnRequired, item, DaysOnExcelColumnName);

                monthlyProd_Staging.Row_Created_By = loggedInUserName;
                monthlyProd_Staging.Row_Created_Date = DateTime.Now;

                monthlyProd_Stagings.Add(monthlyProd_Staging);
            }

            return monthlyProd_Stagings;
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
                //if (isRequiredFlag)
                //{
                //    if (Convert.ToString(dataRow[excelColumnName]) != "")
                //    {
                //        return Convert.ToString(dataRow[excelColumnName]);
                //    }
                //    else
                //    {
                //        throw new Exception(excelColumnName + " is required in database, but there are some empty fields exists in excel. Please verify");
                //    }
                //}
                //else
                //{
                //    return Convert.ToString(dataRow[excelColumnName]);
                //}

                return Convert.ToString(dataRow[excelColumnName]);
            }
        }


        //public static int ImportDataFinishStepForAccessDBTest(string connectionString, ImportDataFinish importDataFinish, string loggedInUserName, List<Dailyprod_Staging> lstStagingData)
        //{
        //    try
        //    {
        //        List<DatasetTypeColumnsExtnl> datasetTypeColumnsExtnls = DatasetTypeColumnsService.SelDatasetTypeColumns(connectionString);

        //        Project project = ProjectObjectMapping(importDataFinish.projectInput, loggedInUserName);
        //        List<ProjectColumnMapping> projectColumnMappings = new List<ProjectColumnMapping>();

        //        int projectID = ProjectDataAccess.InsUpdImportDataFinishForAccessDBTest(connectionString, project, projectColumnMappings, lstStagingData);

        //        return projectID;
        //    }
        //    catch (Exception ex)
        //    {
        //        IRExceptionHandler.HandleException(ProjectType.BLL, ex);
        //    }
        //    return 0;
        //}

        public static void ProcessDailyProdStaging(string connectionString, int ProjectID)
        {
            try
            {
                ProjectDataAccess.ProcessDailyProdStaging(connectionString, ProjectID);
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
        }

        public static void ProcessMonthlyProdStaging(string connectionString, int ProjectID)
        {
            try
            {
                ProjectDataAccess.ProcessDailyProdStaging(connectionString, ProjectID);
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
        }

        public static List<ProjectLogExtnl> SelProjectStagingDataLogByProjectID(string connectionString, int ProjectID)
        {
            try
            {
                DataTable dt = ProjectDataAccess.SelProjectStagingDataLogByProjectID(connectionString, ProjectID);

                List<ProjectLogExtnl> projectLogExtnls = new List<ProjectLogExtnl>();

                BusinessObjectParser.MapRowsToObject(dt, projectLogExtnls, "DataModel.ExternalModels.ProjectLogExtnl",
                     new string[] { "LogID", "ProjectID", "RowsInStaging", "WellsInStaging", "RowsProcessed", "WellsProcessed", "MissingWellsInTargetDB", "ProjectName" });

                return projectLogExtnls;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return null;
        }


    }
}
