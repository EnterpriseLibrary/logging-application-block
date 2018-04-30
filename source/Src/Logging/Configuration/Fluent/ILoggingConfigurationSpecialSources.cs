// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.


namespace EnterpriseLibrary.Common.Configuration.Fluent
{

    /// <summary>
    /// Fluent interface used to configure pre defined logging categories.
    /// </summary>
    public interface ILoggingConfigurationSpecialSources : IFluentInterface
    {
        /// <summary>
        /// Returns an interface to configure the category source used for internal errors and warnings.
        /// </summary>
        ILoggingConfigurationCategoryStart LoggingErrorsAndWarningsCategory { get; }

        /// <summary>
        /// Returns an interface to configure the category source used to log messages that could not be processed
        /// </summary>
        ILoggingConfigurationCategoryStart UnprocessedCategory { get; }

        /// <summary>
        /// Returns an interface to configure the category source used to log all events.
        /// </summary>
        ILoggingConfigurationCategoryStart AllEventsCategory { get; }
    }

}
