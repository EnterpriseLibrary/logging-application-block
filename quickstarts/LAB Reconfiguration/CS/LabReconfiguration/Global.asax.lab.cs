﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security;
using System.Web;
using EnterpriseLibrary.Common.Configuration;
using EnterpriseLibrary.Logging;

namespace LabReconfiguration
{
    partial class MvcApplication
    {
        internal const string LoggingVerbositySettingName = "LoggingVerbosity";
        internal const string AppSettingsFileName = "appSettings.config";

        private IConfigurationSource configurationSource;

        partial void InitializeDiagnostics()
        {
            this.configurationSource = new FileConfigurationSource(Path.Combine(HttpRuntime.AppDomainAppPath, "web.config"));

            Logger.SetLogWriter(new LogWriterFactory(this.configurationSource).Create());

            this.UpdateVerbosity();
            this.configurationSource.SourceChanged += (s, e) => { if (e.ChangedSectionNames.Contains("appSettings")) this.UpdateVerbosity(); };
        }

        private void UpdateVerbosity()
        {
            var appSettings = this.configurationSource.GetSection("appSettings") as AppSettingsSection;
            KeyValueConfigurationElement verbositySetting = null;
            if (appSettings == null || (verbositySetting = appSettings.Settings[LoggingVerbositySettingName]) == null)
            {
                Logger.Write("Missing verbosity setting. Ignoring", "General", 0, 0, TraceEventType.Warning);
            }

            SourceLevels verbosity;
            if (Enum.TryParse<SourceLevels>(verbositySetting.Value, out verbosity))
            {
                Logger.Write(string.Format(CultureInfo.CurrentCulture, "Updating verbosity to {0}", verbosity), "General", 0, 0, TraceEventType.Information);
                Logger.Writer.Configure(config =>
                    {
                        config.LogSources["Messaging"].Level = verbosity;
                    });
            }
            else
            {
                Logger.Write("Invalid verbosity setting. Ignoring", "General", 0, 0, TraceEventType.Warning);
            }
        }

        public override void Dispose()
        {
            if (this.configurationSource != null)
            {
                this.configurationSource.Dispose();
            }

            base.Dispose();
        }
    }
}