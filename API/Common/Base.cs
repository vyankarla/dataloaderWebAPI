using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Common
{
    public class Base
    {
        public string DBConnection
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString();
            }
        }

        public string DBConnectionStringForDataProcessing
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["DBConnectionStringForDataProcessing"].ToString();
            }
        }

        public string GetValueByKeyAppSettings(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }

        public enum WebAPIStatus
        {
            Success = 1,
            Error = 2,
            Warning = 3
        }

    }
}