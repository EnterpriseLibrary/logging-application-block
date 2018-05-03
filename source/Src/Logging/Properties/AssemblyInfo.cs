// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Runtime.InteropServices;
using System.Security;
using EnterpriseLibrary.Common.Configuration.Design;
using EnterpriseLibrary.Logging.Configuration;


[assembly: AllowPartiallyTrustedCallers]
[assembly: SecurityRules(SecurityRuleSet.Level1)]

[assembly: ComVisible(false)]

[assembly: HandlesSection(LoggingSettings.SectionName)]
[assembly: AddApplicationBlockCommand(
                LoggingSettings.SectionName,
                typeof(LoggingSettings),
                TitleResourceName = "AddLoggingSettings",
                TitleResourceType = typeof(DesignResources),
                CommandModelTypeName = LoggingDesignTime.CommandTypeNames.AddLoggingBlockCommand)]
