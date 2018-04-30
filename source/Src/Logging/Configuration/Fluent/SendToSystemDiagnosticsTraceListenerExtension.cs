﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using EnterpriseLibrary.Logging.Configuration;
using System.Diagnostics;
using EnterpriseLibrary.Common.Configuration.Fluent;
using EnterpriseLibrary.Common.Properties;
using System.Globalization;

namespace EnterpriseLibrary.Common.Configuration
{
    /// <summary>
    /// Extension methods to support configuration of <see cref="System.Diagnostics.TraceListener"/>.
    /// </summary>
    public static class SendToSystemDiagnosticsTraceListenerExtensions
    {
        /// <summary>
        /// Adds a new <see cref="System.Diagnostics.TraceListener"/> to the logging settings and creates
        /// a reference to this Trace Listener for the current category source.
        /// </summary>
        /// <param name="context">Fluent interface extension point.</param>
        /// <param name="listenerName">The name of the <see cref="System.Diagnostics.TraceListener"/>.</param>
        /// <returns>Fluent interface that can be used to further configure the created <see cref="SystemDiagnosticsTraceListenerData"/>. </returns>
        /// <seealso cref="System.Diagnostics.TraceListener"/>
        /// <seealso cref="SystemDiagnosticsTraceListenerData"/>
        public static ILoggingConfigurationSendToSystemDiagnosticsTraceListener SystemDiagnosticsListener(this ILoggingConfigurationSendTo context, string listenerName)
        {
            if (string.IsNullOrEmpty(listenerName))
                throw new ArgumentException(Resources.ExceptionStringNullOrEmpty, "listenerName");

            return new SendToSystemDiagnosticsTraceListenerBuilder(context, listenerName);
        }

        private class SendToSystemDiagnosticsTraceListenerBuilder : SendToTraceListenerExtension, ILoggingConfigurationSendToSystemDiagnosticsTraceListener
        {
            SystemDiagnosticsTraceListenerData systemDiagnosticsData;

            public SendToSystemDiagnosticsTraceListenerBuilder(ILoggingConfigurationSendTo context, string listenerName)
                :base(context)
            {
                systemDiagnosticsData = new SystemDiagnosticsTraceListenerData()
                {
                    Name = listenerName
                };

                base.AddTraceListenerToSettingsAndCategory(systemDiagnosticsData);
            }

            public ILoggingConfigurationSendToSystemDiagnosticsTraceListener ForTraceListenerType(Type tracelistenerType)
            {
                if (tracelistenerType == null) throw new ArgumentNullException("tracelistenerType");

                if (!typeof(TraceListener).IsAssignableFrom(tracelistenerType))
                    throw new ArgumentException(string.Format(CultureInfo.CurrentCulture,
                        Resources.ExceptionTypeMustDeriveFromType, typeof(TraceListener)), "tracelistenerType");

                systemDiagnosticsData.Type = tracelistenerType;

                return this;
            }

            public ILoggingConfigurationSendToSystemDiagnosticsTraceListener ForTraceListenerType<TTraceListener>() where TTraceListener : TraceListener
            {
                return ForTraceListenerType(typeof(TTraceListener));
            }

            public ILoggingConfigurationSendToSystemDiagnosticsTraceListener UsingInitData(string initData)
            {
                systemDiagnosticsData.InitData = initData;

                return this;
            }

            public ILoggingConfigurationSendToSystemDiagnosticsTraceListener Filter(SourceLevels sourceLevel)
            {
                systemDiagnosticsData.Filter = sourceLevel;

                return this;
            }

            public ILoggingConfigurationSendToSystemDiagnosticsTraceListener WithTraceOptions(TraceOptions traceOptions)
            {
                systemDiagnosticsData.TraceOutputOptions = traceOptions;

                return this;
            }
        }

    }
}
