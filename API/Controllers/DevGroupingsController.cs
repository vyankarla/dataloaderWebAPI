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
using static API.Common.Base;

namespace API.Controllers
{
    [Route("api/DevGroupings/{action}")]
    [BasicAuthentication]
    public class DevGroupingsController : ApiController
    {
        Base p = new Base();

        [HttpGet]
        [ActionName("SelDevGroupings")]
        public IHttpActionResult SelDevGroupings()
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                List<DevGroupingsExtnl> devGroupings = DevGroupingsService.SelDevGroupings(p.DBConnection);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = devGroupings;
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
        [ActionName("SelDevGroupingsByGroupingID")]
        public IHttpActionResult SelDevGroupingsByGroupingID(int Dev_Groupings_Id)
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                List<DevGroupingsExtnl> devGroupings = DevGroupingsService.SelDevGroupingsByGroupingID(p.DBConnection, Dev_Groupings_Id);

                if (devGroupings != null && devGroupings.Count > 0)
                {
                    returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                    returnData.Data = devGroupings[0];
                }
                else
                {
                    returnData.Status = Convert.ToInt32(WebAPIStatus.Error);
                    returnData.Data = "Invalid Dev grouping id";
                }
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
        [ActionName("InsUpdDevGroupings")]
        public IHttpActionResult InsUpdDevGroupings(DevGroupingsInput devGroupingsInput)
        {
            ControllerReturnObject objControllerReturnObject = new ControllerReturnObject();

            try
            {
                objControllerReturnObject.Data = DevGroupingsService.InsUpdDevGroupings(p.DBConnection, devGroupingsInput);
                objControllerReturnObject.Message = "Dev Grouping Saved Successfully";
                objControllerReturnObject.Status = Convert.ToInt32(WebAPIStatus.Success);
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("budget_input.Dev_Groupings".ToLower()))
                {
                    objControllerReturnObject.Status = Convert.ToInt32(WebAPIStatus.Error);
                    objControllerReturnObject.Data = "";
                    objControllerReturnObject.Message = "Dev Grouping Name already exists.";
                }
                else
                {
                    objControllerReturnObject.Status = Convert.ToInt32(WebAPIStatus.Error);
                    objControllerReturnObject.Data = "";
                    objControllerReturnObject.Message = ex.Message;
                }
            }
            return Ok(objControllerReturnObject);
        }


    }
}