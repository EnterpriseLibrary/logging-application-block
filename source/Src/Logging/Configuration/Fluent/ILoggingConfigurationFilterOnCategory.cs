// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using EnterpriseLibrary.Logging.Configuration;
using EnterpriseLibrary.Logging.Filters;

namespace EnterpriseLibrary.Common.Configuration.Fluent
{
    /// <summary>
    /// Fluent interface used to configure a <see cref="CategoryFilter"/> instance.
    /// </summary>
    /// <see cref="CategoryFilter"/>
    /// <see cref="CategoryFilterData"/>
    public interface ILoggingConfigurationFilterOnCategory : IFluentInterface
    {
        /// <summary>
        /// Specifies that logging is enabled for the specified categories.<br/>
        /// Disabled for all other categories.
        /// </summary>
        /// <param name="categories">The categories for which logging should be enabled.</param>
        /// <returns>Fluent interface for further configuring logging settings.</returns>
        /// <see cref="CategoryFilter"/>
        /// <see cref="CategoryFilterData"/>
        ILoggingConfigurationOptions AllowAllCategoriesExcept(params string[] categories);

        /// <summary>
        /// Specifies that logging is disabled for the specified categories.<br/>
        /// Enabled for all other categories.
        /// </summary>
        /// <param name="categories">The categories for which logging should be disabled.</param>
        /// <returns>Fluent interface for further configuring logging settings.</returns>
        /// <see cref="CategoryFilter"/>
        /// <see cref="CategoryFilterData"/>
        ILoggingConfigurationOptions DenyAllCategoriesExcept(params string[] categories);
    }
}
