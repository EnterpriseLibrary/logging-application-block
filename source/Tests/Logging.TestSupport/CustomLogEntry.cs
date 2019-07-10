// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;

namespace Microsoft.Practices.EnterpriseLibrary.Logging.TestSupport
{
    [Serializable]
    public class CustomLogEntry : LogEntry
    {
        public CustomLogEntry()
            : base()
        {
        }

        public string AcmeCoField1 = string.Empty;
        public string AcmeCoField2 = string.Empty;
        public string AcmeCoField3 = string.Empty;

        private string propertyValue = "myPropertyValue";

        public string MyProperty
        {
            get { return propertyValue; }
            set { propertyValue = value; }
        }

        public string MyPropertyThatReturnsNull
        {
            get { return null; }
            set { }
        }

        public string PropertyNotReadable
        {
            set { }
        }

        public string this[int index]
        {
            get { return null; }
            set { }
        }

        public string MyPropertyThatThrowsException
        {
            get { throw new Exception(); }
            set { }
        }
    }
}
