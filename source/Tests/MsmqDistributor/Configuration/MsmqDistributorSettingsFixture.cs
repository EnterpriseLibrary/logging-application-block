// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using EnterpriseLibrary.Common.Configuration;
using EnterpriseLibrary.Logging.TestSupport;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseLibrary.Logging.MsmqDistributor.Configuration.Tests
{
    [TestClass]
    public class MsmqDistributorSettingsFixture
    {
        [TestMethod]
        public void CanDeserializeSerializedSettings() { }

        [TestMethod]
        public void CanReadSettingsFromConfigurationFile()
        {
            using (var configurationSource = new SystemConfigurationSource(false))
            {
                MsmqDistributorSettings settings = MsmqDistributorSettings.GetSettings(configurationSource);

                Assert.IsNotNull(settings);
                Assert.AreEqual(CommonUtil.MessageQueuePath, settings.MsmqPath);
                Assert.AreEqual(1000, settings.QueueTimerInterval);
                Assert.AreEqual("Msmq Distributor", settings.ServiceName);
            }
        }
    }
}
