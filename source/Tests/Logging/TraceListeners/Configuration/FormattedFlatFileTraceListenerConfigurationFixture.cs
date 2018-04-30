﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using EnterpriseLibrary.Common.Configuration;
using EnterpriseLibrary.Common.TestSupport.Configuration;
using EnterpriseLibrary.Logging.Configuration;
using EnterpriseLibrary.Logging.Formatters;
using EnterpriseLibrary.Logging.TestSupport;
using EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseLibrary.Logging.Tests.TraceListeners.Configuration
{
    [TestClass]
    public class FormattedFlatFileTraceListenerConfigurationFixture
    {
        [TestInitialize]
        public void SetUp()
        {
            AppDomain.CurrentDomain.SetData("APPBASE", Environment.CurrentDirectory);
        }

        private static TraceListener GetListener(string name, IConfigurationSource configurationSource)
        {
            var settings = LoggingSettings.GetLoggingSettings(configurationSource);
            return settings.TraceListeners.Get(name).BuildTraceListener(settings);
        }

        [TestMethod]
        public void ListenerDataIsCreatedCorrectly()
        {
            FlatFileTraceListenerData listenerData = new FlatFileTraceListenerData("listener", "log.txt", "---header---", "+++footer+++", "formatter");

            Assert.AreSame(typeof(FlatFileTraceListener), listenerData.Type);
            Assert.AreSame(typeof(FlatFileTraceListenerData), listenerData.ListenerDataType);
            Assert.AreEqual("listener", listenerData.Name);
            Assert.AreEqual("log.txt", listenerData.FileName);
            Assert.AreEqual("---header---", listenerData.Header);
            Assert.AreEqual("+++footer+++", listenerData.Footer);
            Assert.AreEqual("formatter", listenerData.Formatter);
        }

        [TestMethod]
        public void CanDeserializeSerializedConfiguration()
        {
            string name = "name";
            string filename = "filename";
            string header = "header";
            string footer = "footer";
            string formatter = "formatter";

            TraceListenerData data = new FlatFileTraceListenerData(name, filename, header, footer,
                                                                   formatter, TraceOptions.Callstack);

            LoggingSettings settings = new LoggingSettings();
            settings.TraceListeners.Add(data);

            IDictionary<string, ConfigurationSection> sections = new Dictionary<string, ConfigurationSection>();
            sections[LoggingSettings.SectionName] = settings;
            IConfigurationSource configurationSource
                = ConfigurationTestHelper.SaveSectionsInFileAndReturnConfigurationSource(sections);

            LoggingSettings roSettigs = (LoggingSettings)configurationSource.GetSection(LoggingSettings.SectionName);

            Assert.AreEqual(1, roSettigs.TraceListeners.Count);
            Assert.IsNotNull(roSettigs.TraceListeners.Get(name));
            Assert.AreEqual(TraceOptions.Callstack, roSettigs.TraceListeners.Get(name).TraceOutputOptions);
            Assert.AreSame(typeof(FlatFileTraceListenerData), roSettigs.TraceListeners.Get(name).GetType());
            Assert.AreSame(typeof(FlatFileTraceListenerData), roSettigs.TraceListeners.Get(name).ListenerDataType);
            Assert.AreSame(typeof(FlatFileTraceListener), roSettigs.TraceListeners.Get(name).Type);
            Assert.AreEqual(filename, ((FlatFileTraceListenerData)roSettigs.TraceListeners.Get(name)).FileName);
            Assert.AreEqual(footer, ((FlatFileTraceListenerData)roSettigs.TraceListeners.Get(name)).Footer);
            Assert.AreEqual(formatter, ((FlatFileTraceListenerData)roSettigs.TraceListeners.Get(name)).Formatter);
            Assert.AreEqual(header, ((FlatFileTraceListenerData)roSettigs.TraceListeners.Get(name)).Header);
        }

        [TestMethod]
        public void CanDeserializeSerializedConfigurationWithDefaults()
        {
            LoggingSettings rwLoggingSettings = new LoggingSettings();
            rwLoggingSettings.TraceListeners.Add(new FlatFileTraceListenerData("listener1", "log.txt", "---header---", "+++footer+++", "formatter"));
            rwLoggingSettings.TraceListeners.Add(new FlatFileTraceListenerData("listener2", "log2.txt", "***header***", "===footer===", "formatter"));

            IDictionary<string, ConfigurationSection> sections = new Dictionary<string, ConfigurationSection>();
            sections[LoggingSettings.SectionName] = rwLoggingSettings;
            IConfigurationSource configurationSource
                = ConfigurationTestHelper.SaveSectionsInFileAndReturnConfigurationSource(sections);

            LoggingSettings roLoggingSettings = (LoggingSettings)configurationSource.GetSection(LoggingSettings.SectionName);

            Assert.AreEqual(2, roLoggingSettings.TraceListeners.Count);
            Assert.IsNotNull(roLoggingSettings.TraceListeners.Get("listener1"));
            Assert.IsNotNull(roLoggingSettings.TraceListeners.Get("listener2"));
        }

        [TestMethod]
        public void CanCreateInstanceFromGiveName()
        {
            FlatFileTraceListenerData listenerData = new FlatFileTraceListenerData("listener", "log.txt", "---header---", "+++footer+++", "formatter");

            MockLogObjectsHelper helper = new MockLogObjectsHelper();
            helper.loggingSettings.TraceListeners.Add(listenerData);
            helper.loggingSettings.Formatters.Add(new TextFormatterData("formatter", "some template"));

            TraceListener listener = GetListener("listener", helper.configurationSource);

            Assert.IsNotNull(listener);
            Assert.AreEqual("listener", listener.Name);
            Assert.AreEqual(listener.GetType(), typeof(FlatFileTraceListener));
            Assert.IsNotNull(((FlatFileTraceListener)listener).Formatter);
            Assert.AreEqual(((FlatFileTraceListener)listener).Formatter.GetType(), typeof(TextFormatter));
            Assert.AreEqual("some template", ((TextFormatter)((FlatFileTraceListener)listener).Formatter).Template);
        }

        [TestMethod]
        public void CanCreateInstanceFromConfigurationFile()
        {
            LoggingSettings loggingSettings = new LoggingSettings();
            loggingSettings.Formatters.Add(new TextFormatterData("formatter", "some template"));
            loggingSettings.TraceListeners.Add(new FlatFileTraceListenerData("listener", "log.txt", "---header---", "+++footer+++", "formatter"));

            TraceListener listener = GetListener("listener", CommonUtil.SaveSectionsAndGetConfigurationSource(loggingSettings));

            Assert.IsNotNull(listener);
            Assert.AreEqual(listener.GetType(), typeof(FlatFileTraceListener));
            Assert.IsNotNull(((FlatFileTraceListener)listener).Formatter);
            Assert.AreEqual(((FlatFileTraceListener)listener).Formatter.GetType(), typeof(TextFormatter));
            Assert.AreEqual("some template", ((TextFormatter)((FlatFileTraceListener)listener).Formatter).Template);
        }
    }
}
