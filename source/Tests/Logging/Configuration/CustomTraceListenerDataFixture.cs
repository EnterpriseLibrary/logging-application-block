﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using EnterpriseLibrary.Logging.Configuration;
using EnterpriseLibrary.Logging.Formatters;
using EnterpriseLibrary.Logging.TraceListeners;
using EnterpriseLibrary.Logging.TraceListeners.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseLibrary.Logging.Tests.Configuration
{
    [TestClass]
    public class GivenCustomTraceListenerDataWithInitializationDataForNonCustomTraceListenerType
    {
        private CustomTraceListenerData listenerData;

        [TestInitialize]
        public void Given()
        {
            listenerData =
                new CustomTraceListenerData(
                     "custom trace listener",
                     typeof(MockSystemDiagsTraceListener),
                     "someInitData")
                {
                    Formatter = "formatter"
                };
        }

        [TestMethod]
        public void WhenCreatingInstanceUsingDefaultContructor_ThenListenerDataTypeIsSet()
        {
            var listener = new CustomTraceListenerData();
            Assert.AreEqual(typeof(CustomTraceListenerData), listener.ListenerDataType);
        }

        [TestMethod]
        public void WhenCreatingListener_ThenCreatesListenerWithSingleArgConstructor()
        {
            var settings = new LoggingSettings { };

            var listener = (MockSystemDiagsTraceListener)listenerData.BuildTraceListener(settings);

            Assert.IsNotNull(listener);
            Assert.AreEqual("custom trace listener", listener.Name);
            Assert.AreSame("someInitData", listener.Value);
            Assert.AreEqual(TraceOptions.None, listener.TraceOutputOptions);
            Assert.IsNull(listener.Filter);
            Assert.AreEqual(0, listener.Attributes.Count);
        }
    }

    [TestClass]
    public class GivenCustomTraceListenerDataWithInitializationDataForCustomTraceListenerType
    {
        private CustomTraceListenerData listenerData;

        [TestInitialize]
        public void Given()
        {
            listenerData =
                new CustomTraceListenerData(
                     "custom trace listener",
                     typeof(MockCustomTraceListener),
                     "someInitData")
                {
                    Formatter = "formatter"
                };
        }

        [TestMethod]
        public void WhenCreatingListener_ThenCreatesListenerWithSingleArgConstructor()
        {
            var settings = new LoggingSettings { Formatters = { new TextFormatterData { Name = "formatter", Template = "template" } } };

            var listener = (MockCustomTraceListener)listenerData.BuildTraceListener(settings);

            Assert.IsNotNull(listener);
            Assert.AreEqual("custom trace listener", listener.Name);
            Assert.AreSame("someInitData", listener.initData);
            Assert.AreEqual(TraceOptions.None, listener.TraceOutputOptions);
            Assert.IsNull(listener.Filter);
            Assert.IsNotNull(listener.Formatter);
            Assert.AreEqual("template", ((TextFormatter)listener.Formatter).Template);
            Assert.AreEqual(0, listener.Attributes.Count);
        }
    }

    [TestClass]
    public class GivenCustomTraceListenerDataWithInitializationDataWithEmptyFormatterNameForCustomTraceListenerType
    {
        private CustomTraceListenerData listenerData;

        [TestInitialize]
        public void Given()
        {
            listenerData =
                new CustomTraceListenerData(
                     "custom trace listener",
                     typeof(MockCustomTraceListener),
                     "someInitData")
                {
                    Formatter = string.Empty
                };
        }

        [TestMethod]
        public void WhenCreatingListener_ThenCreatesListenerWithSingleArgConstructor()
        {
            var settings = new LoggingSettings { Formatters = { new TextFormatterData { Name = "formatter", Template = "template" } } };

            var listener = (MockCustomTraceListener)listenerData.BuildTraceListener(settings);

            Assert.IsNotNull(listener);
            Assert.AreEqual("custom trace listener", listener.Name);
            Assert.AreSame("someInitData", listener.initData);
            Assert.AreEqual(TraceOptions.None, listener.TraceOutputOptions);
            Assert.IsNull(listener.Filter);
            Assert.IsNull(listener.Formatter);
            Assert.AreEqual(0, listener.Attributes.Count);
        }
    }

    [TestClass]
    public class GivenCustomTraceListenerWithoutExpectedConstructor
    {
        private CustomTraceListenerData listenerData;

        [TestInitialize]
        public void Given()
        {
            listenerData = new CustomTraceListenerData("someName",
                                                       typeof(MockCustomTraceListenerWithoutExpectedConstructor),
                                                       "initData");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenCreatingListener_ThenThrows()
        {
            var settings = new LoggingSettings { Formatters = { new TextFormatterData { Name = "formatter", Template = "template" } } };

            this.listenerData.BuildTraceListener(settings);
        }
    }

    internal class MockCustomTraceListenerWithoutExpectedConstructor : CustomTraceListener
    {
        public MockCustomTraceListenerWithoutExpectedConstructor()
        {
        }

        public override void Write(string message)
        {
            throw new System.NotImplementedException();
        }

        public override void WriteLine(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}

