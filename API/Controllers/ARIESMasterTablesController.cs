using API.App_Start;
using API.Common;
using DataModel.ExternalModels;
using DataModel.InputModels;
using Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Utilities;
using static API.Common.Base;

namespace API.Controllers
{
    [Route("api/ARIESMasterTables/{action}")]
    //[BasicAuthentication]
    public class ARIESMasterTablesController : ApiController
    {
        Base p = new Base();

        [HttpGet]
        [ActionName("SelStickSheetInfo")]
        public IHttpActionResult SelStickSheetInfo()
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                List<ARIESMasterTablesExtnl> aRIESMasterTables = ARIESMasterTablesService.SelARIESMasterTables(p.DBConnectionStringForDataProcessing);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = aRIESMasterTables;
            }
            catch (Exception ex)
            {
                //IRExceptionHandler.HandleException(ProjectType.WebAPI, ex);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Error);
                returnData.Data = "";
                returnData.Message = ex.Message;
            }

            return Ok(returnData);
        }

        [HttpPost]
        [ActionName("UpdStickSheetDetailsAndMoveToQueue")]
        public IHttpActionResult InsARIESMasterTablesEditBatchAndDetails(List<ARIES_Master_Tables_Edit_DetailsInput> updARIESMasterTablesInputs)
        {
            ControllerReturnObject objControllerReturnObject = new ControllerReturnObject();

            try
            {
                objControllerReturnObject.Data = ARIESMasterTablesService.InsARIESMasterTablesEditBatchAndDetails(p.DBConnectionStringForDataProcessing, updARIESMasterTablesInputs, "Edit", false);
                objControllerReturnObject.Message = "Stick sheet queued Successfully";
                objControllerReturnObject.Status = Convert.ToInt32(WebAPIStatus.Success);
            }
            catch (Exception ex)
            {
                objControllerReturnObject.Status = Convert.ToInt32(WebAPIStatus.Error);
                objControllerReturnObject.Data = "";
                objControllerReturnObject.Message = ex.Message;
            }
            return Ok(objControllerReturnObject);
        }

        [HttpGet]
        [ActionName("SelARIES_Master_Tables_Edit_Batchs")]
        public IHttpActionResult SelARIES_Master_Tables_Edit_Batchs()
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                List<SelARIES_Master_Tables_Edit_BatchsExtnl> batches = ARIESMasterTablesService.SelARIES_Master_Tables_Batchs(p.DBConnectionStringForDataProcessing, "Edit");

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = batches;
            }
            catch (Exception ex)
            {
                //IRExceptionHandler.HandleException(ProjectType.WebAPI, ex);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Error);
                returnData.Data = "";
                returnData.Message = ex.Message;
            }

            return Ok(returnData);
        }

        [HttpGet]
        [ActionName("SelARIES_Master_Tables_Edit_DetailsByBatchId")]
        public IHttpActionResult SelARIES_Master_Tables_Edit_DetailsByBatchId(int Batch_id)
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                List<SelARIES_Master_Tables_Edit_DetailsByBatchId> batchDetails = ARIESMasterTablesService.SelARIES_Master_Tables_Edit_DetailsByBatchId(p.DBConnectionStringForDataProcessing, Batch_id);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = batchDetails;
            }
            catch (Exception ex)
            {
                //IRExceptionHandler.HandleException(ProjectType.WebAPI, ex);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Error);
                returnData.Data = "";
                returnData.Message = ex.Message;
            }

            return Ok(returnData);
        }

        [HttpPost]
        [ActionName("UpdStickSheetDetailsAndMoveToQueueForDrop")]
        public IHttpActionResult InsARIESMasterTablesDropBatchAndDetails(List<ARIES_Master_Tables_Drop_DetailsInput> updARIESMasterTablesInputs)
        {
            ControllerReturnObject objControllerReturnObject = new ControllerReturnObject();

            try
            {
                objControllerReturnObject.Data = ARIESMasterTablesService.InsARIESMasterTablesDropBatchAndDetails(p.DBConnectionStringForDataProcessing, updARIESMasterTablesInputs, "Drop", false);
                objControllerReturnObject.Message = "Drop stick sheet queued Successfully";
                objControllerReturnObject.Status = Convert.ToInt32(WebAPIStatus.Success);
            }
            catch (Exception ex)
            {
                objControllerReturnObject.Status = Convert.ToInt32(WebAPIStatus.Error);
                objControllerReturnObject.Data = "";
                objControllerReturnObject.Message = ex.Message;
            }
            return Ok(objControllerReturnObject);
        }

        [HttpGet]
        [ActionName("SelARIES_Master_Tables_Drop_Batchs")]
        public IHttpActionResult SelARIES_Master_Tables_Drop_Batchs()
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                List<SelARIES_Master_Tables_Edit_BatchsExtnl> batches = ARIESMasterTablesService.SelARIES_Master_Tables_Batchs(p.DBConnectionStringForDataProcessing, "Drop");

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = batches;
            }
            catch (Exception ex)
            {
                //IRExceptionHandler.HandleException(ProjectType.WebAPI, ex);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Error);
                returnData.Data = "";
                returnData.Message = ex.Message;
            }

            return Ok(returnData);
        }

        [HttpGet]
        [ActionName("SelARIES_Master_Tables_All_Batchs")]
        public IHttpActionResult SelARIES_Master_Tables_All_Batchs()
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                List<SelARIES_Master_Tables_Edit_BatchsExtnl> batches = ARIESMasterTablesService.SelARIES_Master_Tables_Batchs(p.DBConnectionStringForDataProcessing, "");

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = batches;
            }
            catch (Exception ex)
            {
                //IRExceptionHandler.HandleException(ProjectType.WebAPI, ex);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Error);
                returnData.Data = "";
                returnData.Message = ex.Message;
            }

            return Ok(returnData);
        }

        [HttpPost]
        [ActionName("UpdExportFlagByBatch_id")]
        public IHttpActionResult UpdExportFlagByBatch_id(UpdExportFlag updExportFlag)
        {
            ControllerReturnObject objControllerReturnObject = new ControllerReturnObject();

            try
            {
                objControllerReturnObject.Data = ARIESMasterTablesService.UpdExportFlagByBatch_id(p.DBConnectionStringForDataProcessing, updExportFlag, true);
                objControllerReturnObject.Message = "Export flag has updated";
                objControllerReturnObject.Status = Convert.ToInt32(WebAPIStatus.Success);
            }
            catch (Exception ex)
            {
                objControllerReturnObject.Status = Convert.ToInt32(WebAPIStatus.Error);
                objControllerReturnObject.Data = "";
                objControllerReturnObject.Message = ex.Message;
            }
            return Ok(objControllerReturnObject);
        }

        [HttpGet]
        [ActionName("SelARIES_Master_Tables_Edit_DetailsByBatchIdsList")]
        public IHttpActionResult SelARIES_Master_Tables_Edit_DetailsByBatchIdsList(string Batch_ids)
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                List<SelARIES_Master_Tables_Edit_DetailsByBatchIdsListExtnl> lstRows = new List<SelARIES_Master_Tables_Edit_DetailsByBatchIdsListExtnl>();
                foreach (string item in Batch_ids.Split(',').ToArray())
                {
                    SelARIES_Master_Tables_Edit_DetailsByBatchIdsListExtnl row = new SelARIES_Master_Tables_Edit_DetailsByBatchIdsListExtnl();

                    List<SelARIES_Master_Tables_Edit_DetailsByBatchId> batchDetails = ARIESMasterTablesService.SelARIES_Master_Tables_Edit_DetailsByBatchId(p.DBConnectionStringForDataProcessing, Convert.ToInt32(item));
                    row.Batch_id = Convert.ToInt32(item);
                    row.BatchDetails = batchDetails;
                    lstRows.Add(row);
                }

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = lstRows;
            }
            catch (Exception ex)
            {
                //IRExceptionHandler.HandleException(ProjectType.WebAPI, ex);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Error);
                returnData.Data = "";
                returnData.Message = ex.Message;
            }

            return Ok(returnData);
        }

        [HttpGet]
        [ActionName("SelARIESDataForEditBatchNew")]
        public IHttpActionResult SelARIESDataForEditBatchNew()
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                List<HeaderInfoForEditStickSheetExtnl> headerInfo = ARIESMasterTablesService.SelARIESDataForEditBatchNew(p.DBConnectionStringForDataProcessing);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = headerInfo;
            }
            catch (Exception ex)
            {
                //IRExceptionHandler.HandleException(ProjectType.WebAPI, ex);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Error);
                returnData.Data = "";
                returnData.Message = ex.Message;
            }

            return Ok(returnData);
        }

        [HttpGet]
        [ActionName("SelARIESDataForEditBatchNewForApproval")]
        public IHttpActionResult SelARIESDataForEditBatchNewForApproval()
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                List<HeaderInfoForEditStickSheetForApprovalExtnl> headerInfo = ARIESMasterTablesService.SelARIESDataForEditBatchNewForApproval(p.DBConnectionStringForDataProcessing);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = headerInfo;
            }
            catch (Exception ex)
            {
                //IRExceptionHandler.HandleException(ProjectType.WebAPI, ex);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Error);
                returnData.Data = "";
                returnData.Message = ex.Message;
            }

            return Ok(returnData);
        }

        [HttpPost]
        [ActionName("UpdHeaderForEditBatchNew")]
        public IHttpActionResult UpdHeaderForEditBatchNew(List<HeaderInfoForEditStickSheetInput> updARIESMasterTablesInputs)
        {
            ControllerReturnObject objControllerReturnObject = new ControllerReturnObject();

            try
            {
                List<HeaderInfoForEditStickSheetExtnl> dbRecords = ARIESMasterTablesService.SelARIESDataForEditBatchNew(p.DBConnectionStringForDataProcessing);

                foreach (var item in updARIESMasterTablesInputs)
                {
                    HeaderInfoForEditStickSheetExtnl dbRecordsByWellIDRecord = dbRecords.Where(x => x.Well_ID == item.Well_ID).ToList().FirstOrDefault();

                    // If Well_Type or Planned_Drilled_Lateral_Length or Planned_Completed_Lateral_Length is changed
                    // then insert record into db
                    // other wise execute combo curve api
                    if (item.Well_Type != dbRecordsByWellIDRecord.Well_Type
                        || item.Planned_Drilled_Lateral_Length != Convert.ToDecimal(dbRecordsByWellIDRecord.Planned_Drilled_Lateral_Length)
                        || item.Planned_Completed_Lateral_Length != Convert.ToDecimal(dbRecordsByWellIDRecord.Planned_Completed_Lateral_Length))
                    {
                        List<HeaderInfoForEditStickSheetInput> insertRecord = new List<HeaderInfoForEditStickSheetInput>();
                        insertRecord.Add(item);
                        int rows = ARIESMasterTablesService.UpdHeaderForEditBatchNew(p.DBConnectionStringForDataProcessing, insertRecord, true);
                    }
                    else
                    {
                        List<HeaderInfoForEditStickSheetInput> insertRecord = new List<HeaderInfoForEditStickSheetInput>();
                        insertRecord.Add(item);
                        int rows = ARIESMasterTablesService.UpdHeaderForEditBatchNew(p.DBConnectionStringForDataProcessing, insertRecord, false);

                        List<SelWellDataForCCByWellIDExtnl> selWellDataForCCByWellIDExtnls = ARIESMasterTablesService.SelWellDataForCCByWellID(p.DBConnectionStringForDataProcessing, item.Well_ID);

                        if (selWellDataForCCByWellIDExtnls != null && selWellDataForCCByWellIDExtnls.Count > 0)
                        {
                            List<HeaderInfoForEditStickSheetInput> updARIESMasterTablesInputsUpdated = new List<HeaderInfoForEditStickSheetInput>();
                            HeaderInfoForEditStickSheetInput headerInfoForEditStickSheetInputUpdated = new HeaderInfoForEditStickSheetInput();

                            headerInfoForEditStickSheetInputUpdated.Well_ID = item.Well_ID;
                            headerInfoForEditStickSheetInputUpdated.Well_Report_Name = selWellDataForCCByWellIDExtnls[0].Well_Official_Name;
                            headerInfoForEditStickSheetInputUpdated.Drilling_Spacing_Unit = selWellDataForCCByWellIDExtnls[0].DSU_Prod_Zone_Assignment;
                            headerInfoForEditStickSheetInputUpdated.Development_Group = selWellDataForCCByWellIDExtnls[0].Dev_Grouping;
                            headerInfoForEditStickSheetInputUpdated.Type_Curve_Risk = selWellDataForCCByWellIDExtnls[0].Type_Curve_Risk;
                            headerInfoForEditStickSheetInputUpdated.Planned_Drilled_Lateral_Length = selWellDataForCCByWellIDExtnls[0].Planned_Drilled_Lateral_Length;
                            headerInfoForEditStickSheetInputUpdated.Planned_Completed_Lateral_Length = selWellDataForCCByWellIDExtnls[0].Planned_Completed_Lateral_Length;
                            headerInfoForEditStickSheetInputUpdated.PerfLateralLength = selWellDataForCCByWellIDExtnls[0].PerfLateralLength;

                            headerInfoForEditStickSheetInputUpdated.CustomNumber3 = selWellDataForCCByWellIDExtnls[0].CustomNumber3;
                            headerInfoForEditStickSheetInputUpdated.CustomNumber4 = selWellDataForCCByWellIDExtnls[0].CustomNumber4;

                            updARIESMasterTablesInputsUpdated.Add(headerInfoForEditStickSheetInputUpdated);

                            string mapPath = System.Web.Hosting.HostingEnvironment.MapPath("~");
                            ComboCurveAPI comboAPI = new ComboCurveAPI();
                            comboAPI.UpdateComboCurveForHeaderForEditBatchNew(updARIESMasterTablesInputsUpdated, "internal",
                                mapPath,
                                p.GetValueByKeyAppSettings("ComboCurveJSONFileName"),
                                p.GetValueByKeyAppSettings("ComboCurveAPIKey"));
                        }
                    }
                }

                


                //foreach (var item in updARIESMasterTablesInputs)
                //{
                //    List<SelWellDataForCCByWellIDExtnl> selWellDataForCCByWellIDExtnls = ARIESMasterTablesService.SelWellDataForCCByWellID(p.DBConnectionStringForDataProcessing, item.Well_ID);

                //    if (selWellDataForCCByWellIDExtnls != null && selWellDataForCCByWellIDExtnls.Count > 0)
                //    {
                //        List<HeaderInfoForEditStickSheetInput> updARIESMasterTablesInputsUpdated = new List<HeaderInfoForEditStickSheetInput>();
                //        HeaderInfoForEditStickSheetInput headerInfoForEditStickSheetInputUpdated = new HeaderInfoForEditStickSheetInput();

                //        headerInfoForEditStickSheetInputUpdated.Well_ID = item.Well_ID;
                //        headerInfoForEditStickSheetInputUpdated.Well_Report_Name = selWellDataForCCByWellIDExtnls[0].Well_Official_Name;
                //        headerInfoForEditStickSheetInputUpdated.Drilling_Spacing_Unit = selWellDataForCCByWellIDExtnls[0].DSU_Prod_Zone_Assignment;
                //        headerInfoForEditStickSheetInputUpdated.Development_Group = selWellDataForCCByWellIDExtnls[0].Dev_Grouping;
                //        headerInfoForEditStickSheetInputUpdated.Type_Curve_Risk = selWellDataForCCByWellIDExtnls[0].Type_Curve_Risk;
                //        headerInfoForEditStickSheetInputUpdated.Planned_Drilled_Lateral_Length = selWellDataForCCByWellIDExtnls[0].Planned_Drilled_Lateral_Length;
                //        headerInfoForEditStickSheetInputUpdated.Planned_Completed_Lateral_Length = selWellDataForCCByWellIDExtnls[0].Planned_Completed_Lateral_Length;
                //        headerInfoForEditStickSheetInputUpdated.PerfLateralLength = selWellDataForCCByWellIDExtnls[0].PerfLateralLength;

                //        headerInfoForEditStickSheetInputUpdated.CustomNumber3 = selWellDataForCCByWellIDExtnls[0].CustomNumber3;
                //        headerInfoForEditStickSheetInputUpdated.CustomNumber4 = selWellDataForCCByWellIDExtnls[0].CustomNumber4;

                //        updARIESMasterTablesInputsUpdated.Add(headerInfoForEditStickSheetInputUpdated);

                //        string mapPath = System.Web.Hosting.HostingEnvironment.MapPath("~");
                //        ComboCurveAPI comboAPI = new ComboCurveAPI();
                //        comboAPI.UpdateComboCurveForHeaderForEditBatchNew(updARIESMasterTablesInputsUpdated, "internal",
                //            mapPath,
                //            p.GetValueByKeyAppSettings("ComboCurveJSONFileName"),
                //            p.GetValueByKeyAppSettings("ComboCurveAPIKey"));
                //    }
                //}

                objControllerReturnObject.Data = 1;
                objControllerReturnObject.Message = "Stick sheet edited updated";
                objControllerReturnObject.Status = Convert.ToInt32(WebAPIStatus.Success);
            }
            catch (Exception ex)
            {
                objControllerReturnObject.Status = Convert.ToInt32(WebAPIStatus.Error);
                objControllerReturnObject.Data = "";
                objControllerReturnObject.Message = ex.Message;
            }
            return Ok(objControllerReturnObject);
        }

        [HttpPost]
        [ActionName("updDataSourceByWellIDInputs")]
        public IHttpActionResult UpdDataSourceToIMByWellID(List<UpdDataSourceByWellIDInput> updDataSourceByWellIDInputs)
        {
            ControllerReturnObject objControllerReturnObject = new ControllerReturnObject();

            try
            {
                int rows = ARIESMasterTablesService.UpdDataSourceToIMByWellID(p.DBConnectionStringForDataProcessing, updDataSourceByWellIDInputs);


                foreach (var item in updDataSourceByWellIDInputs)
                {
                    List<SelWellDataForCCByWellIDExtnl> selWellDataForCCByWellIDExtnls = ARIESMasterTablesService.SelWellDataForCCByWellID(p.DBConnectionStringForDataProcessing, item.Well_ID);

                    if (selWellDataForCCByWellIDExtnls != null && selWellDataForCCByWellIDExtnls.Count > 0)
                    {
                        List<HeaderInfoForEditStickSheetInput> updARIESMasterTablesInputsUpdated = new List<HeaderInfoForEditStickSheetInput>();
                        HeaderInfoForEditStickSheetInput headerInfoForEditStickSheetInputUpdated = new HeaderInfoForEditStickSheetInput();

                        headerInfoForEditStickSheetInputUpdated.Well_ID = item.Well_ID;
                        headerInfoForEditStickSheetInputUpdated.Well_Report_Name = selWellDataForCCByWellIDExtnls[0].Well_Official_Name;
                        headerInfoForEditStickSheetInputUpdated.Drilling_Spacing_Unit = selWellDataForCCByWellIDExtnls[0].DSU_Prod_Zone_Assignment;
                        headerInfoForEditStickSheetInputUpdated.Development_Group = selWellDataForCCByWellIDExtnls[0].Dev_Grouping;
                        headerInfoForEditStickSheetInputUpdated.Type_Curve_Risk = selWellDataForCCByWellIDExtnls[0].Type_Curve_Risk;
                        headerInfoForEditStickSheetInputUpdated.Planned_Drilled_Lateral_Length = selWellDataForCCByWellIDExtnls[0].Planned_Drilled_Lateral_Length;
                        headerInfoForEditStickSheetInputUpdated.Planned_Completed_Lateral_Length = selWellDataForCCByWellIDExtnls[0].Planned_Completed_Lateral_Length;
                        headerInfoForEditStickSheetInputUpdated.PerfLateralLength = selWellDataForCCByWellIDExtnls[0].PerfLateralLength;

                        headerInfoForEditStickSheetInputUpdated.CustomNumber3 = selWellDataForCCByWellIDExtnls[0].CustomNumber3;
                        headerInfoForEditStickSheetInputUpdated.CustomNumber4 = selWellDataForCCByWellIDExtnls[0].CustomNumber4;

                        updARIESMasterTablesInputsUpdated.Add(headerInfoForEditStickSheetInputUpdated);

                        string mapPath = System.Web.Hosting.HostingEnvironment.MapPath("~");
                        ComboCurveAPI comboAPI = new ComboCurveAPI();
                        comboAPI.UpdateComboCurveForHeaderForEditBatchNew(updARIESMasterTablesInputsUpdated, "internal",
                            mapPath,
                            p.GetValueByKeyAppSettings("ComboCurveJSONFileName"),
                            p.GetValueByKeyAppSettings("ComboCurveAPIKey"));
                    }
                }

                objControllerReturnObject.Data = 1;
                objControllerReturnObject.Message = "Stick sheet edited updated";
                objControllerReturnObject.Status = Convert.ToInt32(WebAPIStatus.Success);
            }
            catch (Exception ex)
            {
                objControllerReturnObject.Status = Convert.ToInt32(WebAPIStatus.Error);
                objControllerReturnObject.Data = "";
                objControllerReturnObject.Message = ex.Message;
            }
            return Ok(objControllerReturnObject);
        }


        [HttpPost]
        [ActionName("DeleteRejectedPreApprovalsByWellID")]
        public IHttpActionResult RejectDataSourceByWellID(List<RejectDataSourceByWellIDInput> rejectDataSourceByWellIDInputs)
        {
            ControllerReturnObject objControllerReturnObject = new ControllerReturnObject();

            try
            {
                int rows = ARIESMasterTablesService.RejectDataSourceByWellID(p.DBConnectionStringForDataProcessing, rejectDataSourceByWellIDInputs);

                objControllerReturnObject.Data = 1;
                objControllerReturnObject.Message = "Rejected selected wells";
                objControllerReturnObject.Status = Convert.ToInt32(WebAPIStatus.Success);
            }
            catch (Exception ex)
            {
                objControllerReturnObject.Status = Convert.ToInt32(WebAPIStatus.Error);
                objControllerReturnObject.Data = "";
                objControllerReturnObject.Message = ex.Message;
            }
            return Ok(objControllerReturnObject);
        }


        [HttpGet]
        [ActionName("TestProjectAndInfoLoader")]
        public IHttpActionResult TestProjectAndInfoLoader()
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                string mapPath = System.Web.Hosting.HostingEnvironment.MapPath("~");

                LoadAllProjectsAndRelaventInfo loadAllProjectsAndRelaventInfo = new LoadAllProjectsAndRelaventInfo();
                loadAllProjectsAndRelaventInfo.LoadProjectsAndRelaventInfo(p.GetValueByKeyAppSettings("ComboCurveAPIKey"), 
                    mapPath, p.GetValueByKeyAppSettings("ComboCurveJSONFileName"), p.DBConnection);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = "";
            }
            catch (Exception ex)
            {
                //IRExceptionHandler.HandleException(ProjectType.WebAPI, ex);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Error);
                returnData.Data = "";
                returnData.Message = ex.Message;
            }

            return Ok(returnData);
        }

        [HttpGet]
        [ActionName("TestMonthlyDataExport")]
        public IHttpActionResult TestMonthlyDataExport()
        {
            string projectID = "660c60d8eba8c456de3ac975";
            string scenarioID = "67520e88d46978d93704901a";
            string econRunID = "6772b321afa9a81999af22f2";

            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                string mapPath = System.Web.Hosting.HostingEnvironment.MapPath("~");

                LoadAllProjectsAndRelaventInfo loadAllProjectsAndRelaventInfo = new LoadAllProjectsAndRelaventInfo();
                loadAllProjectsAndRelaventInfo.ExportMonthlyData(p.GetValueByKeyAppSettings("ComboCurveAPIKey"),
                    mapPath, p.GetValueByKeyAppSettings("ComboCurveJSONFileName"), p.DBConnection, projectID, scenarioID, econRunID);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = "";
            }
            catch (Exception ex)
            {
                //IRExceptionHandler.HandleException(ProjectType.WebAPI, ex);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Error);
                returnData.Data = "";
                returnData.Message = ex.Message;
            }

            return Ok(returnData);
        }




    }
}
