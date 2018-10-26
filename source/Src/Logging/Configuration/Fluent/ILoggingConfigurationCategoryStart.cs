// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.


namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Fluent
{
    /// <summary>
    /// Fluent interface that allows settings to be configured for a Category Source.
    /// </summary>
    public interface ILoggingConfigurationCategoryStart : ILoggingConfigurationCategoryContd, IFluentInterface
    {
        /// <summary>
        /// Returns a fluent interface for further configuring a logging category.
        /// </summary>
        ILoggingConfigurationCategoryOptions WithOptions { get; }
    }
}
