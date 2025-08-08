// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Diagnostics;
using System.Runtime.InteropServices;
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
        public FormattedEventLogTraceListener(EventLog eventLog)
                   : base(CreateListener(eventLog))
        {
        }



        /// <summary>
        /// Initializes a new instance of <see cref="FormattedEventLogTraceListener"/> with an 
        /// <see cref="ILogFormatter"/> and a <see cref="EventLogTraceListener"/> 
        /// initialized with <see cref="EventLog"/>.
        /// </summary>
        /// <param name="eventLog">The event log for the wrapped listener.</param>
        /// <param name="formatter">The formatter for the wrapper.</param>
        public FormattedEventLogTraceListener(EventLog eventLog, ILogFormatter formatter)
                   : base(CreateListener(eventLog), formatter)
        {
        }
        /// <summary>
        /// Initializes a new instance of <see cref="FormattedEventLogTraceListener"/> with a 
        /// <see cref="EventLogTraceListener"/> initialized with a source name.
        /// </summary>
        /// <param name="source">The source name for the wrapped listener.</param>
        public FormattedEventLogTraceListener(string source)
                    : base(CreateListener(source))
        {
            Guard.ArgumentNotNullOrEmpty(source, nameof(source));
        }


        /// <summary>
        /// Initializes a new instance of <see cref="FormattedEventLogTraceListener"/> with an 
        /// <see cref="ILogFormatter"/> and a <see cref="EventLogTraceListener"/> 
        /// initialized with source name and default log and machine names.
        /// </summary>
        /// <param name="source">The source name for the wrapped listener.</param>
        /// <param name="formatter">The formatter for the wrapper.</param>
        public FormattedEventLogTraceListener(string source, ILogFormatter formatter)
                  : base(CreateListener(source), formatter)
        {
            Guard.ArgumentNotNullOrEmpty(source, nameof(source));
        }

        /// <summary>
        /// Initializes a new instance of <see cref="FormattedEventLogTraceListener"/> with an 
        /// <see cref="ILogFormatter"/> and a <see cref="EventLogTraceListener"/> 
        /// initialized with source name.
        /// </summary>
        /// <param name="source">The source name for the wrapped listener.</param>
        /// <param name="log">The name of the event log.</param>
        /// <param name="formatter">The formatter for the wrapper.</param>
        public FormattedEventLogTraceListener(string source, string log, ILogFormatter formatter)
                  : base(CreateListener(source, log, DefaultMachineName), formatter)
        {
            Guard.ArgumentNotNullOrEmpty(source, nameof(source));
        }


        /// <summary>
        /// Initializes a new instance of <see cref="FormattedEventLogTraceListener"/> with an 
        /// <see cref="ILogFormatter"/> and a <see cref="EventLogTraceListener"/> 
        /// </summary>
        /// <param name="source">The source name for the wrapped listener.</param>
        /// <param name="log">The name of the event log.</param>
        /// <param name="machineName">The machine name for the event log.</param>
        /// <param name="formatter">The formatter for the wrapper.</param>
        public FormattedEventLogTraceListener(string source, string log, string machineName, ILogFormatter formatter)
                    : base(CreateListener(source, log, NormalizeMachineName(machineName)), formatter)
        {
            Guard.ArgumentNotNullOrEmpty(source, nameof(source));
        }
        private static TraceListener CreateListener(EventLog eventLog)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return new EventLogTraceListener(eventLog);
            return new ConsoleTraceListener();
        }

        private static TraceListener CreateListener(string source)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return new EventLogTraceListener(source);
            return new ConsoleTraceListener();
        }

        private static TraceListener CreateListener(string source, string log, string machineName)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return new EventLogTraceListener(new EventLog(log, machineName, source));
            return new ConsoleTraceListener();
        }

        private static string NormalizeMachineName(string machineName)
        {
            return string.IsNullOrEmpty(machineName) ? DefaultMachineName : machineName;
        }
         
    }
}
