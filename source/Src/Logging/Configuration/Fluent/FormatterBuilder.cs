// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using EnterpriseLibrary.Logging.Formatters;
using System.ComponentModel;

namespace EnterpriseLibrary.Common.Configuration
{
    /// <summary>
    /// Entry point for configuring instances of <see cref="ILogFormatter"/>.
    /// </summary>
    public class FormatterBuilder : IFluentInterface
    {
        /// <summary>
        /// Creates an instance of <see cref="FormatterBuilder"/>, which functions as an entry point to configure instances of <see cref="ILogFormatter"/>.
        /// </summary>
        public FormatterBuilder()
        {
        }

        /// <summary>
        /// Redeclaration that hides the <see cref="object.GetHashCode()"/> method from IntelliSense.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Redeclaration that hides the <see cref="object.ToString()"/> method from IntelliSense.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string ToString()
        {
            return base.ToString();
        }

        /// <summary>
        /// Redeclaration that hides the <see cref="object.Equals(object)"/> method from IntelliSense.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
       
    }
}
