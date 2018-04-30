// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using EnterpriseLibrary.Logging.Configuration;
using EnterpriseLibrary.Logging.Formatters;
using System.ComponentModel;

namespace EnterpriseLibrary.Common.Configuration.Fluent
{
    /// <summary>
    /// Builder class used to configure a <see cref="BinaryLogFormatter"/> instance.
    /// </summary>
    public class BinaryFormatterBuilder : IFormatterBuilder, IFluentInterface
    {
        BinaryLogFormatterData formatterData;

        internal BinaryFormatterBuilder(string formatterName)
        {
            formatterData = new BinaryLogFormatterData(formatterName);
        }

        FormatterData IFormatterBuilder.GetFormatterData()
        {
            return formatterData;
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
