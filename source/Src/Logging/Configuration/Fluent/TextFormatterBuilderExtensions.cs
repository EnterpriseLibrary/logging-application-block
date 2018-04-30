// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using EnterpriseLibrary.Logging.Configuration;
using EnterpriseLibrary.Logging.Formatters;
using EnterpriseLibrary.Common.Configuration.Fluent;
using EnterpriseLibrary.Common.Properties;

namespace EnterpriseLibrary.Common.Configuration
{
    /// <summary>
    /// <see cref="FormatterBuilder"/> extensions to configure <see cref="TextFormatter"/> instances.
    /// </summary>
    /// <seealso cref="TextFormatter"/>
    /// <seealso cref="TextFormatterBuilder"/>
    /// <seealso cref="TextFormatterData"/>
    public static class TextFormatterBuilderExtensions
    {
        /// <summary>
        /// Creates the configuration builder for a <see cref="TextFormatter"/> instance.
        /// </summary>
        /// <param name="builder">Fluent interface extension point.</param>
        /// <param name="formatterName">The name of the <see cref="TextFormatter"/> instance that will be added to configuration.</param>
        /// <seealso cref="TextFormatter"/>
        /// <seealso cref="TextFormatterData"/>
        public static TextFormatterBuilder TextFormatterNamed(this FormatterBuilder builder, string formatterName)
        {
            if (string.IsNullOrEmpty(formatterName))
                throw new ArgumentException(Resources.ExceptionStringNullOrEmpty, "formatterName");

            return new TextFormatterBuilder(formatterName);
        }
    }
}
