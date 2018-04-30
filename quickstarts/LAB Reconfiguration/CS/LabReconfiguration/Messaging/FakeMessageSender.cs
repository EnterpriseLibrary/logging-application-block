﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Globalization;
using EnterpriseLibrary.Logging;

namespace LabReconfiguration.Messaging
{
    public class FakeMessageSender : IMessageSender
    {
        private Random randomFailure;

        public FakeMessageSender()
        {
            this.randomFailure = new Random();
        }

        public void SendMessage(string recipient, string message)
        {
            Logger.Write(string.Format(CultureInfo.CurrentCulture, "Sending message to '{0}': {1}", recipient, message), "Messaging", 0, 1, TraceEventType.Verbose);

            try
            {
                if (this.randomFailure.Next(5) == 0)
                {
                    throw new InvalidOperationException("Random error sending message");
                }

                Logger.Write(string.Format(CultureInfo.CurrentCulture, "Message sent to '{0}'", recipient), "Messaging", 0, 2, TraceEventType.Information);
            }
            catch (InvalidOperationException e)
            {
                Logger.Write(string.Format(CultureInfo.CurrentCulture, "Sending message to '{0}' failed: {1}", recipient, e), "Messaging", 0, 3, TraceEventType.Error);
                throw;
            }
        }
    }
}