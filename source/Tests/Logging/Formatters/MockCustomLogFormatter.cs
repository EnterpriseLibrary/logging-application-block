// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Specialized;
using EnterpriseLibrary.Common.Configuration;
using EnterpriseLibrary.Common.TestSupport.Configuration;
using EnterpriseLibrary.Logging.Configuration;

namespace EnterpriseLibrary.Logging.Formatters.Tests
{
    [ConfigurationElementType(typeof(CustomFormatterData))]
    public class MockCustomLogFormatter
        : MockCustomProviderBase, ILogFormatter
    {
        public MockCustomLogFormatter(NameValueCollection attributes)
            : base(attributes)
        {
        }

        public string Format(LogEntry log)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
