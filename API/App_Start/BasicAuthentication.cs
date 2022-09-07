using API.Common;
using Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace API.App_Start
{
    public class BasicAuthentication: AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized); ;
            }
            else
            {
                string authenticationString = actionContext.Request.Headers.Authorization.Parameter;
                string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationString));
                string[] userNamepasswordArray = decodedAuthenticationToken.Split(':');

                Base p = new Base();

                if (UsersService.ValidateUserCredentials(p.DBConnection, userNamepasswordArray[0], userNamepasswordArray[1]).Count() > 0)
                {
                    //Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userNamepasswordArray[0]), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized); ;
                }
            }
        }

    }
}