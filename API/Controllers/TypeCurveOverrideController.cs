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
    [Route("api/TypeCurveOverride/{action}")]
    [BasicAuthentication]
    public class TypeCurveOverrideController : ApiController
    {
        Base p = new Base();

        [HttpGet]
        [ActionName("SelWellHeadersInfo")]
        public IHttpActionResult SelWellHeadersInfo()
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                List<HeaderInfoExtnl> headerInfoExtnls = TypeCurveOverrideService.SelWellHeadersInfo(p.DBConnectionStringForDataProcessing);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = headerInfoExtnls;
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
        [ActionName("UpdTypeCurveOverrideByWellID")]
        public IHttpActionResult UpdTypeCurveOverrideByWellID(UpdTypeCurveOverrideInput updTypeCurveOverrideInput)
        {
            ControllerReturnObject objControllerReturnObject = new ControllerReturnObject();

            try
            {
                int rows = TypeCurveOverrideService.UpdTypeCurveOverrideByWellID(p.DBConnectionStringForDataProcessing, updTypeCurveOverrideInput);

                if (rows > 0)
                {
                    objControllerReturnObject.Data = rows;
                    objControllerReturnObject.Message = "Type Curve Override has Updated.";
                    objControllerReturnObject.Status = Convert.ToInt32(WebAPIStatus.Success);
                }
                else
                {
                    objControllerReturnObject.Status = Convert.ToInt32(WebAPIStatus.Error);
                    objControllerReturnObject.Data = "";
                    objControllerReturnObject.Message = "An error occured while updating. Please retry.";
                }
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
        [ActionName("UpdTypeCurveOverrideByWellIDList")]
        public IHttpActionResult UpdTypeCurveOverrideByWellIDList(List<UpdTypeCurveOverrideNewInput> updTypeCurveOverrideNewInputs)
        {
            ControllerReturnObject objControllerReturnObject = new ControllerReturnObject();

            try
            {
                int rows = 0;
                foreach (var item in updTypeCurveOverrideNewInputs)
                {
                    List<Type_Curve_MilestonesInput> type_Curve_MilestonesInputs = new List<Type_Curve_MilestonesInput>();

                    UpdTypeCurveOverrideInput updTypeCurveOverrideInputMain = new UpdTypeCurveOverrideInput();
                    updTypeCurveOverrideInputMain.WellID = item.Well_ID;
                    updTypeCurveOverrideInputMain.Type_Curve_Override = item.Type_Curve_Override;
                    updTypeCurveOverrideInputMain.Row_Changed_By = item.Row_Changed_By;

                    for (int i = 0; i < 5; i++)
                    {
                        Type_Curve_MilestonesInput type_Curve_MilestonesInput = new Type_Curve_MilestonesInput();
                        type_Curve_MilestonesInput.Well_ID = item.Well_ID;
                        type_Curve_MilestonesInput.Data_Source = "Web App";
                        
                        type_Curve_MilestonesInput.Comments = "";
                        type_Curve_MilestonesInput.Row_Created_By = item.Row_Changed_By;
                        type_Curve_MilestonesInput.Row_Created_Date = DateTime.UtcNow;
                        type_Curve_MilestonesInput.Row_Changed_By = item.Row_Changed_By;
                        type_Curve_MilestonesInput.Row_Changed_Date = DateTime.UtcNow;
                        type_Curve_MilestonesInput.Active_Ind = "Y";

                        switch (i)
                        {
                            case 0:
                                type_Curve_MilestonesInput.Type_Curve_Milestone = "Inventory";
                                type_Curve_MilestonesInput.Type_Curve_Name = item.Inventory_Milestone;
                                break;
                            case 1:
                                type_Curve_MilestonesInput.Type_Curve_Milestone = "Package Preview";
                                type_Curve_MilestonesInput.Type_Curve_Name = item.Package_review_Milestone;
                                break;
                            case 2:
                                type_Curve_MilestonesInput.Type_Curve_Milestone = "Final Tech Review";
                                type_Curve_MilestonesInput.Type_Curve_Name = item.Final_Tech_Review;
                                break;
                            case 3:
                                type_Curve_MilestonesInput.Type_Curve_Milestone = "Pre-Frac Review";
                                type_Curve_MilestonesInput.Type_Curve_Name = item.Pre_frac_Milestone;
                                break;
                            case 4:
                                type_Curve_MilestonesInput.Type_Curve_Milestone = "Type Curve Override";
                                type_Curve_MilestonesInput.Type_Curve_Name = item.Type_Curve_Override;
                                break;
                        }
                        if (!(string.IsNullOrEmpty(type_Curve_MilestonesInput.Type_Curve_Name)))
                        {
                            type_Curve_MilestonesInputs.Add(type_Curve_MilestonesInput);
                        }
                    }

                    rows = rows + TypeCurveOverrideService.UpdTypeCurveOverrideByWellIDList(p.DBConnectionStringForDataProcessing,
                        updTypeCurveOverrideInputMain, type_Curve_MilestonesInputs);
                }
                
                
                //int rows = TypeCurveOverrideService.UpdTypeCurveOverrideByWellID(p.DBConnectionStringForDataProcessing, updTypeCurveOverrideInput);

                if (rows > 0)
                {

                    string mapPath = System.Web.Hosting.HostingEnvironment.MapPath("~");
                    ComboCurveAPI comboAPI = new ComboCurveAPI();
                    comboAPI.UpdateComboCurveForTypeCurveOverrideByWellIDList(updTypeCurveOverrideNewInputs, "internal",
                        mapPath,
                        p.GetValueByKeyAppSettings("ComboCurveJSONFileName"),
                        p.GetValueByKeyAppSettings("ComboCurveAPIKey"));

                    objControllerReturnObject.Data = rows;
                    objControllerReturnObject.Message = "Type Curve Override has Updated.";
                    objControllerReturnObject.Status = Convert.ToInt32(WebAPIStatus.Success);
                }
                else
                {
                    objControllerReturnObject.Status = Convert.ToInt32(WebAPIStatus.Error);
                    objControllerReturnObject.Data = "";
                    objControllerReturnObject.Message = "An error occured while updating. Please retry.";
                }
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
        [ActionName("TestServerPath")]
        public IHttpActionResult TestServerPath()
        {
            ControllerReturnObject objControllerReturnObject = new ControllerReturnObject();

            try
            {
                string mapPath = System.Web.Hosting.HostingEnvironment.MapPath("~");
                return Ok(mapPath);
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
