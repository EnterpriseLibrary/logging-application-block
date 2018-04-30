﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Management.Instrumentation;
using System.Xml;
using EnterpriseLibrary.Logging.Tests;
using EnterpriseLibrary.Logging.TestSupport;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseLibrary.Logging.Formatters.Tests
{
    /// <summary>
    /// Summary description for LogEntryXmlFixture
    /// </summary>
    [TestClass]
    public class XmlLogFormatterFixture
    {
        LogFormatter xmlLogFormatter;

        [TestInitialize]
        public void SetUp()
        {
            xmlLogFormatter = new XmlLogFormatter();
        }

        [TestMethod]
        public void FormatLogEntry()
        {
            LogEntry logEntry = CommonUtil.CreateLogEntry();
            string xml = xmlLogFormatter.Format(logEntry);
            Assert.IsFalse(string.IsNullOrEmpty(xml));
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            Assert.IsNotNull(xmlDocument.FirstChild);
        }

        [TestMethod]
        public void FormatXmlLogEntry()
        {
            XmlLogEntry logEntry = CommonUtil.CreateXmlLogEntry();
            string xml = xmlLogFormatter.Format(logEntry);
            Assert.IsFalse(string.IsNullOrEmpty(xml));
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            Assert.IsNotNull(xmlDocument.FirstChild);
        }

        [TestMethod]
        public void CanFormatLogEntryWithExtendedProperties()
        {
            LogEntry logEntry = new LogEntry();
            logEntry.Message = "Hello";
            logEntry.Categories.Add("General");
            logEntry.ExtendedProperties.Add("test", "test");
            string xml = xmlLogFormatter.Format(logEntry);
            XmlDocument dom = new XmlDocument();
            dom.LoadXml(xml);
        }

        [TestMethod]
        public void CanFormatLogEntryWithIndexerProperties()
        {
            LogEntry logEntry = new LogEntryWithIndexer();
            string xml = xmlLogFormatter.Format(logEntry);
            Assert.IsFalse(string.IsNullOrEmpty(xml));
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            Assert.IsNotNull(xmlDocument.FirstChild);
        }

        [TestMethod]
        public void CanFormatLogEntryWithWriteOnlyProperties()
        {
            LogEntry logEntry = new LogEntryWithWriteOnlyProperty();
            string xml = xmlLogFormatter.Format(logEntry);
            Assert.IsFalse(string.IsNullOrEmpty(xml));
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            Assert.IsNotNull(xmlDocument.FirstChild);
        }

        [TestMethod]
        public void CanFormatLogEntryWithTextThatNeedsEscaping()
        {
            LogEntry logEntry = CommonUtil.CreateLogEntry();
            logEntry.Message = "some <text> that needs escaping &";
            string xml = xmlLogFormatter.Format(logEntry);
            Assert.IsFalse(string.IsNullOrEmpty(xml));
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            Assert.IsNotNull(xmlDocument.FirstChild);
            Assert.AreEqual("Message", xmlDocument.FirstChild.ChildNodes[0].Name);
            Assert.AreEqual(logEntry.Message, xmlDocument.FirstChild.ChildNodes[0].InnerText);
        }
    }

    // Both must be public classes because they will be exported to WMI 
    // (because they inherit from LogEntry, an InstrumentationClass
    public class LogEntryWithIndexer : LogEntry
    {
        [IgnoreMember]
        public string this[int index]
        {
            get { return null; }
        }
    }

    public class LogEntryWithWriteOnlyProperty : LogEntry
    {
        [IgnoreMember]
        public string WriteOnly
        {
            set { ; }
        }
    }
}
