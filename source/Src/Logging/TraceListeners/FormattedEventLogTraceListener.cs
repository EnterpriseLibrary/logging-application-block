// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;

namespace Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners
{
    /// <summary>
    /// FormattedEventLogTraceListener is a <see cref="TraceListener"/> that wraps an <see cref="FormattedEventLogTraceListener"/> 
    /// and uses a <see cref="ILogFormatter"/> to transform the data to trace.
    /// </summary>
    [ConfigurationElementType(typeof(FormattedEventLogTraceListenerData))]
    public class FormattedEventLogTraceListener : FormattedTraceListenerWrapperBase
    {
        /// <summary>
        /// Default to use when no log name is provided.
        /// </summary>
        public const string DefaultLogName = "";

        /// <summary>
        /// Default to use when no machine name is provided.
        /// </summary>
        public const string DefaultMachineName = ".";

        /// <summary>
        /// Initializes a new instance of <see cref="FormattedEventLogTraceListener"/> with a 
        /// <see cref="EventLogTraceListener"/> initialized with <see cref="EventLog"/>.
        /// </summary>
        /// <param name="eventLog">The event log for the wrapped listener.</param>
#if WINDOWS
        public FormattedEventLogTraceListener(EventLog eventLog)
            : base(new EventLogTraceListener(eventLog))
        {
        }
#else
        public FormattedEventLogTraceListener(EventLog eventLog)
            : base(new ConsoleTraceListener())
        {
        }
#endif


        /// <summary>
        /// Initializes a new instance of <see cref="FormattedEventLogTraceListener"/> with an 
        /// <see cref="ILogFormatter"/> and a <see cref="EventLogTraceListener"/> 
        /// initialized with <see cref="EventLog"/>.
        /// </summary>
        /// <param name="eventLog">The event log for the wrapped listener.</param>
        /// <param name="formatter">The formatter for the wrapper.</param>
#if WINDOWS
        public FormattedEventLogTraceListener(EventLog eventLog, ILogFormatter formatter)
            : base(new EventLogTraceListener(eventLog), formatter)
        {
        }
#else
        public FormattedEventLogTraceListener(EventLog eventLog, ILogFormatter formatter)
            : base(new ConsoleTraceListener(), formatter)
        {
        }
#endif
        /// <summary>
        /// Initializes a new instance of <see cref="FormattedEventLogTraceListener"/> with a 
        /// <see cref="EventLogTraceListener"/> initialized with a source name.
        /// </summary>
        /// <param name="source">The source name for the wrapped listener.</param>
#if WINDOWS
        public FormattedEventLogTraceListener(string source)
            : base(new EventLogTraceListener(source))
        {
            Guard.ArgumentNotNullOrEmpty(source, "source");
        }
#else
        public FormattedEventLogTraceListener(string source)
            : base(new ConsoleTraceListener())
        {
            Guard.ArgumentNotNullOrEmpty(source, "source");
        }
#endif


        /// <summary>
        /// Initializes a new instance of <see cref="FormattedEventLogTraceListener"/> with an 
        /// <see cref="ILogFormatter"/> and a <see cref="EventLogTraceListener"/> 
        /// initialized with source name and default log and machine names.
        /// </summary>
        /// <param name="source">The source name for the wrapped listener.</param>
        /// <param name="formatter">The formatter for the wrapper.</param>
#if WINDOWS

        public FormattedEventLogTraceListener(string source, ILogFormatter formatter)
            : base(new EventLogTraceListener(source), formatter)
        {
            Guard.ArgumentNotNullOrEmpty(source, "source");
        }
#else
        public FormattedEventLogTraceListener(string source, ILogFormatter formatter)
            : base(new ConsoleTraceListener(), formatter)
        {
            Guard.ArgumentNotNullOrEmpty(source, "source");
        }
#endif
        /// <summary>
        /// Initializes a new instance of <see cref="FormattedEventLogTraceListener"/> with an 
        /// <see cref="ILogFormatter"/> and a <see cref="EventLogTraceListener"/> 
        /// initialized with source name.
        /// </summary>
        /// <param name="source">The source name for the wrapped listener.</param>
        /// <param name="log">The name of the event log.</param>
        /// <param name="formatter">The formatter for the wrapper.</param>
#if WINDOWS
        public FormattedEventLogTraceListener(string source, string log, ILogFormatter formatter)
            : base(new EventLogTraceListener(new EventLog(log, DefaultMachineName, source)), formatter)
        {
            Guard.ArgumentNotNullOrEmpty(source, "source");
        }
#else
        public FormattedEventLogTraceListener(string source, string log, ILogFormatter formatter)
            : base(new ConsoleTraceListener(), formatter)
        {
            Guard.ArgumentNotNullOrEmpty(source, "source");
        }
#endif

        /// <summary>
        /// Initializes a new instance of <see cref="FormattedEventLogTraceListener"/> with an 
        /// <see cref="ILogFormatter"/> and a <see cref="EventLogTraceListener"/> 
        /// </summary>
        /// <param name="source">The source name for the wrapped listener.</param>
        /// <param name="log">The name of the event log.</param>
        /// <param name="machineName">The machine name for the event log.</param>
        /// <param name="formatter">The formatter for the wrapper.</param>
#if WINDOWS
        public FormattedEventLogTraceListener(string source, string log, string machineName, ILogFormatter formatter)
            : base(new EventLogTraceListener(new EventLog(log, NormalizeMachineName(machineName), source)), formatter)
        {
            Guard.ArgumentNotNullOrEmpty(source, "source");
        }
#else
        public FormattedEventLogTraceListener(string source, string log, string machineName, ILogFormatter formatter)
            : base(new ConsoleTraceListener(), formatter)
        {
            Guard.ArgumentNotNullOrEmpty(source, "source");
        }
#endif
        private static string NormalizeMachineName(string machineName)
        {
            return string.IsNullOrEmpty(machineName) ? DefaultMachineName : machineName;
        }
    }
}
