// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

namespace EnterpriseLibrary.Logging.Configuration
{
    internal static class LoggingDesignTime
    {
        internal static class ViewModelTypeNames
        {
            public const string LogggingSectionViewModel =
                "EnterpriseLibrary.Configuration.Design.ViewModel.BlockSpecifics.LoggingSectionViewModel, EnterpriseLibrary.Configuration.DesignTime";

            public const string SystemDiagnosticsTraceListenerDataViewModel = "EnterpriseLibrary.Configuration.Design.ViewModel.BlockSpecifics.SystemDiagnosticsTraceListenerDataViewModel, EnterpriseLibrary.Configuration.DesignTime";

            public const string CustomTraceListenerDataViewModel = "EnterpriseLibrary.Configuration.Design.ViewModel.BlockSpecifics.CustomTraceListenerDataViewModel, EnterpriseLibrary.Configuration.DesignTime";

            public const string TraceListenerReferenceViewModel =
                "EnterpriseLibrary.Configuration.Design.ViewModel.BlockSpecifics.TraceListenerReferenceViewModel, EnterpriseLibrary.Configuration.DesignTime";

            public const string TraceListenerElementCollectionViewModel =
                "EnterpriseLibrary.Configuration.Design.ViewModel.BlockSpecifics.TraceListenerElementCollectionViewModel, EnterpriseLibrary.Configuration.DesignTime";

            public const string TypeNameElementProperty =
                "EnterpriseLibrary.Configuration.Design.ViewModel.ElementProperty, EnterpriseLibrary.Configuration.DesignTime";

            public const string EmailTraceListenerPropertyViewModel =
                "EnterpriseLibrary.Configuration.Design.ViewModel.BlockSpecifics.Logging.EmailTraceListenerPasswordProperty, EnterpriseLibrary.Configuration.DesignTime";

            public const string SourceLevelsProperty =
                "EnterpriseLibrary.Configuration.Design.ViewModel.BlockSpecifics.Logging.SourceLevelsProperty, EnterpriseLibrary.Configuration.DesignTime";

            public const string TimeSpanElementConfigurationProperty =
                "EnterpriseLibrary.Configuration.Design.ViewModel.TimeSpanElementConfigurationProperty, EnterpriseLibrary.Configuration.DesignTime";
        }

        internal static class EditorTypeNames
        {
            public const string OverridenTraceListenerCollectionEditor =
                "EnterpriseLibrary.Configuration.Design.ViewModel.BlockSpecifics.OverriddenTraceListenerCollectionEditor, EnterpriseLibrary.Configuration.DesignTime";
        }

        internal static class CommandTypeNames
        {
            public const string AddLoggingBlockCommand = "EnterpriseLibrary.Configuration.Design.ViewModel.BlockSpecifics.AddLoggingBlockCommand, EnterpriseLibrary.Configuration.DesignTime";
        }

        internal static class ValidatorTypes
        {
            public const string EmailTraceListenerAuthenticationValidator = "EnterpriseLibrary.Configuration.Design.ViewModel.BlockSpecifics.Logging.EmailTraceListenerAuthenticationValidator, EnterpriseLibrary.Configuration.DesignTime";

            public const string LogPriorityMinMaxValidatorType = "EnterpriseLibrary.Configuration.Design.ViewModel.BlockSpecifics.Logging.LogPriorityMinMaxValidator, EnterpriseLibrary.Configuration.DesignTime";

            public const string NameValueCollectionValidator = "EnterpriseLibrary.Configuration.Design.Validation.NameValueCollectionValidator, EnterpriseLibrary.Configuration.DesignTime";
        }
    }
}
