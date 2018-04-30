// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using EnterpriseLibrary.Logging.Formatters;
using EnterpriseLibrary.Logging.TestSupport;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseLibrary.Logging.Tests
{
    /// <summary>
    /// Summary description for LogEntryXmlFixture
    /// </summary>
    [TestClass]
    public class LogEntryXmlFixture
    {
        [TestMethod]
        public void CanDeserializeLogEntryXmlUsingBinaryFormatter()
        {
            XmlLogEntry entry = CommonUtil.CreateXmlLogEntry();

            string serializedLogEntryXmlText = new BinaryLogFormatter().Format(entry);
            XmlLogEntry desiaralizedLogEntryXml = (XmlLogEntry)BinaryLogFormatter.Deserialize(serializedLogEntryXmlText);

            Assert.IsNotNull(desiaralizedLogEntryXml);
            CommonUtil.AssertXmlLogEntries(entry, desiaralizedLogEntryXml);
        }
    }
}
