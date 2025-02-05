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

namespace API
{
    [Route("api/Users/{action}")]
    [BasicAuthentication]
    public class UsersController : ApiController
    {
        Base p = new Base();


        [HttpGet]
        [ActionName("SelUsers")]
        public IHttpActionResult SelUsersList()
        {
            ControllerReturnObject returnData = new ControllerReturnObject();
            try
            {
                List<UsersExtnl> usersExtnl = UsersService.SelUsersList(p.DBConnection).Where(x => x.isAdmin == false).ToList();

                returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                returnData.Data = usersExtnl;
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
        [ActionName("InsUpdUsers")]
        public IHttpActionResult InsUpdUsers(UsersInput usersInput)
        {
            ControllerReturnObject objControllerReturnObject = new ControllerReturnObject();

            try
            {
                objControllerReturnObject.Data = UsersService.InsUpdUsers(p.DBConnection, usersInput);
                objControllerReturnObject.Message = "User details Saved Successfully";
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