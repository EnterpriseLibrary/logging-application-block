﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Globalization;
using EnterpriseLibrary.Logging.Configuration;
using EnterpriseLibrary.Logging.Properties;

namespace EnterpriseLibrary.Common.Configuration.Fluent
{
    /// <summary/>
    public abstract class LoggingConfigurationExtension : ILoggingConfigurationOptions, ILoggingConfigurationExtension
    {
        ILoggingConfigurationExtension contextExtension;

        /// <summary/>
        protected LoggingConfigurationExtension(ILoggingConfigurationContd context)
        {
            contextExtension = context as ILoggingConfigurationExtension;

            if (contextExtension == null) throw new ArgumentException(
                string.Format(CultureInfo.CurrentCulture, Resources.ParameterMustImplementType, typeof(ILoggingConfigurationExtension)),
                "context");
        }

        /// <summary/>
        protected LoggingSettings LoggingSettings
        {
            get { return contextExtension.LoggingSettings; }
        }

        /// <summary/>
        protected ILoggingConfigurationOptions LoggingOptions
        {
            get { return contextExtension.LoggingOptions; }
        }


        ILoggingConfigurationOptions ILoggingConfigurationOptions.DisableTracing()
        {
            return LoggingOptions.DisableTracing();
        }

        ILoggingConfigurationOptions ILoggingConfigurationOptions.DoNotRevertImpersonation()
        {
            return LoggingOptions.DoNotRevertImpersonation();
        }

        ILoggingConfigurationOptions ILoggingConfigurationOptions.DoNotLogWarningsWhenNoCategoryExists()
        {
            return LoggingOptions.DoNotLogWarningsWhenNoCategoryExists();
        }

        ILoggingConfigurationCustomCategoryStart ILoggingConfigurationContd.LogToCategoryNamed(string categoryName)
        {
            return LoggingOptions.LogToCategoryNamed(categoryName);
        }

        ILoggingConfigurationSpecialSources ILoggingConfigurationContd.SpecialSources
        {
            get { return LoggingOptions.SpecialSources; }
        }


        ILoggingConfigurationOptions ILoggingConfigurationExtension.LoggingOptions
        {
            get { return contextExtension.LoggingOptions; }
        }

        LoggingSettings ILoggingConfigurationExtension.LoggingSettings
        {
            get { return contextExtension.LoggingSettings; }
        }
    }
}
