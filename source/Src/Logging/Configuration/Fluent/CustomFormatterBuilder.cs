﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using EnterpriseLibrary.Logging.Configuration;
using System.Collections.Specialized;
using EnterpriseLibrary.Logging.Formatters;
using System.ComponentModel;

namespace EnterpriseLibrary.Common.Configuration.Fluent
{
    /// <summary>
    /// Builder class used to configure a custom <see cref="ILogFormatter"/> instance.
    /// </summary>
    public class CustomFormatterBuilder : IFormatterBuilder, IFluentInterface
    {
        CustomFormatterData customFormatterData;

        internal CustomFormatterBuilder(string formatterName, Type customFormatterType)
            :this(formatterName, customFormatterType, new NameValueCollection())
        {
        }

        internal CustomFormatterBuilder(string formatterName, Type customFormatterType, NameValueCollection attributes)
        {
            customFormatterData = new CustomFormatterData(formatterName, customFormatterType);
            customFormatterData.Attributes.Add(attributes);
        }

        FormatterData IFormatterBuilder.GetFormatterData()
        {
            return customFormatterData;
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
