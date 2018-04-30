﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Diagnostics;
using System.Collections.Generic;

namespace EnterpriseLibrary.Logging
{
    /// <summary>
    /// Used by <see cref="LogSource"/>s to keep the knowledge of which <see cref="TraceListeners"/> have been 
    /// already traced through. 
    /// </summary>
    public class TraceListenerFilter
    {
        private Dictionary<TraceListener, object> viewedTraceListeners;

        /// <summary>
        /// Initializes a new instance of the <see cref="TraceListenerFilter"/> class.
        /// </summary>
        public TraceListenerFilter()
        {
            viewedTraceListeners = new Dictionary<TraceListener, object>();
        }

        /// <summary>
        /// Gets the availiable trace listners from a list of all the trace listners.
        /// </summary>
        /// <param name="traceListeners">The list of all the trace listners.</param>
        /// <returns>A filtered list of trace listeners.</returns>
        public IEnumerable<TraceListener> GetAvailableTraceListeners(IEnumerable<TraceListener> traceListeners)
        {
            IList<TraceListener> filteredTraceListeners = new List<TraceListener>();
            foreach (TraceListener listener in traceListeners)
            {
                if (!viewedTraceListeners.ContainsKey(listener))
                {
                    viewedTraceListeners.Add(listener, listener);
                    filteredTraceListeners.Add(listener);
                }
            }

            return filteredTraceListeners;
        }
    }
}

