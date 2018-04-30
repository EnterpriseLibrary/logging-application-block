// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using EnterpriseLibrary.Logging.Formatters;

namespace EnterpriseLibrary.Common.Configuration.Fluent
{
    /// <summary>
    /// Fluent interface used to configure a <see cref="TextFormatter"/> instance.
    /// </summary>
    public interface ITextFormatterBuilder : IFormatterBuilder, IFluentInterface
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        IFormatterBuilder UsingTemplate(string template);
    }
}
