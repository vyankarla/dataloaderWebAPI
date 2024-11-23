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
    [Route("api/TypeCurveAssignment/{action}")]
    [BasicAuthentication]
    public class TypeCurveAssignmentController : ApiController
    {

        Base p = new Base();

        [HttpGet]
        [ActionName("SelTypeCurveAssignments")]
        public IHttpActionResult SelWellHeadersInfo()
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                List<TypeCurveAssignmentExtnl> assignments = TypeCurveAssignmentService.SelTypeCurveAssignments(p.DBConnectionStringForDataProcessing);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = assignments;
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
        [ActionName("SelDistinctTypeCurves")]
        public IHttpActionResult SelDistinctTypeCurves()
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                List<TypeCurvesExtnl> typeCurves = TypeCurveAssignmentService.SelDistinctTypeCurves(p.DBConnectionStringForDataProcessing);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = typeCurves;
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
        [ActionName("UpdTypeCurveAssignmentByAssignmentID")]
        public IHttpActionResult UpdTypeCurveAssignmentByAssignmentID(UpdTypeCurveAssignmentInput updTypeCurveAssignmentInput)
        {
            ControllerReturnObject objControllerReturnObject = new ControllerReturnObject();

            try
            {
                int rows = TypeCurveAssignmentService.UpdTypeCurveAssignmentByAssignmentID(p.DBConnectionStringForDataProcessing, updTypeCurveAssignmentInput);

                objControllerReturnObject.Data = rows;
                objControllerReturnObject.Message = "Type Curve Assignment has Updated.";
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
