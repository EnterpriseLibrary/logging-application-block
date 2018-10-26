// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Configuration;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Design;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;

namespace Microsoft.Practices.EnterpriseLibrary.Logging.Configuration
{
    /// <summary>
    /// Represents the configuration settings that describe a <see cref="XmlTraceListener"/>.
    /// </summary>
    [ResourceDescription(typeof(DesignResources), "XmlTraceListenerDataDescription")]
    [ResourceDisplayName(typeof(DesignResources), "XmlTraceListenerDataDisplayName")]
    public class XmlTraceListenerData : TraceListenerData
    {
        private const string fileNameProperty = "fileName";

        /// <summary>
        /// Initializes a <see cref="XmlTraceListenerData"/>.
        /// </summary>
        public XmlTraceListenerData()
            : base(typeof(XmlTraceListener))
        {
            ListenerDataType = typeof(XmlTraceListenerData);
        }

        /// <summary>
        /// Initializes a <see cref="XmlTraceListenerData"/> with a filename and a formatter name.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="name">The name for the configuration object.</param>
        public XmlTraceListenerData(string name, string fileName)
            : base(name, typeof(XmlTraceListener), TraceOptions.None)
        {
            this.FileName = fileName;
        }

        /// <summary>
        /// Gets and sets the file name.
        /// </summary>
        [ConfigurationProperty(fileNameProperty, IsRequired = true, DefaultValue="trace-xml.log")]
        [ResourceDescription(typeof(DesignResources), "XmlTraceListenerDataFileNameDescription")]
        [ResourceDisplayName(typeof(DesignResources), "XmlTraceListenerDataFileNameDisplayName")]
        [System.ComponentModel.Editor(CommonDesignTime.EditorTypes.FilteredFilePath, CommonDesignTime.EditorTypes.UITypeEditor)]
        public string FileName
        {
            get { return (string)base[fileNameProperty]; }
            set { base[fileNameProperty] = value; }
        }

        /// <summary>
        /// Builds the <see cref="TraceListener" /> object represented by this configuration object.
        /// </summary>
        /// <param name="settings">The logging configuration settings.</param>
        /// <returns>
        /// An <see cref="XmlTraceListener"/>.
        /// </returns>
        protected override TraceListener CoreBuildTraceListener(LoggingSettings settings)
        {
            return new XmlTraceListener(this.FileName);
        }
    }
}
