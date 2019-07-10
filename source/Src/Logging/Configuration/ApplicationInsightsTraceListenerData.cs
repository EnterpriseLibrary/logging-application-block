using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Design;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using System.Configuration;
using System.Diagnostics;

namespace Microsoft.Practices.EnterpriseLibrary.Logging.Configuration
{
    /// <summary>
    /// Configuration object for <see cref="ApplicationInsightsTraceListener"/>
    /// </summary>
    public class ApplicationInsightsTraceListenerData : TraceListenerData
    {
        private const string InstrumentationKeyProperty = "instrumentationKey";
        private const string FormatterNameProperty = "formatter";
    
        /// <summary>
        /// Initializes a <see cref="ApplicationInsightsTraceListenerData"/>
        /// </summary>
        public ApplicationInsightsTraceListenerData()
            : base(typeof(ApplicationInsightsTraceListener))
        {
            this.ListenerDataType = typeof(ApplicationInsightsTraceListenerData);
        }

        /// <summary>
        /// Initializes a named instance of <see cref="ApplicationInsightsTraceListener"/> with 
        /// name, instrumentation key, and formatter name.
        /// </summary>
        /// <param name="name">The listener name</param>
        /// <param name="instrumentationKey">Instrumentation key to use for connecting to Application Insights</param>
        /// <param name="formatterName">The formatter name</param>
        /// <param name="traceOutputOptions">The trace options</param>
        /// <param name="filter">The filter to be applied</param>
        public ApplicationInsightsTraceListenerData(string name, 
                                                    string instrumentationKey, 
                                                    string formatterName, 
                                                    TraceOptions traceOutputOptions,
                                                    SourceLevels filter)
            : base(name, typeof(ApplicationInsightsTraceListener), traceOutputOptions, filter)
        {
            InstrumentationKey = instrumentationKey;
            Formatter = formatterName;
        }

        /// <summary>
        /// Gets and sets the instrumentation key
        /// </summary>
        [ConfigurationProperty(InstrumentationKeyProperty, IsRequired = true)]
        [ResourceDescription(typeof(DesignResources), "ApplicationInsightsTraceListenerDataInstrumentationKeyDescription")]
        [ResourceDisplayName(typeof(DesignResources), "ApplicationInsightsTraceListenerDataInstrumentationKeyDisplayName")]
        public string InstrumentationKey
        {
            get { return (string)base[InstrumentationKeyProperty]; }
            set { base[InstrumentationKeyProperty] = value; }
        }

        /// <summary>
        /// Gets and sets the formatter name.
        /// </summary>
        [ConfigurationProperty(FormatterNameProperty, IsRequired = false)]
        [ResourceDescription(typeof(DesignResources), "ApplicationInsightsTraceListenerDataFormatterDescription")]
        [ResourceDisplayName(typeof(DesignResources), "ApplicationInsightsTraceListenerDataFormatterDisplayName")]
        [Reference(typeof(NameTypeConfigurationElementCollection<FormatterData, CustomFormatterData>), typeof(FormatterData))]
        public string Formatter
        {
            get { return (string)base[FormatterNameProperty]; }
            set { base[FormatterNameProperty] = value; }
        }

        /// <summary>
        /// Builds the log <see cref="ApplicationInsightsTraceListener"/> object represented by this configuration object 
        /// </summary>
        /// <param name="settings">The configuration settings for logging</param>
        /// <returns>An Application Insights trace listener</returns>
        protected override TraceListener CoreBuildTraceListener(LoggingSettings settings) =>
            new ApplicationInsightsTraceListener(this.InstrumentationKey) { Formatter = this.BuildFormatterSafe(settings, this.Formatter)};
    }
}
