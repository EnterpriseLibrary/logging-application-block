﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Messaging;
using System.Security.Principal;
using EnterpriseLibrary.Logging.Filters;
using EnterpriseLibrary.Logging.Formatters;
using EnterpriseLibrary.Logging.MsmqDistributor.Instrumentation;
using EnterpriseLibrary.Logging.MsmqDistributor.Tests.Properties;
using EnterpriseLibrary.Logging.TestSupport;
using EnterpriseLibrary.Logging.TestSupport.TraceListeners;
using EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseLibrary.Logging.MsmqDistributor.Tests
{
    [TestClass]
    public class MsmqLogDistributorFixture
    {
        MsmqLogDistributor msmqDistributor;
        LogSource clientSource;
        DistributorEventLogger eventLogger;

        [TestInitialize]
        public void SetUp()
        {
            CommonUtil.ValidateMsmqIsRunning();
            CommonUtil.DeletePrivateTestQ();
            CreateQueueForTesting();

            var msmqTraceListener = new MsmqTraceListener("unnamed", CommonUtil.MessageQueuePath, new BinaryLogFormatter(),
                                      MessagePriority.Normal, false, new TimeSpan(0, 1, 0), new TimeSpan(0, 1, 0),
                                      false, true, false, MessageQueueTransactionType.None);

            clientSource = new LogSource("unnamed", new[] { msmqTraceListener }, SourceLevels.All);

            LogSource distributorSource = new LogSource("unnamed", new[] { new MockTraceListener() }, SourceLevels.All);

            Dictionary<string, LogSource> traceSources = new Dictionary<string, LogSource>();
            Logger.SetLogWriter(new LogWriter(new List<ILogFilter>(), traceSources, distributorSource, null, new LogSource("errors"), "default", false, false), false);
            eventLogger = new DistributorEventLogger();
            msmqDistributor = new MsmqLogDistributor(CommonUtil.MessageQueuePath, eventLogger);
            msmqDistributor.StopReceiving = false;
        }

        protected virtual void CreateQueueForTesting()
        {
            CommonUtil.CreatePrivateTestQ();
        }

        [TestCleanup]
        public void TearDown()
        {
            CommonUtil.DeletePrivateTestQ();
            MockTraceListener.Reset();
            Logger.Reset();
        }

        [TestMethod]
        public void Constructor()
        {
            msmqDistributor = new MsmqLogDistributor(CommonUtil.MessageQueuePath, new DistributorEventLogger());

            Assert.IsNotNull(msmqDistributor);
        }

        [TestMethod]
        public void ReceiveMSMQMessage()
        {
            SendMessageToQ(CommonUtil.MsgBody);

            msmqDistributor.CheckForMessages();

            Assert.AreEqual(1, MockTraceListener.Entries.Count);
            Assert.AreEqual(CommonUtil.MsgBody, MockTraceListener.LastEntry.Message, "Body");
        }

        [TestMethod]
        public void ReceiveTwoMessages()
        {
            SendMessageToQ(CommonUtil.MsgBody);
            SendMessageToQ(CommonUtil.MsgBody + " 4 5 6");

            Assert.AreEqual(2, CommonUtil.GetNumberOfMessagesOnQueue());

            msmqDistributor.CheckForMessages();

            Assert.AreEqual(0, CommonUtil.GetNumberOfMessagesOnQueue());

            // confirm that the second message was processed by the sink
            Assert.AreEqual(2, MockTraceListener.Entries.Count);
            Assert.AreEqual(CommonUtil.MsgBody + " 4 5 6", MockTraceListener.LastEntry.Message);
        }

        [TestMethod]
        public void SendTwoMessagesWithPauseReceiving()
        {
            SendMessageToQ(CommonUtil.MsgBody);
            SendMessageToQ(CommonUtil.MsgBody + " 4 5 6");

            // By setting StopRecieving = true, only one message will be processed from the Q
            msmqDistributor.StopReceiving = true;
            msmqDistributor.CheckForMessages();

            // confirm that the second message was NOT processed by the sink
            Assert.AreEqual(1, MockTraceListener.Entries.Count);
            Assert.AreEqual(CommonUtil.MsgBody, MockTraceListener.LastEntry.Message);

            msmqDistributor.CheckForMessages();
        }

        [TestMethod]
        public void SendCustomLogEntryViaMsmq()
        {
            CustomLogEntry log = new CustomLogEntry();
            log.TimeStamp = DateTime.MaxValue;
            log.Title = "My custom message title";
            log.Message = "My custom message body";
            log.Categories = new string[] { "CustomFormattedCategory" };
            log.AcmeCoField1 = "apple";
            log.AcmeCoField2 = "orange";
            log.AcmeCoField3 = "lemon";

            clientSource.TraceData(TraceEventType.Information, 1, log);

            msmqDistributor.CheckForMessages();

            //string expected = "Timestamp: 12/31/9999 11:59:59 PM\r\nTitle: My custom message title\r\n\r\nAcme Field1: apple\r\nAcme Field2: orange\r\nAcme Field3: lemon\r\n\r\nMessage: My custom message body";
            Assert.IsFalse(MockTraceListener.LastEntry == log);
            Assert.AreEqual(MockTraceListener.LastEntry.Message, log.Message);
            Assert.AreEqual(((CustomLogEntry)MockTraceListener.LastEntry).AcmeCoField1, log.AcmeCoField1);
            Assert.AreEqual(((CustomLogEntry)MockTraceListener.LastEntry).AcmeCoField2, log.AcmeCoField2);
            Assert.AreEqual(((CustomLogEntry)MockTraceListener.LastEntry).AcmeCoField3, log.AcmeCoField3);
        }

        [TestMethod]
        public void SendLogEntryViaMsmq()
        {
            LogEntry log = new LogEntry();
            log.TimeStamp = DateTime.MaxValue;
            log.Title = "My custom message title";
            log.Message = "My custom message body";
            log.Categories = new string[] { "FormattedCategory" };

            clientSource.TraceData(TraceEventType.Information, 1, log);

            msmqDistributor.CheckForMessages();

            Assert.IsFalse(MockTraceListener.LastEntry == log);
            Assert.AreEqual(MockTraceListener.LastEntry.Message, log.Message);
        }

        [TestMethod]
        public void SendLogEntryWithDictionaryViaMsmq()
        {
            LogEntry log = new LogEntry();
            log.TimeStamp = DateTime.MaxValue;
            log.Title = "My custom message title";
            log.Message = "My custom message body";
            log.Categories = new string[] { "AppTest" };
            log.ExtendedProperties = CommonUtil.GetPropertiesDictionary();

            clientSource.TraceData(TraceEventType.Information, 1, log);

            msmqDistributor.CheckForMessages();

            Assert.IsFalse(MockTraceListener.LastEntry == log);
            Assert.AreEqual(MockTraceListener.LastEntry.Message, log.Message);
            Assert.AreEqual(MockTraceListener.LastEntry.ExtendedProperties.Count, log.ExtendedProperties.Count);
            foreach (string key in log.ExtendedProperties.Keys)
            {
                Assert.AreEqual(MockTraceListener.LastEntry.ExtendedProperties[key], log.ExtendedProperties[key]);
            }
        }

        [TestMethod]
        public void SendDictionaryWithNestedInvalidXml()
        {
            LogEntry log = new LogEntry();
            log.TimeStamp = DateTime.MaxValue;
            log.Title = "My custom message title";
            log.Message = "My custom message body";
            log.Categories = new string[] { "DictionaryCategory" };
            Dictionary<string, object> hash = new Dictionary<string, object>();
            hash["key1"] = "value1";
            hash["key2"] = "<xml>my values<field1>INVALID ><><XML</field2></xml>";
            hash["key3"] = "value3";
            log.ExtendedProperties = hash;

            clientSource.TraceData(TraceEventType.Information, 1, log);

            msmqDistributor.CheckForMessages();

            Assert.IsFalse(MockTraceListener.LastEntry == log);
            Assert.AreEqual(MockTraceListener.LastEntry.Message, log.Message);
            Assert.AreEqual(MockTraceListener.LastEntry.ExtendedProperties.Count, log.ExtendedProperties.Count);
            foreach (string key in log.ExtendedProperties.Keys)
            {
                Assert.AreEqual(MockTraceListener.LastEntry.ExtendedProperties[key], log.ExtendedProperties[key]);
            }
        }

        [TestMethod]
        public void MsmqAccessDenied()
        {
            MsmqReceiverTestWrapper testSync = new MsmqReceiverTestWrapper(CommonUtil.MessageQueuePath, eventLogger);
            testSync.LogMsgQueueException(MessageQueueErrorCode.AccessDenied);

            string expected = string.Format(Resources.MsmqAccessDenied, CommonUtil.MessageQueuePath, WindowsIdentity.GetCurrent().Name);
            string actual = CommonUtil.GetLastEventLogEntry();
            Assert.IsTrue(actual.IndexOf(expected) > -1);
        }

        [TestMethod]
        public void IllFormattedMessageWritesInEventLog()
        {
            MsmqReceiverTestWrapper testSync = new MsmqReceiverTestWrapper(CommonUtil.MessageQueuePath, eventLogger);
            MsmqTraceListener mqTracelistener = new MsmqTraceListener("unnamed", CommonUtil.MessageQueuePath, new BinaryLogFormatter(),
                                                                      MessagePriority.Normal, false, new TimeSpan(0, 1, 0), new TimeSpan(0, 1, 0),
                                                                      false, true, false, MessageQueueTransactionType.None);
            mqTracelistener.Write("this is a plain trace message");
            try
            {
                testSync.CheckForMessages();
            }
            catch (LoggingException) { }

            string eventlogEntry = CommonUtil.GetLastEventLogEntry();
            Assert.IsTrue(-1 != eventlogEntry.IndexOf("Unable to deserialize message"));
        }

        void SendMessageToQ(string body)
        {
            //submit msg to queue
            LogEntry logEntry = CommonUtil.GetDefaultLogEntry();
            logEntry.Categories = new string[] { "MockCategoryOne" };
            logEntry.Message = body;
            logEntry.Severity = TraceEventType.Information;

            clientSource.TraceData(logEntry.Severity, 1, logEntry);
        }
    }

    class MsmqReceiverTestWrapper : MsmqLogDistributor
    {
        public MsmqReceiverTestWrapper(string msmqPath, DistributorEventLogger eventLogger)
            : base(msmqPath, eventLogger) { }

        public void LogMsgQueueException(MessageQueueErrorCode code)
        {
            // a better approach would be to derive a new Exception type from
            // MessagequeueException and then throw it using a mock MessageQueue
            // However, this exception is protected and we cannot created a derived type
            base.LogMessageQueueException(code, new Exception("simulated exception"));
        }
    }
}
