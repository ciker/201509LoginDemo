using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Blogs.Helper
{
    public class ConfigHelper
    {

        public static string GetAppSettings(string key)
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
                return ConfigurationManager.AppSettings[key].ToString();
            return string.Empty;
        }
    }
}
