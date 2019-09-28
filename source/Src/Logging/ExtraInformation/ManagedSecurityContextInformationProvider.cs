// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Practices.EnterpriseLibrary.Logging.ExtraInformation
{
    /// <summary>
    /// Provides useful diagnostic information from the managed runtime.
    /// </summary>
    public class ManagedSecurityContextInformationProvider : IExtraInformationProvider
    {
        /// <summary>
        /// Populates an <see cref="IDictionary{K,T}"/> with helpful diagnostic information.
        /// </summary>
        /// <param name="dict">Dictionary used to populate the <see cref="ManagedSecurityContextInformationProvider"></see></param>
        public void PopulateDictionary(IDictionary<string, object> dict)
        {
            dict.Add(Properties.Resources.ManagedSecurity_AuthenticationType, AuthenticationType);
            dict.Add(Properties.Resources.ManagedSecurity_IdentityName, IdentityName);
            dict.Add(Properties.Resources.ManagedSecurity_IsAuthenticated, IsAuthenticated.ToString());
        }

        /// <summary>
        ///        Gets the AuthenticationType, calculating it if necessary. 
        /// </summary>
        public string AuthenticationType
        {
            // .NET Core has a null CurrentPrincipal if it hasn't been set whereas .NET Framework has an
            // empty one. Explicitly translate null to empty here so that the two work the same.
            get { return Thread.CurrentPrincipal?.Identity.AuthenticationType ?? string.Empty; }
        }

        /// <summary>
        ///        Gets the IdentityName, calculating it if necessary. 
        /// </summary>
        public string IdentityName
        {
            // .NET Core has a null CurrentPrincipal if it hasn't been set whereas .NET Framework has an
            // empty one. Explicitly translate null to empty here so that the two work the same.
            get { return Thread.CurrentPrincipal?.Identity.Name ?? string.Empty; }
        }

        /// <summary>
        ///        Gets the IsAuthenticated, calculating it if necessary. 
        /// </summary>
        public bool IsAuthenticated
        {
            // .NET Core has a null CurrentPrincipal if it hasn't been set whereas .NET Framework has an
            // empty one. Explicitly translate null to false here so that the two work the same.
            get { return Thread.CurrentPrincipal?.Identity.IsAuthenticated ?? false; }
        }
    }
}
