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
    [Route("api/SwimLaneInterest/{action}")]
    [BasicAuthentication]
    public class SwimLaneInterestController : ApiController
    {
        Base p = new Base();

        [NonAction]
        [HttpGet]
        [ActionName("SelDataForSwimLaneInterests")]
        public IHttpActionResult SelDataForSwimLaneInterests(int DSUHeaderID)
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                SwimLaneInterests swimLaneInterests = DSUSwimLanesService.SelDataForSwimLaneInterests(p.DBConnection, DSUHeaderID);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = swimLaneInterests;
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
        [ActionName("SelSWIMLaneOwnershipPivotView")]
        public IHttpActionResult SelSWIMLaneOwnershipPivotView(int DSUHeaderID)
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                List<SWIMLaneOwnershipPivotViewExtnl> swimLaneOwnership = DSUSwimLanesService.SelSWIMLaneOwnershipPivotView(p.DBConnection, DSUHeaderID);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = swimLaneOwnership;
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
        [ActionName("UpdateSwimLaneOwnership")]
        public IHttpActionResult UpdateSwimLaneOwnership([FromBody] SwimLaneInterestsInput swimLaneInterestsInput)
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                if (swimLaneInterestsInput.swimLaneOwnershipPivotViewExtnls != null && swimLaneInterestsInput.swimLaneOwnershipPivotViewExtnls.Count > 0)
                {
                    DSUSwimLanesService.UpdConfidenceLevelDSUHeaderByDSUHeaderID(p.DBConnection,
                        swimLaneInterestsInput.swimLaneOwnershipPivotViewExtnls[0].DSU_Header_Id, swimLaneInterestsInput.Confidence_Level,
                        swimLaneInterestsInput.Comments, swimLaneInterestsInput.LoggedInUserID, swimLaneInterestsInput.LoggedInUserName);

                    DSUSwimLanesService.UpdDSU_Swim_Lane_OwnershipByProducingZoneAndSwimLaneID(p.DBConnection, swimLaneInterestsInput);

                    returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                    returnData.Data = "";
                    returnData.Message = "Updated successfully";
                }
                else
                {
                    returnData.Status = Convert.ToInt32(WebAPIStatus.Error);
                    returnData.Data = "";
                    returnData.Message = "Please pass swimlanes to update";
                }
            }
            catch (Exception ex)
            {
                returnData.Status = Convert.ToInt32(WebAPIStatus.Error);
                returnData.Data = "";
                returnData.Message = ex.Message;
            }

            return Ok(returnData);
        }



    }
}
