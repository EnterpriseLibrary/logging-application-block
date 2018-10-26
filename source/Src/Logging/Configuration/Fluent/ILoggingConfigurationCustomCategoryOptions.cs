// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Diagnostics;

namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Fluent
{
    /// <summary>
    /// Fluent interface used to configure a custom logging category.
    /// </summary>
    public interface ILoggingConfigurationCustomCategoryOptions : ILoggingConfigurationCategoryContd
    {
        /// <summary>
        /// Specifed the default <see cref="SourceLevels"/> for this Category.<br/>
        /// By default the source level is set to <see cref="SourceLevels.All"/>.
        /// </summary>
        /// <param name="sourceLevels">The <see cref="SourceLevels"/> to be set as default.</param>
        /// <returns>Fluent interface that allows for this Category Source to be configured further.</returns>
        ILoggingConfigurationCustomCategoryOptions ToSourceLevels(SourceLevels sourceLevels);

        /// <summary>
        /// Specifies that Flush doesnt have to be called after every write to a listener.<br/>
        /// By default a Flush will be called after every write to a listener.
        /// </summary>
        /// <returns>Fluent interface that allows for this Category Source to be configured further.</returns>
        ILoggingConfigurationCustomCategoryOptions DoNotAutoFlushEntries();

        /// <summary>
        /// Specifies that this category will be the default category.
        /// </summary>
        /// <returns>Fluent interface that allows for this Category Source to be configured further.</returns>
        ILoggingConfigurationCustomCategoryOptions SetAsDefaultCategory();
    }
}
