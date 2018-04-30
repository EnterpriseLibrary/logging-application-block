// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using EnterpriseLibrary.Logging.Configuration;
using System.Diagnostics;
using EnterpriseLibrary.Common.Configuration.Fluent;
using EnterpriseLibrary.Common.Properties;
using EnterpriseLibrary.Logging.TraceListeners;

namespace EnterpriseLibrary.Common.Configuration
{
    /// <summary>
    /// Extension methods to support configuration of <see cref="XmlTraceListener"/>.
    /// </summary>
    public static class SendToXmlTraceListenerExtensions
    {    
        /// <summary>
        /// Adds a new <see cref="XmlTraceListener"/> to the logging settings and creates
        /// a reference to this Trace Listener for the current category source.
        /// </summary>
        /// <param name="context">Fluent interface extension point.</param>
        /// <param name="listenerName">The name of the <see cref="XmlTraceListener"/>.</param>
        /// <returns>Fluent interface that can be used to further configure the created <see cref="XmlTraceListenerData"/>. </returns>
        /// <seealso cref="XmlTraceListener"/>
        /// <seealso cref="XmlTraceListenerData"/>
        public static ILoggingConfigurationSendToXmlTraceListener XmlFile(this ILoggingConfigurationSendTo context, string listenerName)
        {
            if (string.IsNullOrEmpty(listenerName))
                throw new ArgumentException(Resources.ExceptionStringNullOrEmpty, "listenerName");

            return new SendToXmlTraceListenerBuilder(context, listenerName);
        }

        private class SendToXmlTraceListenerBuilder : SendToTraceListenerExtension, ILoggingConfigurationSendToXmlTraceListener
        {            
            XmlTraceListenerData xmlTraceListener;

            public SendToXmlTraceListenerBuilder(ILoggingConfigurationSendTo context, string listenerName)
                :base(context)
            {
                xmlTraceListener = new XmlTraceListenerData
                {
                    Name = listenerName
                };

                base.AddTraceListenerToSettingsAndCategory(xmlTraceListener);
            }

            public ILoggingConfigurationSendToXmlTraceListener ToFile(string filename)
            {
                if (string.IsNullOrEmpty(filename))
                    throw new ArgumentException(Resources.ExceptionStringNullOrEmpty, "filename");

                xmlTraceListener.FileName = filename;
                
                return this;
            }

            public ILoggingConfigurationSendToXmlTraceListener Filter(SourceLevels sourceLevel)
            {
                xmlTraceListener.Filter = sourceLevel;

                return this;
            }

            public ILoggingConfigurationSendToXmlTraceListener WithTraceOptions(TraceOptions traceOptions)
            {
                xmlTraceListener.TraceOutputOptions = traceOptions;

                return this;
            }
        }

    }
}
