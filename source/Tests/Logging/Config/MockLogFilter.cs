// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using EnterpriseLibrary.Logging.Filters;
using System;

namespace EnterpriseLibrary.Logging.Tests.Config
{
    public class MockLogFilter : ILogFilter
    {
        public bool Filter(LogEntry log)
        {
            throw new NotImplementedException();
        }

        public string Name
        {
            get { return "Mock"; }
        }
    }
}
