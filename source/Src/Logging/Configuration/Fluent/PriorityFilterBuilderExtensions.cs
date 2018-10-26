// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Fluent;
using Microsoft.Practices.EnterpriseLibrary.Common.Properties;

namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration
{
    /// <summary/>
    public static class PriorityFilterBuilderExtensions
    {
        /// <summary/>
        public static ILoggingConfigurationFilterOnPriority FilterOnPriority(this ILoggingConfigurationOptions context, string logFilterName)
        {

            if (string.IsNullOrEmpty(logFilterName))
                throw new ArgumentException(Resources.ExceptionStringNullOrEmpty, "logFilterName");

            return new FilterOnPriorityBuilder(context, logFilterName);
        }


        private class FilterOnPriorityBuilder : LoggingConfigurationExtension, ILoggingConfigurationFilterOnPriority 
        {
            PriorityFilterData priorityFilterData;

            public FilterOnPriorityBuilder(ILoggingConfigurationOptions context, string logFilterName)
                :base(context)
            {
                priorityFilterData = new PriorityFilterData()
                {
                    Name = logFilterName
                };

                LoggingSettings.LogFilters.Add(priorityFilterData);
            }

            public ILoggingConfigurationFilterOnPriority StartingWithPriority(int minimumPriority)
            {
                priorityFilterData.MinimumPriority = minimumPriority;
                return this;
            }

            public ILoggingConfigurationFilterOnPriority UpToPriority(int maximumPriority)
            {
                priorityFilterData.MaximumPriority = maximumPriority;
                return this;
            }

        }
    }
}
