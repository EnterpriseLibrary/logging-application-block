﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using EnterpriseLibrary.Logging.Configuration;
using System.Diagnostics;
using EnterpriseLibrary.Common.Configuration.Fluent;
using EnterpriseLibrary.Logging.Properties;
using CommonResources = EnterpriseLibrary.Common.Properties.Resources;

namespace EnterpriseLibrary.Common.Configuration
{
    /// <summary>
    /// <see cref="IConfigurationSourceBuilder"/> extensions to support creation of logging configuration sections.
    /// </summary>
    public static class LoggingConfigurationSourceBuilderExtensions
    {
        /// <summary>
        /// Main entry point to configuration a <see cref="LoggingSettings"/> section.
        /// </summary>
        /// <param name="configurationSourceBuilder">The builder interface to extend.</param>
        /// <returns></returns>
        public static ILoggingConfigurationStart ConfigureLogging(this IConfigurationSourceBuilder configurationSourceBuilder)
        {
            if(configurationSourceBuilder == null) throw new ArgumentNullException("configurationSourceBuilder");

            LoggingSettings loggingSettings = new LoggingSettings();
            configurationSourceBuilder.AddSection(LoggingSettings.SectionName, loggingSettings);

            return new LoggingConfigurationBuilder(loggingSettings);
        }

        private class LoggingConfigurationBuilder :
            ILoggingConfigurationStart,
            ILoggingConfigurationOptions,
            ILoggingConfigurationSpecialSources,
            ILoggingConfigurationExtension,
            ILoggingConfigurationSendTo,
            ILoggingConfigurationSendToExtension,
            ILoggingConfigurationCustomCategoryStart,
            ILoggingConfigurationCustomCategoryOptions,
            ILoggingConfigurationCategoryStart,
            ILoggingConfigurationCategoryOptions,
            ILoggingConfigurationCategoryContd
        {
            LoggingSettings loggingSettings;
            TraceSourceData currentTraceSource;

            public LoggingConfigurationBuilder(LoggingSettings loggingSettings)
            {
                this.loggingSettings = loggingSettings;
                this.loggingSettings.SpecialTraceSources = new SpecialTraceSourcesData();
            }

            ILoggingConfigurationCustomCategoryOptions ILoggingConfigurationCustomCategoryOptions.ToSourceLevels(SourceLevels sourceLevels)
            {
                currentTraceSource.DefaultLevel = sourceLevels;
                return this;
            }

            ILoggingConfigurationCustomCategoryOptions ILoggingConfigurationCustomCategoryOptions.DoNotAutoFlushEntries()
            {
                currentTraceSource.AutoFlush = false;

                return this;
            }

            ILoggingConfigurationOptions ILoggingConfigurationOptions.DisableTracing()
            {
                loggingSettings.TracingEnabled = false;

                return this;
            }

            ILoggingConfigurationCategoryContd ILoggingConfigurationSendTo.SharedListenerNamed(string listenerName)
            {
                if (string.IsNullOrEmpty(listenerName)) 
                    throw new ArgumentException(CommonResources.ExceptionStringNullOrEmpty, "listenerName");

                currentTraceSource.TraceListeners.Add(new TraceListenerReferenceData(listenerName));
                return this;
            }

            ILoggingConfigurationSendTo ILoggingConfigurationCategoryContd.SendTo
            {
                get
                {
                    return this;
                }
            }

            ILoggingConfigurationOptions ILoggingConfigurationOptions.DoNotRevertImpersonation()
            {
                loggingSettings.RevertImpersonation = false;

                return this;
            }

            ILoggingConfigurationOptions ILoggingConfigurationOptions.DoNotLogWarningsWhenNoCategoryExists()
            {
                loggingSettings.LogWarningWhenNoCategoriesMatch = false;

                return this;
            }


            ILoggingConfigurationCustomCategoryStart ILoggingConfigurationContd.LogToCategoryNamed(string categoryName)
            {
                if (string.IsNullOrEmpty(categoryName))
                    throw new ArgumentException(CommonResources.ExceptionStringNullOrEmpty, "categoryName");

                currentTraceSource = new TraceSourceData()
                {
                    Name = categoryName
                };

                loggingSettings.TraceSources.Add(currentTraceSource);
                return this;
            }

            ILoggingConfigurationCustomCategoryOptions ILoggingConfigurationCustomCategoryOptions.SetAsDefaultCategory()
            {
                loggingSettings.DefaultCategory = currentTraceSource.Name;

                return this;
            }

            TraceSourceData ILoggingConfigurationSendToExtension.CurrentTraceSource
            {
                get { return currentTraceSource; }
            }

            ILoggingConfigurationSpecialSources ILoggingConfigurationContd.SpecialSources
            {
                get { return this; }
            }

            ILoggingConfigurationCategoryStart ILoggingConfigurationSpecialSources.LoggingErrorsAndWarningsCategory
            {
                get 
                {
                    loggingSettings.SpecialTraceSources.ErrorsTraceSource = new TraceSourceData
                    {
                        Name = Resources.ErrorsTraceSourceName
                    };
                    currentTraceSource = loggingSettings.SpecialTraceSources.ErrorsTraceSource;

                    return this;
                }
            }

            ILoggingConfigurationCategoryStart ILoggingConfigurationSpecialSources.UnprocessedCategory
            {
                get
                {
                    loggingSettings.SpecialTraceSources.NotProcessedTraceSource = new TraceSourceData
                    {
                        Name = Resources.NotProcessedTraceSourceName
                    };
                    currentTraceSource = loggingSettings.SpecialTraceSources.NotProcessedTraceSource;

                    return this;
                }
            }

            ILoggingConfigurationCategoryStart ILoggingConfigurationSpecialSources.AllEventsCategory
            {
                get
                {
                    loggingSettings.SpecialTraceSources.AllEventsTraceSource = new TraceSourceData
                    {
                        Name = Resources.AllEventsTraceSourceName
                    };
                    currentTraceSource = loggingSettings.SpecialTraceSources.AllEventsTraceSource;

                    return this;
                }
            }

            LoggingSettings ILoggingConfigurationExtension.LoggingSettings
            {
                get { return loggingSettings; }
            }

            ILoggingConfigurationCategoryOptions ILoggingConfigurationCategoryOptions.ToSourceLevels(SourceLevels sourceLevels)
            {
                currentTraceSource.DefaultLevel = sourceLevels;

                return this;
            }

            ILoggingConfigurationCategoryOptions ILoggingConfigurationCategoryOptions.DoNotAutoFlushEntries()
            {
                currentTraceSource.AutoFlush = false;

                return this;
            }

            ILoggingConfigurationCategoryOptions ILoggingConfigurationCategoryStart.WithOptions
            {
                get { return this; }
            }

            ILoggingConfigurationOptions ILoggingConfigurationStart.WithOptions
            {
                get { return this; }
            }

            ILoggingConfigurationCustomCategoryOptions ILoggingConfigurationCustomCategoryStart.WithOptions
            {
                get { return this; }
            }


            ILoggingConfigurationCategoryContd ILoggingConfigurationSendToExtension.LoggingCategoryContd
            {
                get { return this; }
            }

            ILoggingConfigurationOptions ILoggingConfigurationExtension.LoggingOptions
            {
                get { return this; }
            }
        }
    }

}
