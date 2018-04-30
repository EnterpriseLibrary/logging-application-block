// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Runtime.InteropServices;
using EnterpriseLibrary.Logging.ExtraInformation.Helpers;

namespace EnterpriseLibrary.Logging.ExtraInformation.Tests
{
    public class MockContextUtils : IContextUtils
    {
        public string GetActivityId()
        {
            throw new COMException();
        }

        public string GetApplicationId()
        {
            throw new COMException();
        }

        public string GetTransactionId()
        {
            throw new COMException();
        }

        public string GetDirectCallerAccountName()
        {
            throw new COMException();
        }

        public string GetOriginalCallerAccountName()
        {
            throw new COMException();
        }
    }
}

