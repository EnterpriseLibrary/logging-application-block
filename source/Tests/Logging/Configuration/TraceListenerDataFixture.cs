﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Threading;
using EnterpriseLibrary.Logging.TestSupport.TraceListeners;
using EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseLibrary.Logging.Configuration.Tests
{
    [TestClass]
    public class TraceListenerDataFixture
    {
        [TestInitialize]
        public void TestInitialize()
        {
            AppDomain.CurrentDomain.SetData("APPBASE", Environment.CurrentDirectory);
        }

        [TestMethod]
        public void HasDefaultValues()
        {
            TraceListenerData data = new TraceListenerData();

            Assert.AreEqual(TraceOptions.None, data.TraceOutputOptions);
            Assert.AreEqual(SourceLevels.All, data.Filter);
            Assert.AreEqual(false, data.Asynchronous);
            Assert.AreEqual(30000, data.AsynchronousBufferSize);
            Assert.AreEqual(Timeout.InfiniteTimeSpan, data.AsynchronousDisposeTimeout);
            Assert.AreEqual(null, data.AsynchronousMaxDegreeOfParallelism);
        }

        [TestMethod]
        public void CreatesSynchronousTraceListenerByDefault()
        {
            var data = new MockTraceListenerData() { Filter = SourceLevels.Warning, TraceOutputOptions = TraceOptions.ProcessId };

            var listener = data.BuildTraceListener(new LoggingSettings());

            Assert.IsInstanceOfType(listener, typeof(MockTraceListener));
            Assert.AreEqual(SourceLevels.Warning, ((EventTypeFilter)listener.Filter).EventType);
        }

        [TestMethod]
        public void CreatesAynchronousTraceListenerWhenOverridden()
        {
            var data = new MockTraceListenerData() { Asynchronous = true, Filter = SourceLevels.Warning, TraceOutputOptions = TraceOptions.ProcessId };

            var listener = data.BuildTraceListener(new LoggingSettings());

            Assert.IsInstanceOfType(listener, typeof(AsynchronousTraceListenerWrapper));
            Assert.AreEqual(SourceLevels.Warning, ((EventTypeFilter)listener.Filter).EventType);
        }

        [TestMethod]
        public void CreatesAynchronousTraceListenerWithTimeoutWhenOverridden()
        {
            var data = new MockTraceListenerData() { Asynchronous = true, AsynchronousDisposeTimeout = TimeSpan.FromSeconds(10), Filter = SourceLevels.Warning, TraceOutputOptions = TraceOptions.ProcessId };

            var listener = data.BuildTraceListener(new LoggingSettings());

            Assert.IsInstanceOfType(listener, typeof(AsynchronousTraceListenerWrapper));
            Assert.AreEqual(SourceLevels.Warning, ((EventTypeFilter)listener.Filter).EventType);
        }
    }
}
