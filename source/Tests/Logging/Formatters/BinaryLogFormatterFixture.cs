﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Configuration;
using EnterpriseLibrary.Common.Configuration;
using EnterpriseLibrary.Common.TestSupport.Configuration;
using EnterpriseLibrary.Logging.Configuration;
using EnterpriseLibrary.Logging.Formatters;
using EnterpriseLibrary.Logging.TestSupport;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseLibrary.Logging.Tests.Formatters
{
    [TestClass]
    public class BinaryLogFormatterFixture
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
        public void CanDeserializeSerializedConfiguration()
        {
            LoggingSettings rwLoggingSettings = new LoggingSettings();
            rwLoggingSettings.Formatters.Add(new BinaryLogFormatterData("formatter1"));
            rwLoggingSettings.Formatters.Add(new BinaryLogFormatterData("formatter2"));

            IDictionary<string, ConfigurationSection> sections = new Dictionary<string, ConfigurationSection>();
            sections[LoggingSettings.SectionName] = rwLoggingSettings;
            IConfigurationSource configurationSource
                = ConfigurationTestHelper.SaveSectionsInFileAndReturnConfigurationSource(sections);

            LoggingSettings roLoggingSettings = (LoggingSettings)configurationSource.GetSection(LoggingSettings.SectionName);

            Assert.AreEqual(2, roLoggingSettings.Formatters.Count);
            Assert.IsNotNull(roLoggingSettings.Formatters.Get("formatter1"));
            Assert.AreSame(typeof(BinaryLogFormatterData), roLoggingSettings.Formatters.Get("formatter1").GetType());
            Assert.AreSame(typeof(BinaryLogFormatter), roLoggingSettings.Formatters.Get("formatter1").Type);
            Assert.IsNotNull(roLoggingSettings.Formatters.Get("formatter2"));
            Assert.AreSame(typeof(BinaryLogFormatterData), roLoggingSettings.Formatters.Get("formatter2").GetType());
            Assert.AreSame(typeof(BinaryLogFormatter), roLoggingSettings.Formatters.Get("formatter2").Type);
        }

        [TestMethod]
        public void CanCreateFormatterFromContainerFromGivenName()
        {
            FormatterData data = new BinaryLogFormatterData("ignore");
            LoggingSettings settings = new LoggingSettings();
            settings.Formatters.Add(data);
            DictionaryConfigurationSource configurationSource = new DictionaryConfigurationSource();
            configurationSource.Add(LoggingSettings.SectionName, settings);

            ILogFormatter formatter = GetFormatter("ignore", configurationSource);

            Assert.IsNotNull(formatter);
            Assert.AreEqual(formatter.GetType(), typeof(BinaryLogFormatter));
        }

        [TestMethod]
        public void CanDeserializeFormattedEntry()
        {
            LogEntry entry = CommonUtil.GetDefaultLogEntry();
            entry.Message = "message";
            entry.Title = "title";
            entry.Categories = new List<string>(new string[] { "cat1", "cat2", "cat3" });

            string serializedLogEntryText = new BinaryLogFormatter().Format(entry);
            LogEntry deserializedEntry = BinaryLogFormatter.Deserialize(serializedLogEntryText);

            Assert.IsNotNull(deserializedEntry);
            Assert.IsFalse(ReferenceEquals(entry, deserializedEntry));
            Assert.AreEqual(entry.Categories.Count, deserializedEntry.Categories.Count);
            foreach (string category in entry.Categories)
            {
                Assert.IsTrue(deserializedEntry.Categories.Contains(category));
            }
            Assert.AreEqual(entry.Message, deserializedEntry.Message);
            Assert.AreEqual(entry.Title, deserializedEntry.Title);
        }

        [TestMethod]
        public void CanDeserializeFormattedCustomEntry()
        {
            CustomLogEntry entry = new CustomLogEntry();
            entry.TimeStamp = DateTime.MaxValue;
            entry.Title = "My custom message title";
            entry.Message = "My custom message body";
            entry.Categories = new List<string>(new string[] { "CustomFormattedCategory", "OtherCategory" });
            entry.AcmeCoField1 = "apple";
            entry.AcmeCoField2 = "orange";
            entry.AcmeCoField3 = "lemon";

            string serializedLogEntryText = new BinaryLogFormatter().Format(entry);
            CustomLogEntry deserializedEntry =
                (CustomLogEntry)BinaryLogFormatter.Deserialize(serializedLogEntryText);

            Assert.IsNotNull(deserializedEntry);
            Assert.IsFalse(ReferenceEquals(entry, deserializedEntry));
            Assert.AreEqual(entry.Categories.Count, deserializedEntry.Categories.Count);
            foreach (string category in entry.Categories)
            {
                Assert.IsTrue(deserializedEntry.Categories.Contains(category));
            }
            Assert.AreEqual(entry.Message, deserializedEntry.Message);
            Assert.AreEqual(entry.Title, deserializedEntry.Title);
            Assert.AreEqual(entry.AcmeCoField1, deserializedEntry.AcmeCoField1);
            Assert.AreEqual(entry.AcmeCoField2, deserializedEntry.AcmeCoField2);
            Assert.AreEqual(entry.AcmeCoField3, deserializedEntry.AcmeCoField3);
        }
    }
}
