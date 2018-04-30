// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using EnterpriseLibrary.Logging.Tests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseLibrary.Logging.ExtraInformation.Tests
{
    [TestClass]
    public class ManagedSecurityContextInformationProviderFixture
    {
        Dictionary<string, object> dictionary;
        ManagedSecurityContextInformationProvider provider;

        [TestInitialize]
        public void SetUp()
        {
            dictionary = new Dictionary<string, object>();
        }

        [TestMethod]
        public void PopulateDictionaryFilledCorrectly()
        {
            provider = new ManagedSecurityContextInformationProvider();
            provider.PopulateDictionary(dictionary);

            Assert.IsTrue(dictionary.Count > 0, "Dictionary contains no items");
            AssertUtilities.AssertStringDoesNotContain(dictionary[Resources.ManagedSecurity_AuthenticationType] as string, string.Format(Resources.ExtendedPropertyError, ""), "Authentication Type");
            AssertUtilities.AssertStringDoesNotContain(dictionary[Resources.ManagedSecurity_IdentityName] as string, string.Format(Resources.ExtendedPropertyError, ""), "Identity Name");
            AssertUtilities.AssertStringDoesNotContain(dictionary[Resources.ManagedSecurity_IsAuthenticated] as string, string.Format(Resources.ExtendedPropertyError, ""), "Is Authenticated");
        }
    }
}
