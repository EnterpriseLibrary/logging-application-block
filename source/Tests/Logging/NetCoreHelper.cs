using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Microsoft.Practices.EnterpriseLibrary.Logging.Tests
{
    public class NetCoreHelper
    {
        public static ConfigurationSection LookupConfigSection(string sectionName)
        {
            var configMap = new ExeConfigurationFileMap();
            configMap.ExeConfigFilename = "App.config";
            var config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            return config.GetSection(sectionName);
        }
    }
}
