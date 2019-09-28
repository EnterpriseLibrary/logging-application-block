using System.Collections.Generic;
using Microsoft.ApplicationInsights.Channel;

namespace Microsoft.Practices.EnterpriseLibrary.Logging.Tests.TraceListeners
{
    /// <summary>
    /// Test class for mocking Application Insights communication
    /// Stores telemetry that would have been sent and tracks flush count
    /// </summary>
    internal class MockApplicationInsightsTelemetryChannel : ITelemetryChannel
    {
        public List<ITelemetry> Traces { get; private set; }
        public int FlushCount { get; private set; }

        public MockApplicationInsightsTelemetryChannel()
        {
            ResetTelemetry();
        }

        public void ResetTelemetry()
        {
            Traces = new List<ITelemetry>();
            FlushCount = 0;
        }

        public bool? DeveloperMode { get; set; }
        public string EndpointAddress { get; set; }

        public void Dispose() { }

        public void Flush() =>  FlushCount++;

        public void Send(ITelemetry item) => Traces.Add(item);
    }
}