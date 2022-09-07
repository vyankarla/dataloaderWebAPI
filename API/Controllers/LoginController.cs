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
using DataModel.InputModels;

namespace API.Controllers
{
    [Route("api/Login/{action}")]
    public class LoginController : ApiController
    {
        Base p = new Base();

        [HttpPost]
        [ActionName("Login")]
        public IHttpActionResult ValidateLoginCredentials([FromBody] LoginInput loginInput)
        {
            try
            {
                List<LoginExtnl> login = UsersService.ValidateUserCredentials(p.DBConnection, loginInput.Username, loginInput.Password);

                ControllerReturnObject returnData = new ControllerReturnObject();

                if (login.Count > 0)
                {
                    returnData.Status = Convert.ToInt32(WebAPIStatus.Success);
                    returnData.Data = login[0];
                }
                else
                {
                    returnData.Status = Convert.ToInt32(WebAPIStatus.Error);
                    returnData.Message = "Invalid Credentials.";
                }


                return Ok(returnData);
            }
            catch (Exception ex)
            {
                IRExceptionHandler.HandleException(ProjectType.WebAPI, ex);
                return BadRequest("An error occured while validating user.");
            }
        }

    }
}
