using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Practices.EnterpriseLibrary.Logging.Tests.Configuration
{
    [TestClass]
    public class ApplicationInsightsTraceListenerDataFixture
    {
        const string Name = "name";
        const string InstrumentationKey = "aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee";
        const string FormatterName = "formatterName";
        const TraceOptions TraceOutputOptions = TraceOptions.LogicalOperationStack | TraceOptions.ProcessId;
        const SourceLevels Filter = SourceLevels.Warning;

        [TestMethod]
        public void PropertiesAreSetProperly()
        {
            // Create a couple listener data objects
            var defaultListenerData = new ApplicationInsightsTraceListenerData();
            var listenerData = new ApplicationInsightsTraceListenerData(Name, InstrumentationKey, FormatterName, TraceOutputOptions, Filter);

            // Confirm properties are set, as expected, including auto-set properties like Type and ListenerDataType
            Assert.AreEqual(typeof(ApplicationInsightsTraceListenerData), defaultListenerData.ListenerDataType);
            Assert.AreEqual(typeof(ApplicationInsightsTraceListenerData), listenerData.ListenerDataType);
            Assert.AreEqual(typeof(ApplicationInsightsTraceListener), defaultListenerData.Type);
            Assert.AreEqual(typeof(ApplicationInsightsTraceListener), listenerData.Type);
            Assert.AreEqual(null, defaultListenerData.Name);
            Assert.AreEqual(Name, listenerData.Name);
            Assert.AreEqual(string.Empty, defaultListenerData.InstrumentationKey);
            Assert.AreEqual(InstrumentationKey, listenerData.InstrumentationKey);
            Assert.AreEqual(string.Empty, defaultListenerData.Formatter);
            Assert.AreEqual(FormatterName, listenerData.Formatter);
            Assert.AreEqual(TraceOptions.None, defaultListenerData.TraceOutputOptions);
            Assert.AreEqual(TraceOutputOptions, listenerData.TraceOutputOptions);
            Assert.AreEqual(SourceLevels.All, defaultListenerData.Filter);
            Assert.AreEqual(Filter, listenerData.Filter);
        }

        [TestMethod]
        public void CreatesListenerWithCorrectProperties()
        {
            var listenerData = new ApplicationInsightsTraceListenerData(Name, InstrumentationKey, FormatterName, TraceOutputOptions, Filter);
            var settings = new LoggingSettings { Formatters = { new TextFormatterData { Name = "formatterName", Template = "template" } } };

            using (var listener = listenerData.BuildTraceListener(settings) as ApplicationInsightsTraceListener)
            {

                Assert.IsNotNull(listener);
                Assert.AreEqual(Name, listener.Name);
                Assert.AreEqual(((TextFormatterData)settings.Formatters.First()).Template, ((TextFormatter)listener.Formatter).Template);
                Assert.AreEqual(Filter, ((EventTypeFilter)listener.Filter).EventType);
                Assert.AreEqual(TraceOutputOptions, listener.TraceOutputOptions);
                Assert.AreEqual(0, listener.Attributes.Count);
                Assert.AreEqual(InstrumentationKey, listener.InstrumentationKey);
            }
        }
    }
}
