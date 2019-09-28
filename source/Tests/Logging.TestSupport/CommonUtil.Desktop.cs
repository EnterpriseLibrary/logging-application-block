using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Microsoft.Practices.EnterpriseLibrary.Logging.TestSupport
{
    public partial class CommonUtil
    {
        public static void CreatePrivateTestQ()
        {
            string path = MessageQueuePath;
            if (MessageQueue.Exists(path))
            {
                DeletePrivateTestQ();
            }
            MessageQueue.Create(path, false);
        }

        public static void CreateTransactionalPrivateTestQ()
        {
            string path = MessageQueuePath;
            if (!MessageQueue.Exists(path))
            {
                MessageQueue.Create(path, true);
            }
        }

        public static void DeletePrivateTestQ()
        {
            string path = MessageQueuePath;
            if (MessageQueue.Exists(path))
            {
                MessageQueue.Delete(path);
            }
        }

        public static void ValidateMsmqIsRunning()
        {
            try
            {
                MessageQueue.Exists(MessageQueuePath);
            }
            catch (InvalidOperationException ex)
            {
                Assert.Inconclusive(ex.Message);
            }
        }

        public static int EventLogEntryCount()
        {
            using (EventLog log = new EventLog(EventLogName))
            {
                return log.Entries.Count - eventLogEntryCounter;
            }
        }

        public static int EventLogEntryCountCustom()
        {
            return GetCustomEventLog().Entries.Count - eventLogEntryCounterCustom;
        }

        public static int GetNumberOfMessagesOnQueue()
        {
            using (MessageQueue queue = new MessageQueue(MessageQueuePath))
            {
                Message[] messages = queue.GetAllMessages();
                return messages.Length;
            }
        }

        public static string GetLastEventLogEntry()
        {
            using (EventLog log = new EventLog(EventLogName))
            {
                return log.Entries[log.Entries.Count - 1].Message;
            }
        }

        public static string GetLastEventLogEntryCustom()
        {
            EventLog log = GetCustomEventLog();
            return log.Entries[log.Entries.Count - 1].Message;
        }

        public static CounterSample GetPerformanceCounterSample(string categoryName,
                                                        string instanceName,
                                                        string counterName)
        {
            using (PerformanceCounter counter = new PerformanceCounter())
            {
                counter.CategoryName = categoryName;
                counter.CounterName = counterName;
                counter.InstanceName = instanceName;
                return counter.NextSample();
            }
        }

        public static long GetPerformanceCounterValue(string categoryName,
                                                      string instanceName,
                                                      string counterName)
        {
            if (PerformanceCounterCategory.InstanceExists(instanceName, categoryName))
            {
                return GetPerformanceCounterSample(categoryName, instanceName, counterName).RawValue;
            }
            return 0;
        }

        public static bool LogEntryExists(string message)
        {
            // confirm listener started begin message written
            using (EventLog log = new EventLog(EventLogName))
            {
                string expected = message;
                string entry = log.Entries[log.Entries.Count - 1].Message;
                return (entry.IndexOf(expected) > -1);
            }
        }

        public static string ReadEventLogEntryBody()
        {
            using (EventLog log = new EventLog(EventLogName))
            {
                return log.Entries[log.Entries.Count - 1].Message;
            }
        }

        public static void ResetEventLogCounter()
        {
            using (EventLog log = new EventLog(EventLogName))
            {
                eventLogEntryCounter = log.Entries.Count;
            }
        }

        public static void ResetEventLogCounterCustom()
        {
            eventLogEntryCounterCustom = GetCustomEventLog().Entries.Count;
        }
    }
}
