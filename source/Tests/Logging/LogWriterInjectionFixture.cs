﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics;
using EnterpriseLibrary.Logging.Filters;
using EnterpriseLibrary.Logging.TestSupport.TraceListeners;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseLibrary.Logging.Tests
{

    [TestClass]
    public class GivenLogWriterInjectedWithLoggingStack
    {
        private MockTraceListener traceListener;
        private LogWriter logWriter;

        [TestInitialize]
        public void Setup()
        {
            this.traceListener = new MockTraceListener("original");
            this.logWriter =
                new LogWriter(
                    new LogWriterStructureHolder(
                        new ILogFilter[0],
                        new Dictionary<string, LogSource>(),
                        new LogSource("all", new[] { traceListener }, SourceLevels.All),
                        new LogSource("not processed"),
                        new LogSource("error"),
                        "default",
                        false,
                        false,
                        false));
        }

        [TestMethod]
        public void WhenLogging_ThenLogWriterWritesToTheInjectedStack()
        {
            var logEntry = new LogEntry() { Message = "message" };
            this.logWriter.Write(logEntry);

            Assert.AreSame(logEntry, this.traceListener.tracedData);
        }

        [TestMethod]
        public void WhenLogWriterIsDisposed_ThenTraceListenerIsDisposed()
        {
            this.logWriter.Dispose();

            Assert.IsTrue(this.traceListener.wasDisposed);
        }
    }
}
