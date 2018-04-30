using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnterpriseLibrary.Common.Configuration;
using EnterpriseLibrary.Common.Configuration.Fluent;
using EnterpriseLibrary.Logging.Configuration;
using EnterpriseLibrary.Logging.TraceListeners;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseLibrary.Logging.BVT.DatabaseListener
{
    [TestClass]
    public class DatabaseFluentConfigurationFixture : LoggingFixtureBase
    {
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }
    }
}
