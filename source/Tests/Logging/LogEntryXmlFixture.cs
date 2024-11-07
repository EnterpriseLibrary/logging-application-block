// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.
 
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TestSupport;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Practices.EnterpriseLibrary.Logging.Tests
{
    /// <summary>
    /// Summary description for LogEntryXmlFixture
    /// </summary>
    [TestClass]
    public class LogEntryXmlFixture
    {
#if !NET8_0
        [TestMethod]
        public void CanDeserializeLogEntryXmlUsingBinaryFormatter()
        {
            XmlLogEntry entry = CommonUtil.CreateXmlLogEntry();

            string serializedLogEntryXmlText = new BinaryLogFormatter().Format(entry);
            XmlLogEntry desiaralizedLogEntryXml = (XmlLogEntry)BinaryLogFormatter.Deserialize(serializedLogEntryXmlText);

            Assert.IsNotNull(desiaralizedLogEntryXml);
            CommonUtil.AssertXmlLogEntries(entry, desiaralizedLogEntryXml);
        }
#endif
        




    }
}
