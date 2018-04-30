// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using EnterpriseLibrary.Logging.Formatters;
using EnterpriseLibrary.Logging.Configuration;

namespace EnterpriseLibrary.Common.Configuration.Fluent
{
    /// <summary>
    /// Interface for builder classes used to configure <see cref="ILogFormatter"/> instances.
    /// </summary>
    public interface IFormatterBuilder
    {
        /// <summary>
        /// Returns the <see cref="FormatterData"/> instance that contains the configuration for an <see cref="ILogFormatter"/> instance.
        /// </summary>
        FormatterData GetFormatterData();
    }
}
