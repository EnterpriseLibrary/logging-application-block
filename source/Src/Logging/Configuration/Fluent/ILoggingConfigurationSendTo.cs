// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.


namespace EnterpriseLibrary.Common.Configuration.Fluent
{

    /// <summary>
    /// Fluent interface that allows tracelisteners to be configured.
    /// </summary>
    public interface ILoggingConfigurationSendTo : IFluentInterface
    {
        /// <summary>
        /// Creates a reference to an existing Trace Listener with a specific name.
        /// </summary>
        /// <param name="listenerName">The name of the Trace Listener a reference should be made for.</param>
        ILoggingConfigurationCategoryContd SharedListenerNamed(string listenerName);

    }

}
