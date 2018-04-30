using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using EnterpriseLibrary.Logging.Filters;

namespace EnterpriseLibrary.Logging.BVT.Logging
{
    [TestClass]
    public class LogWriterNoConfigurationFixture : EntLibFixtureBase
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ExceptionIsThrownWhenNotConfigured()
        {
            Logger.Write("Test");
        }
    }
}
