using API.App_Start;
using API.Common;
using DataModel.ExternalModels;
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
    [Route("api/DSUHeaders/{action}")]
    [BasicAuthentication]
    public class DSUHeadersController : ApiController
    {
        Base p = new Base();

        [HttpGet]
        [ActionName("SelDSUHeaders")]
        public IHttpActionResult SelDSUHeaders()
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                List<DSUHeadersExtnl> DSUHeadersExtnl = DSUHeaderService.SelDSUHeaders(p.DBConnectionStringForDataProcessing);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = DSUHeadersExtnl;
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
        [ActionName("SelDSUHeaderDetailsByDSUHeaderID")]
        public IHttpActionResult SelDSUHeaderDetailsByDSUHeaderID(int DSU_Header_Id)
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                List<DSUHeadersExtnl> DSUHeadersExtnl = DSUHeaderService.SelDSUHeaders(p.DBConnectionStringForDataProcessing);

                if (DSUHeadersExtnl.Where(x => x.DSU_Header_Id == DSU_Header_Id).ToList().Count > 0)
                {
                    returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                    returnData.Data = DSUHeadersExtnl.Where(x => x.DSU_Header_Id == DSU_Header_Id).ToList()[0];
                }
                else
                {
                    returnData.Status = Convert.ToInt32(WebAPIStatus.Error);
                    returnData.Data = "";
                    returnData.Message = "Please check dsu header id once.";
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

        [HttpGet]
        [ActionName("SelDSUHeaderHistoryByDSUHeaderId")]
        public IHttpActionResult SelDSUHeaderHistoryByDSUHeaderId(int DSU_Header_Id)
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                List<DSUHeadersExtnlHistory> history = DSUHeaderService.SelDSUHeaderHistoryByDSUHeaderId(p.DBConnectionStringForDataProcessing, DSU_Header_Id);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = history;
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
        [ActionName("SelWellsInDSUByDSUHeaderID")]
        public IHttpActionResult SelWellsInDSUByDSUHeaderID(int DSU_Header_Id)
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                List<DSUHeaderWells> history = DSUHeaderService.SelWellsInDSUByDSUHeaderID(p.DBConnectionStringForDataProcessing, DSU_Header_Id);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = history;
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
