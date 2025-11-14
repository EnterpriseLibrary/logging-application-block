using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Practices.EnterpriseLibrary.Logging.Database.Tests
{
    public class NetCoreHelper
    {
        public static ConfigurationSection LookupConfigSection(string sectionName)
        {
            var configMap = new ExeConfigurationFileMap();
            configMap.ExeConfigFilename = "Microsoft.Practices.EnterpriseLibrary.Logging.Database.Tests.dll.config";
            var config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            return config.GetSection(sectionName);
        }
    }
}
