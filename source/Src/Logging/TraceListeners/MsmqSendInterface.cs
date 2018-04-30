﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Messaging;
using System.Security;

namespace EnterpriseLibrary.Logging.TraceListeners
{
    /// <summary>
    /// Implementation of <see cref="MsmqSendInterface"/> that forwards messages to an actual MSMQ.
    /// </summary>
    [SecurityCritical]
    internal class MsmqSendInterface : IMsmqSendInterface
    {
        private MessageQueue messageQueue;

        internal MsmqSendInterface(string queuePath)
        {
            messageQueue = new MessageQueue(queuePath, false, true);
        }

        /// <summary>
        /// Closes the underlying MSMQ.
        /// </summary>
        [SecurityCritical]
        public void Close()
        {
            messageQueue.Close();
        }

        /// <summary>
        /// Disposes the underlying MSMQ.
        /// </summary>
        [SecuritySafeCritical]
        public void Dispose()
        {
            messageQueue.Dispose();
        }

        /// <summary>
        /// Sends a message to the underlying MSMQ.
        /// </summary>
        /// <param name="message">The <see cref="Message"/> to send.</param>
        /// <param name="transactionType">The <see cref="MessageQueueTransactionType"/> value that specifies the type of transaction to use.</param>
        [SecurityCritical]
        public void Send(Message message, MessageQueueTransactionType transactionType)
        {
            messageQueue.Send(message, transactionType);
        }

        /// <summary>
        /// Returns the transactional status of the underlying MSMQ.
        /// </summary>
        public bool Transactional
        {
            [SecurityCritical]
            get { return messageQueue.Transactional; }
        }
    }
}
