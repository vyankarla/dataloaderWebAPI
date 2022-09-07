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
    [Route("api/DatasetTypeColumns/{action}")]
    [BasicAuthentication]
    public class DatasetTypeColumnsController : ApiController
    {

        Base p = new Base();

        [HttpGet]
        [ActionName("DatasetTypeColumns")]
        public IHttpActionResult SelDatasetTypeColumns()
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                List<DatasetTypeColumnsExtnl> datasetTypesExtnls = DatasetTypeColumnsService.SelDatasetTypeColumns(p.DBConnection);

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = datasetTypesExtnls;
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
