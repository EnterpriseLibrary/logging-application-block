﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Diagnostics;
using EnterpriseLibrary.Logging.Configuration;
using EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseLibrary.Logging.Tests.Configuration
{
    [TestClass]
    public class GivenXmlTraceListenerDataWithFilterData
    {
        private TraceListenerData listenerData;

        [TestInitialize]
        public void Setup()
        {
            listenerData =
                new XmlTraceListenerData("listener", "file name")
                {
                    TraceOutputOptions = TraceOptions.DateTime | TraceOptions.Callstack,
                    Filter = SourceLevels.Warning
                };
        }

        [TestMethod]
        public void WhenCreatingInstanceUsingDefaultContructor_ThenListenerDataTypeIsSet()
        {
            var listener = new XmlTraceListenerData();
            Assert.AreEqual(typeof(XmlTraceListenerData), listener.ListenerDataType);
        }

        [TestMethod]
        public void WhenCreatingListener_ThenCreatesXmlListener()
        {
            var settings = new LoggingSettings();

            var listener = (XmlTraceListener)listenerData.BuildTraceListener(settings);

            Assert.IsNotNull(listener);
            Assert.AreEqual("listener", listener.Name);
            Assert.AreEqual(TraceOptions.DateTime | TraceOptions.Callstack, listener.TraceOutputOptions);
            Assert.IsNotNull(listener.Filter);
            Assert.AreEqual(SourceLevels.Warning, ((EventTypeFilter)listener.Filter).EventType);
        }
    }
}
