using Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Utilities;
using DataModel.ExternalModels;
using API.Common;
using static API.Common.Base;
using API.App_Start;

namespace API.Controllers
{
    [Route("api/Lookup/{action}")]
    [BasicAuthentication]
    public class LookupController : ApiController
    {
        Base p = new Base();

        [HttpGet]
        [ActionName("FileTypes")]
        public IHttpActionResult SelFileTypes()
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                List<FileTypesExtnl> fileTypesExtnl = LookupService.SelFileTypes(p.DBConnection);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = fileTypesExtnl;
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
        [ActionName("DatasetTypes")]
        public IHttpActionResult SelDatasetTypes()
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                List<DatasetTypesExtnl> datasetTypes = LookupService.SelDatasetTypes(p.DBConnection);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = datasetTypes;
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
        [ActionName("DatasourceTypes")]
        public IHttpActionResult SelDatasourceTypes()
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                List<DatasourceTypesExtnl> datasourceTypesExtnl = LookupService.SelDatasourceTypes(p.DBConnection);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = datasourceTypesExtnl;
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
        [ActionName("SelWellTypes")]
        public IHttpActionResult SelWellTypes()
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                List<WellTypesExtnl> wellTypesExtnls = LookupService.SelWellTypes(p.DBConnection);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = wellTypesExtnls;
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
