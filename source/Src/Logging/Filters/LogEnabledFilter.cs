﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using EnterpriseLibrary.Common.Configuration;
using EnterpriseLibrary.Logging.Configuration;

namespace EnterpriseLibrary.Logging.Filters
{
    /// <summary>
    /// Represents a Boolean on/off filter.
    /// </summary>
    [ConfigurationElementType(typeof(LogEnabledFilterData))]
    public class LogEnabledFilter : LogFilter
    {
        private bool enabled = false;

        /// <summary>
        /// Initializes an instance of <see cref="LogEnabledFilter"/>.
        /// </summary>
        /// <param name="name">The name of the filter.</param>
        /// <param name="enabled">True if the filter allows messages, false otherwise.</param>
        public LogEnabledFilter(string name, bool enabled)
            : base(name)
        {
            this.enabled = enabled;
        }

        /// <summary>
        /// Tests to see if a message meets the criteria to be processed. 
        /// </summary>
        /// <param name="log">Log entry to test.</param>
        /// <returns><b>true</b> if the message passes through the filter and should be logged, <b>false</b> otherwise.</returns>
        public override bool Filter(LogEntry log)
        {
            return enabled;
        }

        /// <summary>
        /// Gets or set the Boolean flag for the filter.
        /// </summary>
        public bool Enabled
        {
            get { return this.enabled; }
            set { this.enabled = value; }
        }
    }
}
