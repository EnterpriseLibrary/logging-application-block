// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnterpriseLibrary.Common.TestSupport.ContextBase;
using EnterpriseLibrary.Common.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EnterpriseLibrary.Logging.Configuration;
using System.Diagnostics;
using EnterpriseLibrary.Logging.TraceListeners;
using System.Messaging;
using System.Collections.Specialized;

namespace EnterpriseLibrary.Logging.Tests.Configuration
{
    public abstract class Given_ConfigurationSourceBuilder : ArrangeActAssert
    {
        protected ConfigurationSourceBuilder ConfigurationSourceBuilder;

        protected override void Arrange()
        {
            ConfigurationSourceBuilder = new ConfigurationSourceBuilder();
        }

        public IConfigurationSource GetConfigurationSource()
        {
            DictionaryConfigurationSource configSource = new DictionaryConfigurationSource();
            ConfigurationSourceBuilder.UpdateConfigurationWithReplace(configSource);
            return configSource;
        }
    }

    [TestClass]
    public class When_ConfiguringLoggongOnConfigurationSourceBuilder : Given_ConfigurationSourceBuilder
    {
        protected override void Arrange()
        {
            base.Arrange();

            ConfigurationSourceBuilder.ConfigureLogging();
        }

        [TestMethod]
        public void Then_ConfigurationSourceContainsLoggingSettings()
        {
            var configurationSource = new DictionaryConfigurationSource();
            ConfigurationSourceBuilder.UpdateConfigurationWithReplace(configurationSource);

            Assert.IsNotNull(configurationSource.GetSection(LoggingSettings.SectionName));
        }

        [TestMethod]
        public void Then_RevertImpersonationIsTrue()
        {
            var configurationSource = GetConfigurationSource();
            var loggingSettings = (LoggingSettings)configurationSource.GetSection(LoggingSettings.SectionName);

            Assert.IsTrue(loggingSettings.RevertImpersonation);
        }

        [TestMethod]
        public void Then_EnableTracingIsTrue()
        {
            var configurationSource = GetConfigurationSource();
            var loggingSettings = (LoggingSettings)configurationSource.GetSection(LoggingSettings.SectionName);

            Assert.IsTrue(loggingSettings.TracingEnabled);
        }


        [TestMethod]
        public void Then_LogWarningsWhenNoCategoryExistsIsTrue()
        {
            var configurationSource = GetConfigurationSource();
            var loggingSettings = (LoggingSettings)configurationSource.GetSection(LoggingSettings.SectionName);

            Assert.IsTrue(loggingSettings.LogWarningWhenNoCategoriesMatch);
        }

    }
}
