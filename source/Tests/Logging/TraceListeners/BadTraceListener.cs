// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;
using EnterpriseLibrary.Logging.TraceListeners;

namespace EnterpriseLibrary.Logging.Tests.TraceListeners
{
    public class BadTraceListener : CustomTraceListener
    {
        private Exception exceptionToThrow;

        public BadTraceListener(Exception exceptionToThrow)
        {
            this.exceptionToThrow = exceptionToThrow;
        }

        public override void Write(string message)
        {
            throw exceptionToThrow;
        }

        public override void WriteLine(string message)
        {
            throw exceptionToThrow;
        }
    }
}
