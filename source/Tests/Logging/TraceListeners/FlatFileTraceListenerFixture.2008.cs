﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using EnterpriseLibrary.Logging.Formatters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseLibrary.Logging.TraceListeners.Tests
{
    public partial class FlatFileTraceListenerFixture
    {
        [TestMethod]
        [ExpectedException(typeof(SecurityException))]
        public void FlatFileListenerWillFallbackIfNotPriviledgesToWrite()
        {
            string fileName = @"trace.log";
            string fullPath = String.Format(@"{0}\{1}", Directory.GetCurrentDirectory(), fileName);

            File.Delete(fileName);

            FileIOPermission fileIOPerm1 = new FileIOPermission(PermissionState.None);
            fileIOPerm1.SetPathList(FileIOPermissionAccess.Read, fullPath);
            fileIOPerm1.PermitOnly();

            try
            {
                FlatFileTraceListener listener = new FlatFileTraceListener(fileName, "---header---", "***footer***",
                    new TextFormatter("DUMMY{newline}DUMMY"));

                // need to go through the source to get a TraceEventCache
                LogSource source = new LogSource("notfromconfig", new[] { listener }, SourceLevels.All);
                source.TraceData(TraceEventType.Error, 0,
                    new LogEntry("message", "cat1", 0, 0, TraceEventType.Error, "title", null));
                listener.Dispose();
            }
            catch (SecurityException)
            {
                FileIOPermission.RevertAll();
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void FlatFileListenerReplacedEnviromentVariablesWillFallBackIfNotPrivilegesToRead()
        {
            var evidence = new Evidence();
            evidence.AddHostEvidence(new Zone(SecurityZone.Internet));
            var set = SecurityManager.GetStandardSandbox(evidence);
            set.AddPermission(new ReflectionPermission(ReflectionPermissionFlag.MemberAccess));
            set.RemovePermission(typeof(EnvironmentPermission));

            var domain = AppDomain.CreateDomain("partial trust", null, AppDomain.CurrentDomain.SetupInformation, set);

            try
            {
                domain.DoCallBack(CheckListener);
            }
            catch
            {
                throw;
            }
            finally
            {
                AppDomain.Unload(domain);
            }
        }

        public static void CheckListener()
        {
            string environmentVariable = "%USERPROFILE%";
            string fileName = Path.Combine(environmentVariable, "test.log");

            FlatFileTraceListener listener = new FlatFileTraceListener(fileName);
            listener.TraceData(new TraceEventCache(), "source", TraceEventType.Error, 1, "This is a test");
            listener.Dispose();
        }
    }
}
