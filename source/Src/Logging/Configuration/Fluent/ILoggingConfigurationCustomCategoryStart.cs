// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.


namespace EnterpriseLibrary.Common.Configuration.Fluent
{
    /// <summary>
    /// Fluent interface that allows settings to be configured for a custom category source.
    /// </summary>
    public interface ILoggingConfigurationCustomCategoryStart : ILoggingConfigurationCategoryContd, IFluentInterface
    {
        /// <summary>
        /// Returns a fluent interface that can be used to further configure a custom category source.
        /// </summary>
        ILoggingConfigurationCustomCategoryOptions WithOptions
        {
            get;
        }
    }
}
