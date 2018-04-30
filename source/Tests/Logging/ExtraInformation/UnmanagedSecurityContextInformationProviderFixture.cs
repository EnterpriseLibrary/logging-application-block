﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using EnterpriseLibrary.Logging.Tests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseLibrary.Logging.ExtraInformation.Tests
{
    [TestClass]
    public class UnmanagedSecurityContextInformationProviderFixture
    {
        Dictionary<string, object> dictionary;
        UnmanagedSecurityContextInformationProvider provider;

        [TestInitialize]
        public void SetUp()
        {
            dictionary = new Dictionary<string, object>();
        }

        [TestMethod]
        public void PopulateDictionaryFilledCorrectly()
        {
            provider = new UnmanagedSecurityContextInformationProvider();
            provider.PopulateDictionary(dictionary);

            Assert.AreEqual(2, dictionary.Count);
            AssertUtilities.AssertStringDoesNotContain(dictionary[Resources.UnmanagedSecurity_CurrentUser] as string, string.Format(Resources.ExtendedPropertyError, ""), "CurrentUser");
            AssertUtilities.AssertStringDoesNotContain(dictionary[Resources.UnmanagedSecurity_ProcessAccountName] as string, string.Format(Resources.ExtendedPropertyError, ""), "ProcessAccountName");
        }
    }
}
