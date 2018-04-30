﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

namespace EnterpriseLibrary.Common.Configuration.Fluent
{

    /// <summary>
    /// Fluent interface that allows log categories to be set up.
    /// </summary>
    public interface ILoggingConfigurationContd : IFluentInterface
    {
        /// <summary>
        /// Creates a Category Source in the configuration schema with the specified name.
        /// </summary>
        /// <param name="categoryName">The name of the Category Source.</param>
        /// <returns>Fluent interface that allows for this Category Source to be configured further.</returns>
        ILoggingConfigurationCustomCategoryStart LogToCategoryNamed(string categoryName);

        /// <summary>
        /// Returns an interface that can be used to configure special logging categories.
        /// </summary>
        ILoggingConfigurationSpecialSources SpecialSources { get; }
    }

}
