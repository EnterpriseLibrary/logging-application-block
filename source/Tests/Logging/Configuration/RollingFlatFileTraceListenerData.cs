﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Diagnostics;
using System.IO;
using EnterpriseLibrary.Logging.Configuration;
using EnterpriseLibrary.Logging.Formatters;
using EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseLibrary.Logging.Tests.Configuration
{
    [TestClass]
    public class GivenRollingFlatFileTraceListenerDataWithFilterData
    {
        private TraceListenerData listenerData;

        [TestInitialize]
        public void Setup()
        {
            listenerData =
                new RollingFlatFileTraceListenerData(
                    "listener",
                    "file name",
                    "header",
                    "footer",
                    100,
                    "timestamp pattern",
                    RollFileExistsBehavior.Increment,
                    RollInterval.Day,
                    TraceOptions.DateTime | TraceOptions.Callstack,
                    "formatter")
                {
                    Filter = SourceLevels.Warning,
                    MaxArchivedFiles = 100
                };
        }

        [TestMethod]
        public void WhenCreatingInstanceUsingDefaultContructor_ThenListenerDataTypeIsSet()
        {
            var listener = new RollingFlatFileTraceListenerData();
            Assert.AreEqual(typeof(RollingFlatFileTraceListenerData), listener.ListenerDataType);
        }

        [TestMethod]
        public void WhenCreatingListener_ThenCreatesRollingFileListener()
        {
            var settings = new LoggingSettings { Formatters = { new TextFormatterData { Name = "formatter", Template = "template" } } };

            var listener = (RollingFlatFileTraceListener)listenerData.BuildTraceListener(settings);

            try
            {
                Assert.IsNotNull(listener);
                Assert.AreEqual("listener", listener.Name);
                Assert.AreEqual(TraceOptions.DateTime | TraceOptions.Callstack, listener.TraceOutputOptions);
                Assert.IsNotNull(listener.Filter);
                Assert.AreEqual(SourceLevels.Warning, ((EventTypeFilter)listener.Filter).EventType);
                Assert.IsInstanceOfType(listener.Formatter, typeof(TextFormatter));
                Assert.AreEqual("file name", Path.GetFileName(((FileStream)((StreamWriter)listener.Writer).BaseStream).Name));
            }
            finally
            {
                listener.Dispose();
            }
        }
    }
}
