using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Microsoft.Practices.EnterpriseLibrary.Logging.Tests.TraceListeners
{
    [TestClass]
    public class ApplicationInsightsTraceListenerFixture
    {
        MockApplicationInsightsTelemetryChannel TelemetryChannel;
        
        [TestInitialize]
        public void SetUp()
        {
            // Intercept AI telemetry client
            TelemetryChannel = new MockApplicationInsightsTelemetryChannel();
            TelemetryConfiguration.Active.TelemetryChannel = TelemetryChannel;
        }

        [TestMethod]
        public void TelemetryIsSentToApplicationInsights()
        {
            var key = "TestKey";
            var message = "Test message";
            var category = "Test category";
            var priority = 5;
            var eventId = 11;
            var eventType = TraceEventType.Warning;
            var title = "Test title";
            var testValue = "Test value";
            var properties = new Dictionary<string, object>() { { "Test property", testValue } };

            TelemetryChannel.ResetTelemetry();
            using (var listener = new ApplicationInsightsTraceListener(key))
            {
                var source = new LogSource("TestSource", new[] { listener }, SourceLevels.All);

                source.TraceData(TraceEventType.Information, 1, new LogEntry(message, category, priority, eventId, eventType, title, properties));
            }

            Assert.AreEqual(TelemetryChannel.Traces.Count, 1);
            var trace = TelemetryChannel.Traces.Single() as TraceTelemetry;

            Assert.IsNotNull(trace);
            Assert.AreEqual(message, trace.Message);
            Assert.AreEqual(SeverityLevel.Warning, trace.SeverityLevel);
            Assert.AreEqual(title, trace.Properties["Title"]);
            Assert.AreEqual(category, trace.Properties["Categories"]);
            Assert.AreEqual(priority.ToString(), trace.Properties["Priority"]);
            Assert.AreEqual(Environment.MachineName, trace.Properties["MachineName"]);
            Assert.AreEqual(eventId.ToString(), trace.Properties["EventId"]);
            Assert.AreEqual(testValue, trace.Properties["Test property"]);
            Assert.AreEqual(key, trace.Context.InstrumentationKey);
        }

        [TestMethod]
        public void StringMessagesAreWrittenAsEvents()
        {
            TelemetryChannel.ResetTelemetry();
            using (var listener = new ApplicationInsightsTraceListener(string.Empty))
            {
                var source = new LogSource("TestSource", new[] { listener }, SourceLevels.All);

                listener.TraceData(new TraceEventCache(), "Test source", TraceEventType.Error, 10, "Test message");
            }

            Assert.AreEqual(TelemetryChannel.Traces.Count, 1);
            var trace = TelemetryChannel.Traces.Single() as TraceTelemetry;

            Assert.IsNotNull(trace);
            Assert.AreEqual("Test message", trace.Message);
            Assert.AreEqual(0, trace.Properties.Count);
            Assert.AreEqual(SeverityLevel.Error, trace.SeverityLevel);
        }

        [TestMethod]
        public void WriteLineCallsWrite()
        {
            TelemetryChannel.ResetTelemetry();
            using (var listener = new ApplicationInsightsTraceListener(string.Empty))
            {
                var source = new LogSource("TestSource", new[] { listener }, SourceLevels.All);

                listener.WriteLine("Test message");
            }

            Assert.AreEqual(TelemetryChannel.Traces.Count, 1);
            var trace = TelemetryChannel.Traces.Single() as TraceTelemetry;

            Assert.IsNotNull(trace);
            Assert.AreEqual("Test message", trace.Message);
            Assert.AreEqual(0, trace.Properties.Count);
            Assert.AreEqual(SeverityLevel.Information, trace.SeverityLevel);
        }


        [DataTestMethod]
        [DataRow(TraceEventType.Start)]
        [DataRow(TraceEventType.Stop)]
        [DataRow(TraceEventType.Suspend)]
        [DataRow(TraceEventType.Resume)]
        [DataRow(TraceEventType.Transfer)]
        [DataRow(TraceEventType.Verbose)]
        [DataRow((TraceEventType)(-1))]
        public void ActivityEventTypesAreTreatedAsVerbose(TraceEventType eventType)
        {
            TelemetryChannel.ResetTelemetry();
            using (var listener = new ApplicationInsightsTraceListener(string.Empty))
            {
                var source = new LogSource("TestSource", new[] { listener }, SourceLevels.All);

                source.TraceData(TraceEventType.Information, 1, new LogEntry(string.Empty, "TestCategory", 1, 1, eventType, string.Empty, null));
            }

            Assert.AreEqual(TelemetryChannel.Traces.Count, 1);
            var trace = TelemetryChannel.Traces.Single() as TraceTelemetry;

            Assert.IsNotNull(trace);
            Assert.AreEqual(SeverityLevel.Verbose, trace.SeverityLevel);
            Assert.AreEqual(eventType.ToString(), trace.Properties["LoggedSeverity"]);
        }

        [TestMethod]
        public void NonEventNonStringObjectsLoggedAsStrings()
        {
            TelemetryChannel.ResetTelemetry();
            using (var listener = new ApplicationInsightsTraceListener(string.Empty))
            {
                var source = new LogSource("TestSource", new[] { listener }, SourceLevels.All);

                listener.TraceData(new TraceEventCache(), "Test source", TraceEventType.Warning, 10, 1);
            }

            Assert.AreEqual(TelemetryChannel.Traces.Count, 1);
            var trace = TelemetryChannel.Traces.Single() as TraceTelemetry;

            Assert.IsNotNull(trace);
            Assert.AreEqual("1", trace.Message);
            Assert.AreEqual(0, trace.Properties.Count);
            Assert.AreEqual(SeverityLevel.Warning, trace.SeverityLevel);

        }

        [TestMethod]
        public void FlushesOnDispose()
        {
            TelemetryChannel.ResetTelemetry();
            var listener = new ApplicationInsightsTraceListener(string.Empty);
            listener.Dispose();

            Assert.AreEqual(1, TelemetryChannel.FlushCount);
        }

        [TestMethod]
        public void InstrumentationKeyIsSetProperly()
        {
            var listener = new ApplicationInsightsTraceListener("Key1");
            Assert.AreEqual("Key1", listener.InstrumentationKey);

            listener.InstrumentationKey = "Key2";
            Assert.AreEqual("Key2", listener.InstrumentationKey);
        }
    }
}
