﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using EnterpriseLibrary.Logging.Configuration;
using System.Diagnostics;
using EnterpriseLibrary.Logging.TraceListeners;
using EnterpriseLibrary.Common.Configuration.Fluent;
using EnterpriseLibrary.Common.Properties;

namespace EnterpriseLibrary.Common.Configuration
{

    /// <summary>
    /// Extension methods to support configuration of <see cref="FlatFileTraceListener"/>.
    /// </summary>
    public static class SendToFlatFileTraceListenerExtension
    {
        /// <summary>
        /// Adds a new <see cref="FlatFileTraceListener"/> to the logging settings and creates
        /// a reference to this Trace Listener for the current category source.
        /// </summary>
        /// <param name="context">Fluent interface extension point.</param>
        /// <param name="listenerName">The name of the <see cref="FlatFileTraceListener"/>.</param>
        /// <returns>Fluent interface that can be used to further configure the created <see cref="FlatFileTraceListenerData"/>. </returns>
        /// <seealso cref="FlatFileTraceListenerData"/>
        public static ILoggingConfigurationSendToFlatFileTraceListener FlatFile(this ILoggingConfigurationSendTo context, string listenerName)
        {
            if (string.IsNullOrEmpty(listenerName)) 
                throw new ArgumentException(Resources.ExceptionStringNullOrEmpty, "listenerName");

            return new SendToFlatFileTraceListenerBuilder(context, listenerName);
        }

        private class SendToFlatFileTraceListenerBuilder : SendToTraceListenerExtension, ILoggingConfigurationSendToFlatFileTraceListener
        {
            FlatFileTraceListenerData flatFileTracelistenerData;

            public SendToFlatFileTraceListenerBuilder(ILoggingConfigurationSendTo context, string listenerName)
                :base(context)
            {
                flatFileTracelistenerData = new FlatFileTraceListenerData
                {
                    Name = listenerName
                };

                base.AddTraceListenerToSettingsAndCategory(flatFileTracelistenerData);
            }

            public ILoggingConfigurationSendToFlatFileTraceListener WithFooter(string footer)
            {
                flatFileTracelistenerData.Footer = footer;

                return this;
            }

            public ILoggingConfigurationSendToFlatFileTraceListener WithHeader(string header)
            {
                flatFileTracelistenerData.Header = header;

                return this;
            }

            public ILoggingConfigurationSendToFlatFileTraceListener ToFile(string filename)
            {
                if (string.IsNullOrEmpty(filename)) throw new ArgumentException(Resources.ExceptionStringNullOrEmpty, "filename");

                flatFileTracelistenerData.FileName = filename;

                return this;
            }

            public ILoggingConfigurationSendToFlatFileTraceListener FormatWith(IFormatterBuilder formatBuilder)
            {
                if (formatBuilder == null) throw new ArgumentNullException("formatBuilder");

                FormatterData formatter = formatBuilder.GetFormatterData();
                flatFileTracelistenerData.Formatter = formatter.Name;
                LoggingSettings.Formatters.Add(formatter);

                return this;
            }

            public ILoggingConfigurationSendToFlatFileTraceListener FormatWithSharedFormatter(string formatterName)
            {
                flatFileTracelistenerData.Formatter = formatterName;

                return this;
            }

            public ILoggingConfigurationSendToFlatFileTraceListener Filter(SourceLevels sourceLevel)
            {
                flatFileTracelistenerData.Filter = sourceLevel;

                return this;
            }

            public ILoggingConfigurationSendToFlatFileTraceListener WithTraceOptions(TraceOptions traceOptions)
            {
                flatFileTracelistenerData.TraceOutputOptions = traceOptions;

                return this;
            }
        }
    }
}
