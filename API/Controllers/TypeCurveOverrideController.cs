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
    [Route("api/    /{action}")]
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

    }
}
