﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Diagnostics;
using System.IO;
using System.Xml.XPath;
using EnterpriseLibrary.Logging.TestSupport;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseLibrary.Logging.TraceListeners.Tests
{
    /// <summary>
    /// Summary description for LogEntryXmlFixture
    /// </summary>
    [TestClass]
    public class XmlTraceListenerFixture
    {
        TraceSource source;
        string tempFileName;
        XmlTraceListener xmlTraceListener;

        [TestInitialize]
        public void SetUp()
        {
            source = new TraceSource("unnamed", SourceLevels.All);
            tempFileName = Path.GetTempFileName();
            xmlTraceListener = new XmlTraceListener(tempFileName);
            source.Listeners.Add(xmlTraceListener);
        }

        [TestCleanup]
        public void Teardown()
        {
            xmlTraceListener.Dispose();
        }

        [TestMethod]
        public void TraceXmlLogEntryWithPayload()
        {
            XmlLogEntry xmlLogEntry = CommonUtil.CreateXmlLogEntry();
            source.TraceData(TraceEventType.Error, 1, xmlLogEntry);
            AssertTempFileNameHasSomeContent();
        }

        [TestMethod]
        public void TraceXmlLogEntryWithoutPayload()
        {
            XmlLogEntry xmlLogEntry = CommonUtil.CreateXmlLogEntry();
            xmlLogEntry.Xml = null;
            source.TraceData(TraceEventType.Error, 1, xmlLogEntry);
            AssertTempFileNameHasSomeContent();
        }

        [TestMethod]
        public void TraceLogEntry()
        {
            LogEntry xmlLogEntry = CommonUtil.CreateLogEntry();
            source.TraceData(TraceEventType.Error, 1, xmlLogEntry);
            AssertTempFileNameHasSomeContent();
        }

        [TestMethod]
        public void TraceXPathNavogator()
        {
            XPathNavigator navigator = new XPathDocument(new StringReader(CommonUtil.Xml)).CreateNavigator();
            source.TraceData(TraceEventType.Error, 1, navigator);
            AssertTempFileNameHasSomeContent();
        }

        [TestMethod]
        public void TraceXmlString()
        {
            source.TraceData(TraceEventType.Error, 1, CommonUtil.Xml);
            AssertTempFileNameHasSomeContent();
        }

        [TestMethod]
        public void TraceString()
        {
            source.TraceData(TraceEventType.Error, 1, @"& ""<>=\r\n                                                '");
            AssertTempFileNameHasSomeContent();
        }

        void AssertTempFileNameHasSomeContent()
        {
            xmlTraceListener.Close();
            Assert.IsTrue(new FileInfo(tempFileName).Length > 0);
        }
    }
}
