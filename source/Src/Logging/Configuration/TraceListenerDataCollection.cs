﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Configuration;
using System.Xml;
using EnterpriseLibrary.Common.Configuration;
using EnterpriseLibrary.Logging.Properties;
using System.Globalization;

namespace EnterpriseLibrary.Logging.Configuration
{
    /// <summary>
    /// Custom <see cref="PolymorphicConfigurationElementCollection{T}"/> that deals with <see cref="TraceListenerData"/>.
    /// </summary>
    /// <remarks>
    /// The default implementation based on annotations on the feature types can't be used because trace listeners can't be annotated.
    /// </remarks>
    public class TraceListenerDataCollection : PolymorphicConfigurationElementCollection<TraceListenerData>
    {
        /// <summary>
        /// Returns the <see cref="ConfigurationElement"/> type to created for the current xml node.
        /// </summary>
        /// <remarks>
        /// The <see cref="TraceListenerData"/> include the configuration object type as a serialized attribute.
        /// </remarks>
        /// <param name="reader">The <see cref="XmlReader"/> that is deserializing the element.</param>
        protected override Type RetrieveConfigurationElementType(XmlReader reader)
        {
            Type configurationElementType = null;

            if (reader.AttributeCount > 0)
            {
                // expect the first attribute to be the name
                for (bool go = reader.MoveToFirstAttribute(); go; go = reader.MoveToNextAttribute())
                {
                    if (TraceListenerData.listenerDataTypeProperty.Equals(reader.Name))
                    {
                        configurationElementType = Type.GetType(reader.Value);
                        if (configurationElementType == null)
                        {
                            throw new ConfigurationErrorsException(
                                string.Format(
                                    CultureInfo.CurrentCulture,
                                    Resources.ExceptionTraceListenerConfigurationElementTypeNotFound,
                                    reader.ReadOuterXml()));
                        }

                        break;
                    }
                }

                if (configurationElementType == null)
                {
                    throw new ConfigurationErrorsException(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            Resources.ExceptionTraceListenerConfigurationElementMissingTypeAttribute,
                            reader.ReadOuterXml()));
                }

                // cover the traces
                reader.MoveToElement();
            }

            return configurationElementType;
        }
    }
}
