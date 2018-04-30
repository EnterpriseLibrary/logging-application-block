// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Specialized;
using EnterpriseLibrary.Common.Configuration;
using EnterpriseLibrary.Common.TestSupport.Configuration;
using EnterpriseLibrary.Logging.Configuration;

namespace EnterpriseLibrary.Logging.Filters.Tests
{
    [ConfigurationElementType(typeof(CustomLogFilterData))]
    public class MockCustomLogFilter : MockCustomProviderBase, ILogFilter
    {
        public MockCustomLogFilter(NameValueCollection attributes)
            : base(attributes)
        {
        }

        public bool Filter(LogEntry log)
        {
            return true;
        }

        public string Name
        {
            get { return string.Empty; }
        }
    }
}
