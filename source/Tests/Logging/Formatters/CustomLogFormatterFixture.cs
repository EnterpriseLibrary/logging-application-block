﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Configuration;
using EnterpriseLibrary.Common.Configuration;
using EnterpriseLibrary.Common.TestSupport.Configuration;
using EnterpriseLibrary.Logging.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseLibrary.Logging.Formatters.Tests
{
    [TestClass]
    public class CustomLogFormatterFixture
    {
        [TestInitialize]
        public void SetUp()
        {
            AppDomain.CurrentDomain.SetData("APPBASE", Environment.CurrentDirectory);
        }

        private static ILogFormatter GetFormatter(string name, IConfigurationSource configurationSource)
        {
            var settings = LoggingSettings.GetLoggingSettings(configurationSource);
            return settings.Formatters.Get(name).BuildFormatter();
        }

        [TestMethod]
        public void CanBuildCustomLogFormatterFromGivenConfiguration()
        {
            CustomFormatterData customData
                = new CustomFormatterData("formatter", typeof(MockCustomLogFormatter));
            customData.SetAttributeValue(MockCustomProviderBase.AttributeKey, "value1");
            LoggingSettings settings = new LoggingSettings();
            settings.Formatters.Add(customData);
            DictionaryConfigurationSource configurationSource = new DictionaryConfigurationSource();
            configurationSource.Add(LoggingSettings.SectionName, settings);

            ILogFormatter formatter = GetFormatter("formatter", configurationSource);

            Assert.IsNotNull(formatter);
            Assert.AreSame(typeof(MockCustomLogFormatter), formatter.GetType());
            Assert.AreEqual("value1", ((MockCustomLogFormatter)formatter).customValue);
        }

        [TestMethod]
        public void CanBuildCustomLogFormatterFromSavedConfiguration()
        {
            CustomFormatterData customData
                = new CustomFormatterData("formatter", typeof(MockCustomLogFormatter));
            customData.SetAttributeValue(MockCustomProviderBase.AttributeKey, "value1");
            LoggingSettings settings = new LoggingSettings();
            settings.Formatters.Add(customData);

            IDictionary<string, ConfigurationSection> sections = new Dictionary<string, ConfigurationSection>(1);
            sections[LoggingSettings.SectionName] = settings;
            IConfigurationSource configurationSource
                = ConfigurationTestHelper.SaveSectionsInFileAndReturnConfigurationSource(sections);

            ILogFormatter formatter = GetFormatter("formatter", configurationSource);

            Assert.IsNotNull(formatter);
            Assert.AreSame(typeof(MockCustomLogFormatter), formatter.GetType());
            Assert.AreEqual("value1", ((MockCustomLogFormatter)formatter).customValue);
        }

        [TestMethod]
        public void CanDeserializeSerializedConfiguration()
        {
            LoggingSettings rwLoggingSettings = new LoggingSettings();
            rwLoggingSettings.Formatters.Add(new CustomFormatterData("formatter1", typeof(MockCustomLogFormatter)));

            IDictionary<string, ConfigurationSection> sections = new Dictionary<string, ConfigurationSection>();
            sections[LoggingSettings.SectionName] = rwLoggingSettings;
            IConfigurationSource configurationSource
                = ConfigurationTestHelper.SaveSectionsInFileAndReturnConfigurationSource(sections);

            LoggingSettings roLoggingSettings = (LoggingSettings)configurationSource.GetSection(LoggingSettings.SectionName);

            Assert.AreEqual(1, roLoggingSettings.Formatters.Count);
            Assert.IsNotNull(roLoggingSettings.Formatters.Get("formatter1"));
            Assert.AreSame(typeof(CustomFormatterData), roLoggingSettings.Formatters.Get("formatter1").GetType());
            Assert.AreSame(typeof(MockCustomLogFormatter), roLoggingSettings.Formatters.Get("formatter1").Type);
        }
    }
}
