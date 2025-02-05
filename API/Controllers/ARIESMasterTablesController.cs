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

        [HttpPost]
        [ActionName("UpdHeaderForEditBatchNew")]
        public IHttpActionResult UpdHeaderForEditBatchNew(List<HeaderInfoForEditStickSheetInput> updARIESMasterTablesInputs)
        {
            ControllerReturnObject objControllerReturnObject = new ControllerReturnObject();

            try
            {
                //int rows = ARIESMasterTablesService.UpdHeaderForEditBatchNew(p.DBConnectionStringForDataProcessing, updARIESMasterTablesInputs);

                string mapPath = System.Web.Hosting.HostingEnvironment.MapPath("~");
                ComboCurveAPI comboAPI = new ComboCurveAPI();
                comboAPI.UpdateComboCurveForHeaderForEditBatchNew(updARIESMasterTablesInputs, "internal",
                    mapPath,
                    p.GetValueByKeyAppSettings("ComboCurveJSONFileName"),
                    p.GetValueByKeyAppSettings("ComboCurveAPIKey"));

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




    }
}
