using System;
using System.Messaging;

namespace ET.WinService.Core.MSMQ
{
    /// <summary>
    /// 接受消息事件的代理
    /// </summary>
    /// <param name="src"></param>
    /// <param name="e"></param>
    public delegate void MSReceivedEventHandler(object src, MSReceiveEventArgs e);

    /// <summary>
    /// 消息发出后事件的代理
    /// </summary>
    /// <param name="src"></param>
    /// <param name="e"></param>
    public delegate void MSSendedEventHandler(object src, MSSendedEventArgs e);

    /// <summary>
    /// 用MSMQ发消息的消息类
    /// </summary>
    public class MSQueue
    {
        protected MessageQueueTransactionType transactionType = MessageQueueTransactionType.Automatic;
        protected MessageQueue queue;
        protected TimeSpan timeout;
        protected bool isAsyContinue = false;

        /// <summary>
        /// 接受消息后，是否再等待接受，在同步的情况下，此项无效
        /// </summary>
        public bool IsAsyContinue
        {
            get
            {
                return this.isAsyContinue;
            }
            set
            {
                this.isAsyContinue = value;
            }
        }


        /// <summary>
        /// 接受消息后处理的事件
        /// </summary>
        public event MSReceivedEventHandler OnMSReceived;

        /// <summary>
        /// 消息发出后的事件
        /// </summary>
        public event MSSendedEventHandler OnMSSended;

        /// <summary>
        /// 创建消息类
        /// </summary>
        /// <param name="queuePath">消息路径</param>
        /// <param name="timeoutSeconds">消息等待时间</param>
        /// <param name="formatter">消息格式</param>
        public MSQueue(string queuePath, int timeoutSeconds)
        {
            IMessageFormatter formatter = new BinaryMessageFormatter();

            if (MessageQueue.Exists(queuePath))
            {
                this.queue = new MessageQueue(queuePath);
            }
            else
            {
                this.queue = MessageQueue.Create(queuePath);
            }
            this.timeout = TimeSpan.FromSeconds(Convert.ToDouble(timeoutSeconds));

            queue.Formatter = formatter;

            // Performance optimization since we don't need these features
            queue.DefaultPropertiesToSend.AttachSenderId = false;
            queue.DefaultPropertiesToSend.UseAuthentication = false;
            queue.DefaultPropertiesToSend.UseEncryption = false;
            queue.DefaultPropertiesToSend.AcknowledgeType = AcknowledgeTypes.None;
            queue.DefaultPropertiesToSend.UseJournalQueue = false;
        }

        /// <summary>
        /// 创建消息类
        /// </summary>
        /// <param name="queuePath">消息路径</param>
        /// <param name="timeoutSeconds">消息等待时间</param>
        /// <param name="formatter">消息格式</param>
        public MSQueue(string queuePath, int timeoutSeconds, IMessageFormatter formatter)
        {
            if (MessageQueue.Exists(queuePath))
            {
                this.queue = new MessageQueue(queuePath);
            }
            else
            {
                this.queue = MessageQueue.Create(queuePath);
            }
            this.timeout = TimeSpan.FromSeconds(Convert.ToDouble(timeoutSeconds));

            queue.Formatter = formatter;

            // Performance optimization since we don't need these features
            queue.DefaultPropertiesToSend.AttachSenderId = false;
            queue.DefaultPropertiesToSend.UseAuthentication = false;
            queue.DefaultPropertiesToSend.UseEncryption = false;
            queue.DefaultPropertiesToSend.AcknowledgeType = AcknowledgeTypes.None;
            queue.DefaultPropertiesToSend.UseJournalQueue = false;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="messageObj">消息内容</param>
        public void Send(object messageObj)
        {
            Message m = new Message(messageObj, this.queue.Formatter);
            SendMessage(messageObj, m);
            m = null;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="messageObj">消息内容</param>
        /// <param name="lable">标签</param>
        public void Send(object messageObj, string lable)
        {
            Message m = new Message(messageObj, this.queue.Formatter);
            m.Label = lable;
            SendMessage(messageObj, m);
            m = null;
        }

        /// <summary>
        /// 执行发送消息
        /// </summary>
        /// <param name="messageObj">消息内容</param>
        /// <param name="m">消息</param>
        private void SendMessage(object messageObj, Message m)
        {
            this.queue.Send(m);
            if (this.OnMSSended != null)
            {
                this.OnMSSended(this, new MSSendedEventArgs(messageObj));
            }
        }

        /// <summary>
        /// 同步接受消息
        /// </summary>
        public void Receive()
        {
            Message m = this.queue.Receive();
            if (this.OnMSReceived != null)
            {
                this.OnMSReceived(this, new MSReceiveEventArgs(m));
            }
        }

        /// <summary>
        /// 异步接受消息
        /// </summary>
        public void RecevieAsy()
        {
            this.queue.ReceiveCompleted += new ReceiveCompletedEventHandler(this.ReceivedAsyEvt); //事件
            this.queue.BeginReceive();
        }

        /// <summary>
        /// ReceiveCompleted事件的实现
        /// </summary>
        /// <param name="source"></param>
        /// <param name="asyncResult"></param>
        protected virtual void ReceivedAsyEvt(object source, ReceiveCompletedEventArgs asyncResult)
        {
            try
            {
                //异步接受消息
                Message m = this.queue.EndReceive(asyncResult.AsyncResult);

                //在此插入处理消息的代码 
                if (this.OnMSReceived != null)
                {
                    this.OnMSReceived(this, new MSReceiveEventArgs(m, this.isAsyContinue));
                }

                m = null;
            }
            catch (MessageQueueException ex)
            {

            }
            finally
            {
                if (this.isAsyContinue)
                {
                    this.queue.BeginReceive();//接收下一次事件
                }
            }
        }
    }
}
