﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using EnterpriseLibrary.Logging.Configuration;
using EnterpriseLibrary.Logging.Formatters;
using System.ComponentModel;
using EnterpriseLibrary.Common.Properties;

namespace EnterpriseLibrary.Common.Configuration.Fluent
{

    /// <summary>
    /// Builder class used to configure a <see cref="TextFormatter"/> instance.
    /// </summary>
    /// <seealso cref="TextFormatter"/>
    /// <seealso cref="TextFormatterData"/>
    public class TextFormatterBuilder : ITextFormatterBuilder, IFormatterBuilder, IFluentInterface
    {
        TextFormatterData formatterData = new TextFormatterData();


        internal TextFormatterBuilder(string name)
        {
            formatterData.Name = name;
        }

        /// <summary>
        /// Specifies the text template that should be used when formatting a log message.
        /// </summary>
        /// <param name="template">The text template that should be used when formatting a log message.</param>
        /// <seealso cref="TextFormatter"/>
        /// <seealso cref="TextFormatterData"/>
        public IFormatterBuilder UsingTemplate(string template)
        {
            if (string.IsNullOrEmpty(template))
                throw new ArgumentException(Resources.ExceptionStringNullOrEmpty, "template");

            formatterData.Template = template;
            return this;
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
