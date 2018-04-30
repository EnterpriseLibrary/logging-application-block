// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnterpriseLibrary.Common.Configuration;
using EnterpriseLibrary.Logging.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnterpriseLibrary.Common.TestSupport.ContextBase;
using EnterpriseLibrary.Common.Configuration.Fluent;
using EnterpriseLibrary.Logging.Filters;

namespace EnterpriseLibrary.Logging.Tests.Configuration.Fluent
{
    public abstract class Given_LogEnabledFilterBuilder : Given_LoggingSettingsInConfigurationSourceBuilder
    {
        protected ILoggingConfigurationFilterLogEnabled logEnabledFilterBuilder;
        protected string logEnabledFilterName = "log enabled filter";

        protected override void Arrange()
        {
            base.Arrange();

            logEnabledFilterBuilder = base.ConfigureLogging.WithOptions.FilterEnableOrDisable(logEnabledFilterName);
        }

        protected LogEnabledFilterData GetLogEnabledFilterData()
        {
            return GetLoggingConfiguration().LogFilters.OfType<LogEnabledFilterData>().FirstOrDefault();
        }
    }

    [TestClass]
    public class When_CreatinigLogEnabledFilterBuilder : Given_LogEnabledFilterBuilder
    {
        [TestMethod]
        public void Then_ConfigurationContainsLogEnabledFilter()
        {
            Assert.IsTrue(GetLoggingConfiguration().LogFilters.OfType<LogEnabledFilterData>().Any());
        }

        [TestMethod]
        public void Then_LoggingIsDisabledByDefault()
        {
            Assert.IsFalse(GetLogEnabledFilterData().Enabled);
        }

        [TestMethod]
        public void Then_LogEnabledFilterHasAppropriateName()
        {
            Assert.AreEqual(logEnabledFilterName, GetLogEnabledFilterData().Name);
        }

        [TestMethod]
        public void Then_LogEnabledFilterHasCorrectType()
        {
            Assert.AreEqual(typeof(LogEnabledFilter), GetLogEnabledFilterData().Type);
        }
    }

    [TestClass]
    public class When_CreatinigLogEnabledFilterBuilderPassingNullForName : Given_LoggingSettingsInConfigurationSourceBuilder
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Then_FilterEnableOrDisable_ThrowsArgumentException()
        {
            ConfigureLogging.WithOptions.FilterEnableOrDisable(null);
        }
    }


    [TestClass]
    public class When_EnablingLoggingOnLogEnabledFilterBuilder : Given_LogEnabledFilterBuilder
    {
        protected override void Act()
        {
            logEnabledFilterBuilder.Enable();
        }

        [TestMethod]
        public void Then_LoggingIsEnabled()
        {
            Assert.IsTrue(GetLogEnabledFilterData().Enabled);
        }
    }
}
