using DataAccess;
using DataModel.BusinessObjects;
using DataModel.ExternalModels;
using DataModel.InputModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Management
{
    public class ARIESMasterTablesService
    {
        public static List<ARIESMasterTablesExtnl> SelARIESMasterTables(string connectionString)
        {
            try
            {
                DataTable dt = ARIESMasterTablesAccess.SelARIESMasterTables(connectionString);

                List<ARIESMasterTablesExtnl> aRIESMasterTables = new List<ARIESMasterTablesExtnl>();

                BusinessObjectParser.MapRowsToObject(dt, aRIESMasterTables, "DataModel.ExternalModels.ARIESMasterTablesExtnl",
                     new string[] { "PROPNUM", "LEASE_CURRENT", "LEASE_NEW", "TC_AREA_CURRENT", "TC_AREA_NEW", "RESERVOIR", "TYPECURVE_CURRENT",
                         "TYPECURVE_NEW", "TYPECURVE_RISK_CURRENT", "TYPECURVE_RISK_NEW", "PLANNED_DLL_CURRENT", "PLANNED_DLL_NEW",
                         "PLANNED_CLL_CURRENT", "PLANNED_CLL_NEW", "ONLINE_GROUPING_CURRENT", "ONLINE_GROUPING_NEW", "Well_ID", "SCHEDULED_CURRENT",
                         "SCHEDULED_NEW", "TC_CODE_CURRENT", "TC_CODE_NEW", "Producing_Zone" });

                return aRIESMasterTables;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return null;
        }

        public static int InsARIESMasterTablesEditBatchAndDetails(string connectionString, List<ARIES_Master_Tables_Edit_DetailsInput> updARIESMasterTablesInputs, string Batch_Type, Boolean ExportFlag)
        {
            int rows = 0;
            try
            {
                if (updARIESMasterTablesInputs.Count > 0)
                {
                    DateTime CurrentUtcDateTime = DateTime.UtcNow;

                    ARIES_Master_Tables_Edit_BatchInput aries_Master_Tables_Edit_BatchInput = new ARIES_Master_Tables_Edit_BatchInput();
                    aries_Master_Tables_Edit_BatchInput.Batch_id = 0;
                    aries_Master_Tables_Edit_BatchInput.Batch_Timestamp = CurrentUtcDateTime;
                    aries_Master_Tables_Edit_BatchInput.Row_Created_By = updARIESMasterTablesInputs[0].Row_Created_By;
                    aries_Master_Tables_Edit_BatchInput.Row_Created_Date = CurrentUtcDateTime;

                    rows = ARIESMasterTablesAccess.InsARIESMasterTablesEditBatchAndDetails(connectionString, aries_Master_Tables_Edit_BatchInput, CurrentUtcDateTime, updARIESMasterTablesInputs, Batch_Type, ExportFlag);
                }
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
                throw ex;
            }
            return rows;
        }

        public static int InsARIESMasterTablesDropBatchAndDetails(string connectionString, List<ARIES_Master_Tables_Drop_DetailsInput> updARIESMasterTablesInputs, string Batch_Type, Boolean ExportFlag)
        {
            int rows = 0;
            try
            {
                if (updARIESMasterTablesInputs.Count > 0)
                {
                    DateTime CurrentUtcDateTime = DateTime.UtcNow;

                    ARIES_Master_Tables_Edit_BatchInput aries_Master_Tables_Edit_BatchInput = new ARIES_Master_Tables_Edit_BatchInput();
                    aries_Master_Tables_Edit_BatchInput.Batch_id = 0;
                    aries_Master_Tables_Edit_BatchInput.Batch_Timestamp = CurrentUtcDateTime;
                    aries_Master_Tables_Edit_BatchInput.Row_Created_By = updARIESMasterTablesInputs[0].Row_Created_By;
                    aries_Master_Tables_Edit_BatchInput.Row_Created_Date = CurrentUtcDateTime;

                    rows = ARIESMasterTablesAccess.InsARIESMasterTablesDropBatchAndDetails(connectionString, aries_Master_Tables_Edit_BatchInput, CurrentUtcDateTime, updARIESMasterTablesInputs, Batch_Type, ExportFlag);
                }
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
                throw ex;
            }
            return rows;
        }


        public static List<SelARIES_Master_Tables_Edit_BatchsExtnl> SelARIES_Master_Tables_Batchs(string connectionString, string Batch_Type)
        {
            try
            {
                DataTable dt = ARIESMasterTablesAccess.SelARIES_Master_Tables_Batchs(connectionString, Batch_Type);


                List<SelARIES_Master_Tables_Edit_BatchsExtnl> returnData = new List<SelARIES_Master_Tables_Edit_BatchsExtnl>();

                BusinessObjectParser.MapRowsToObject(dt, returnData, "DataModel.ExternalModels.SelARIES_Master_Tables_Edit_BatchsExtnl",
                    new string[] { "Batch_id", "Batch_Timestamp", "Row_Created_By", "Row_Created_Date", "Batch_Type", "ExportFlag" });
                return returnData;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return null;
        }

        public static List<SelARIES_Master_Tables_Edit_DetailsByBatchId> SelARIES_Master_Tables_Edit_DetailsByBatchId(string connectionString, int Batch_id)
        {
            try
            {
                DataTable dt = ARIESMasterTablesAccess.SelARIES_Master_Tables_Edit_DetailsByBatchId(connectionString, Batch_id);


                List<SelARIES_Master_Tables_Edit_DetailsByBatchId> returnData = new List<SelARIES_Master_Tables_Edit_DetailsByBatchId>();

                BusinessObjectParser.MapRowsToObject(dt, returnData, "DataModel.ExternalModels.SelARIES_Master_Tables_Edit_DetailsByBatchId",
                    new string[] { "Well_ID", "PROPNUM", "LEASE_CURRENT", "LEASE_NEW", "SCHEDULED_CURRENT", "SCHEDULED_NEW",
                        "ONLINE_GROUPING_CURRENT", "ONLINE_GROUPING_NEW", "TC_AREA_CURRENT", "TC_AREA_NEW", "TYPECURVE_CURRENT",
                        "TYPECURVE_NEW", "TC_CODE_CURRENT", "TC_CODE_NEW", "TYPECURVE_RISK_CURRENT", "TYPECURVE_RISK_NEW", "PLANNED_DLL_CURRENT",
                        "PLANNED_DLL_NEW", "PLANNED_CLL_CURRENT", "PLANNED_CLL_NEW", "Batch_id", "Row_Created_By", "Row_Created_Date",
                        "Row_Changed_By", "Row_Changed_Date", "DropComments", "Batch_Type" });
                return returnData;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return null;
        }

        public static int UpdExportFlagByBatch_id(string connectionString, UpdExportFlag updExportFlag, Boolean ExportFlag)
        {
            int rows = 0;
            try
            {
                rows = ARIESMasterTablesAccess.UpdExportFlagByBatch_id(connectionString, updExportFlag, ExportFlag);
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
                throw ex;
            }
            return rows;
        }

        public static List<HeaderInfoForEditStickSheetExtnl> SelARIESDataForEditBatchNew(string connectionString)
        {
            try
            {
                DataTable dt = ARIESMasterTablesAccess.SelARIESDataForEditBatchNew(connectionString);


                List<HeaderInfoForEditStickSheetExtnl> returnData = new List<HeaderInfoForEditStickSheetExtnl>();

                BusinessObjectParser.MapRowsToObject(dt, returnData, "DataModel.ExternalModels.HeaderInfoForEditStickSheetExtnl",
                    new string[] { "Well_ID", "PROPNUM", "Well_Report_Name", "Drilling_Spacing_Unit", "Development_Group", "Type_Curve_Risk",
                    "Planned_Drilled_Lateral_Length", "Planned_Completed_Lateral_Length", "Producing_Zone", "Well_Type", "Study_Area" });
                return returnData;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return null;
        }

        public static List<HeaderInfoForEditStickSheetForApprovalExtnl> SelARIESDataForEditBatchNewForApproval(string connectionString)
        {
            try
            {
                DataTable dt = ARIESMasterTablesAccess.SelARIESDataForEditBatchNewForApproval(connectionString);


                List<HeaderInfoForEditStickSheetForApprovalExtnl> returnData = new List<HeaderInfoForEditStickSheetForApprovalExtnl>();

                List<HeaderInfoForEditStickSheetForApprovalExtnl> finalReturnData = new List<HeaderInfoForEditStickSheetForApprovalExtnl>();

                BusinessObjectParser.MapRowsToObject(dt, returnData, "DataModel.ExternalModels.HeaderInfoForEditStickSheetForApprovalExtnl",
                    new string[] { "Well_ID", "PROPNUM", "Well_Report_Name", "Drilling_Spacing_Unit", "Development_Group", "Type_Curve_Risk",
                    "Planned_Drilled_Lateral_Length", "Planned_Completed_Lateral_Length", "Producing_Zone", "Well_Type", "Data_Source" });

                foreach (var item in returnData.Where(x => x.Data_Source == "Inventory Pre-Approval").ToList())
                {
                    HeaderInfoForEditStickSheetForApprovalExtnl headerRow = new HeaderInfoForEditStickSheetForApprovalExtnl();
                    headerRow = item;

                    foreach (var child in returnData.Where(x => x.Data_Source != "Inventory Pre-Approval" && x.Well_ID == item.Well_ID).ToList())
                    {
                        item.Clilds.Add(child);
                    }
                    finalReturnData.Add(item);
                }

                return finalReturnData;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return null;
        }

        public static int UpdHeaderForEditBatchNew(string connectionString, List<HeaderInfoForEditStickSheetInput> updARIESMasterTablesInputs, Boolean InsertFlag)
        {
            int rows = 0;
            try
            {
                rows = ARIESMasterTablesAccess.UpdHeaderForEditBatchNew(connectionString, updARIESMasterTablesInputs, DateTime.UtcNow, InsertFlag);
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
                throw ex;
            }
            return rows;
        }

        public static int UpdDataSourceToIMByWellID(string connectionString, List<UpdDataSourceByWellIDInput> updDataSourceByWellIDInputs)
        {
            int rows = 0;
            try
            {
                rows = ARIESMasterTablesAccess.UpdDataSourceToIMByWellID(connectionString, updDataSourceByWellIDInputs, DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
                throw ex;
            }
            return rows;
        }

        public static int RejectDataSourceByWellID(string connectionString, List<RejectDataSourceByWellIDInput> rejectDataSourceByWellIDInputs)
        {
            int rows = 0;
            try
            {
                rows = ARIESMasterTablesAccess.RejectDataSourceByWellID(connectionString, rejectDataSourceByWellIDInputs, DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
                throw ex;
            }
            return rows;
        }

        public static List<SelWellDataForCCByWellIDExtnl> SelWellDataForCCByWellID(string connectionString, int Well_ID)
        {
            try
            {
                DataTable dt = ARIESMasterTablesAccess.SelWellDataForCCByWellID(connectionString, Well_ID);


                List<SelWellDataForCCByWellIDExtnl> returnData = new List<SelWellDataForCCByWellIDExtnl>();

                BusinessObjectParser.MapRowsToObject(dt, returnData, "DataModel.ExternalModels.SelWellDataForCCByWellIDExtnl",
                    new string[] { "Well_Official_Name", "DSU_Prod_Zone_Assignment", "Dev_Grouping", "Type_Curve_Risk",
                    "Planned_Drilled_Lateral_Length", "Planned_Completed_Lateral_Length", "PerfLateralLength", "CustomNumber3", "CustomNumber4" });
                return returnData;
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.BLL, ex);
            }
            return null;
        }




    }
}

