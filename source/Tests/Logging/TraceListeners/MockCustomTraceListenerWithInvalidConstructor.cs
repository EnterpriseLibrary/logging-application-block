// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Specialized;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;

namespace Microsoft.Practices.EnterpriseLibrary.Logging.Tests.TraceListeners
{
    public class MockCustomTraceListenerWithInvalidConstructor
        : CustomTraceListener
    {
        internal const string AttributeKey = "attribute";
        internal readonly static string[] SupportedAttributes = new string[] { AttributeKey };

        internal string initData;

        // for sys.diags
        public MockCustomTraceListenerWithInvalidConstructor(string initData, string ignored)
        {
            this.initData = initData;
        }

        internal string Attribute
        {
            get { return Attributes[AttributeKey]; }
        }

        public override void Write(string message)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void WriteLine(string message)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override string[] GetSupportedAttributes()
        {
            return SupportedAttributes;
        }
    }
}
