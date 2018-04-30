// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using EnterpriseLibrary.Common.Configuration;
using EnterpriseLibrary.Logging.Configuration;

namespace EnterpriseLibrary.Logging.TestSupport
{
    public class MockLogObjectsHelper
    {
        public DictionaryConfigurationSource configurationSource;
        public LoggingSettings loggingSettings;

        public MockLogObjectsHelper()
        {
            loggingSettings = new LoggingSettings();
            configurationSource = new DictionaryConfigurationSource();
            configurationSource.Add(LoggingSettings.SectionName, loggingSettings);
        }
    }
}
