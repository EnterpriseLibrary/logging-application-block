// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;

namespace Microsoft.Practices.EnterpriseLibrary.Logging.Formatters
{
// Keeping obsolete binary formatter in net 6/7 build for a compatibility with old code using EnterpriseLibrary. If not disabled this warning generates warning as error in net 6/7.
#pragma warning disable SYSLIB0011
    /// <summary>
    /// Log formatter that will format a <see cref="LogEntry"/> in a way suitable for wire transmission.
    /// </summary>
    [ConfigurationElementType(typeof(BinaryLogFormatterData))]
    public class BinaryLogFormatter : LogFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryLogFormatter"/> class.
        /// </summary>
        public BinaryLogFormatter()
        { }

        /// <summary>
        /// Formats a log entry as a serialized representation.
        /// </summary>
        /// <remarks>
        /// Will use a BinaryFormatter for doing the actual serialization.
        /// </remarks>
        /// <param name="log">The <see cref="LogEntry"/> to format.</param>
        /// <returns>A string version of the <see cref="LogEntry"/> that can be deserialized back to a <see cref="LogEntry"/> instance.</returns>
        public override string Format(LogEntry log)
        {
            using (MemoryStream binaryStream = new MemoryStream())
            {
                GetFormatter().Serialize(binaryStream, log);
                return Convert.ToBase64String(binaryStream.ToArray());
            }
        }

        /// <summary>
        /// Deserializes the string representation of a <see cref="LogEntry"/> into a <see cref="LogEntry"/> instance.
        /// </summary>
        /// <param name="serializedLogEntry">The serialized <see cref="LogEntry"/> representation.</param>
        /// <returns>The <see cref="LogEntry"/>.</returns>
        public static LogEntry Deserialize(string serializedLogEntry)
        {
            using (MemoryStream binaryStream = new MemoryStream(Convert.FromBase64String(serializedLogEntry)))
            {
                return (LogEntry)GetFormatter().Deserialize(binaryStream);
            }
        }

        private static BinaryFormatter GetFormatter()
        {
            return new BinaryFormatter();
        }
    }
#pragma warning restore SYSLIB0011
}
