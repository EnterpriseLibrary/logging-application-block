using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;

namespace Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners
{
    /// <summary>
    /// A <see cref="System.Diagnostics.TraceListener"/> that sends data as Application Insights telemetry, 
    /// formatting the output with an <see cref="ILogFormatter"/>.
    /// </summary>
    [ConfigurationElementType(typeof(ApplicationInsightsTraceListenerData))]
    public class ApplicationInsightsTraceListener : FormattedTraceListenerBase
    {
        private TelemetryClient telemetryClient;

        /// <summary>
        /// Initializes a new <see cref="ApplicationInsightsTraceListener"/>
        /// using a given instrumentation key.
        /// </summary>
        /// <param name="instrumentationKey">The instrumentation key to use for sending Application Insights data</param>
        public ApplicationInsightsTraceListener(string instrumentationKey)
        {
            this.telemetryClient = new TelemetryClient(new TelemetryConfiguration(instrumentationKey));
        }

        /// <summary>
        /// The instrumentation key used to record Application Insights telemetry
        /// </summary>
        public string InstrumentationKey
        {
            get => telemetryClient.InstrumentationKey;
            set => telemetryClient.InstrumentationKey = value;
        }

        /// <summary>
        /// Sends Application Insights telemetry for a string message
        /// </summary>
        /// <param name="message">The message to log</param>
        public override void Write(string message)
        {
            var telemetry = new TraceTelemetry(message, SeverityLevel.Information);
            telemetryClient.TrackTrace(telemetry);
        }

        /// <summary>
        /// Sends Application Insights telemetry for a string message. Identical to <see cref="ApplicationInsightsTraceListener.Write(string)"/>
        /// </summary>
        /// <param name="message">The message to log</param>
        public override void WriteLine(string message) => Write(message);

        /// <summary>
        /// Delivers trace data as Application Insights telemetry
        /// </summary>
        /// <param name="eventCache">The context information provided by <see cref="System.Diagnostics"/></param>
        /// <param name="source">The name of the trace source that delivered the trace data</param>
        /// <param name="eventType">The type of event</param>
        /// <param name="id">The id of the event</param>
        /// <param name="data">The data to trace</param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if (this.Filter == null || this.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
            {
                if (data is LogEntry logEntry)
                {
                    var telemetry = GetTelemetry(logEntry);
                    telemetryClient.TrackTrace(telemetry);
                }
                else if (data is string dataString)
                {
                    Write(dataString);
                }
                else
                {
                    base.TraceData(eventCache, source, eventType, id, data);
                }
            }
        }

        /// <summary>
        /// Creates a <see cref="TraceTelemetry"/> object from a <see cref="LogEntry"/>
        /// </summary>
        private TraceTelemetry GetTelemetry(LogEntry logEntry)
        {
            // Use the LogEntry object's severity and message (with this object's formatter) to
            // create a TraceTelemetry
            var telemetry = new TraceTelemetry(Formatter == null ? logEntry.Message : Formatter.Format(logEntry), 
                GetSeverityLevel(logEntry.Severity));

            if (logEntry.TimeStamp != new DateTime())
            {
                telemetry.Timestamp = logEntry.TimeStamp;
            }

            // Populate TraceTelemetry custom properties with the LogEntry object's properties
            AddProperty(logEntry.ActivityIdString, "ActivityId", telemetry);
            AddProperty(logEntry.AppDomainName, "AppDomainName", telemetry);
            if (logEntry.Categories.Any())
            {
                AddProperty(string.Join(",", logEntry.Categories), "Categories", telemetry);
            }
            AddProperty(logEntry.ErrorMessages, "ErrorMessages", telemetry);
            AddProperty(logEntry.EventId.ToString(), "EventId", telemetry);
            foreach (var kvp in logEntry.ExtendedProperties)
            {
                AddProperty(kvp.Value.ToString(), kvp.Key, telemetry);
            }
            // Include LoggedSeverity even though severity is used elsewhere since this
            // preserves different 'equivalent' severities like Start and Resume
            AddProperty(logEntry.LoggedSeverity, "LoggedSeverity", telemetry);
            AddProperty(logEntry.MachineName, "MachineName", telemetry);
            AddProperty(logEntry.ManagedThreadName, "ManagedThreadName", telemetry);
            // Include message as a custom property even thought it also is tracked
            // as the trace message since the raw (unformatted) message may differ from
            // the formatted one used elsewhere.
            AddProperty(logEntry.Message, "Message", telemetry);
            AddProperty(logEntry.Priority.ToString(), "Priority", telemetry);
            AddProperty(logEntry.ProcessId, "Processid", telemetry);
            AddProperty(logEntry.ProcessName, "ProcessName", telemetry);
            AddProperty(logEntry.RelatedActivityId?.ToString(), "RelatedActivityId", telemetry);
            AddProperty(logEntry.Title, "Title", telemetry);
            AddProperty(logEntry.Win32ThreadId, "Win32ThreadId", telemetry);

            return telemetry;
        }

        private void AddProperty(string value, string key, TraceTelemetry telemetry)
        {
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
            {
                telemetry.Properties[key] = value;
            }
        }

        private SeverityLevel GetSeverityLevel(TraceEventType severity)
        {
            switch (severity)
            {
                case TraceEventType.Verbose:
                    return SeverityLevel.Verbose;
                case TraceEventType.Information:
                    return SeverityLevel.Information;
                case TraceEventType.Warning:
                    return SeverityLevel.Warning;
                case TraceEventType.Error:
                    return SeverityLevel.Error;
                case TraceEventType.Critical:
                    return SeverityLevel.Critical;
                case TraceEventType.Start:
                case TraceEventType.Stop:
                case TraceEventType.Suspend:
                case TraceEventType.Resume:
                case TraceEventType.Transfer:
                    return SeverityLevel.Verbose;
                default:
                    return SeverityLevel.Verbose;
            }
        }

        /// <summary>
        /// Declares the supported attributes for <see cref="ApplicationInsightsTraceListener"/>.
        /// </summary>
        protected override string[] GetSupportedAttributes()
        {
            return new string[] { "formatter", "instrumentationKey" };
        }

        /// <summary>
        /// Flush telemetry to Application Insights
        /// </summary>
        public override void Flush()
        {
            telemetryClient?.Flush();
            base.Flush();
        }

        /// <summary>
        /// Optionally releases managed resources used by the <see cref="ApplicationInsightsTraceListener"/>
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    telemetryClient?.Flush();
                }
                finally
                {
                    telemetryClient = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}
