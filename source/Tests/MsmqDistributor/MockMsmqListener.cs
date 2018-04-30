﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using EnterpriseLibrary.Logging.MsmqDistributor;

namespace EnterpriseLibrary.Logging.MsmqDistributor.Tests
{
    internal class MockMsmqListener : MsmqListener
    {
        public bool ExceptionOnStart = false;
        public bool ExceptionOnStop = false;
        public bool StopReturnsFalse = false;

        public bool StartCalled = false;
        public bool StopCalled = false;

        public MockMsmqListener(DistributorService logDistributor, int timerInterval, string msmqPath)
            : base(logDistributor, timerInterval, msmqPath)
        {
        }

        public override void StartListener()
        {
            StartCalled = true;
            if (ExceptionOnStart)
            {
                throw new Exception("simulated exception");
            }
        }

        public override bool StopListener()
        {
            StopCalled = true;
            if (ExceptionOnStop)
            {
                throw new Exception("simulated exception");
            }
            if (StopReturnsFalse)
            {
                return false;
            }
            return true;
        }
    }
}
