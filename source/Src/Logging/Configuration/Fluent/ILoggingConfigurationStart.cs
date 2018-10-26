// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.


namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Fluent
{

    /// <summary>
    /// Fluent interface that allows global logging settings to be configured.
    /// </summary>
    public interface ILoggingConfigurationStart : ILoggingConfigurationContd, IFluentInterface
    {
        /// <summary>
        /// Returns an fluent interface that can be used to further configure logging settings.
        /// </summary>
        ILoggingConfigurationOptions WithOptions { get; }

    }
}
