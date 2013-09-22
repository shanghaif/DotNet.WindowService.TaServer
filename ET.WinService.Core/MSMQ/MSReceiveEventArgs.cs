using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Messaging;

namespace ET.WinService.Core.MSMQ
{
    /// <summary>
    /// 接受消息处理事件
    /// </summary>
    public class MSReceiveEventArgs
    {
        private Message msg;

        /// <summary>
        /// 接受的消息
        /// </summary>
        public Message Msg
        {
            get
            {
                return this.msg;
            }
        }

        public object MsgBody
        {
            get
            {
                return this.msg.Body;
            }
        }

        private bool isAsyContinue;

        /// <summary>
        /// 接受消息后，是否再等待接受，在同步的情况下，此项无效
        /// </summary>
        public bool IsAsyContinue
        {
            get
            {
                return this.isAsyContinue;
            }
        }

        /// <summary>
        /// 接受消息处理事件
        /// </summary>
        /// <param name="message">消息</param>
        public MSReceiveEventArgs(Message message)
        {
            this.msg = message;
            this.isAsyContinue = false;
        }

        /// <summary>
        /// 接受消息处理事件
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="isAsyContinue">接受消息后，是否再等待接受，在同步的情况下，此项无效</param>
        public MSReceiveEventArgs(Message message, bool isAsyContinue)
        {
            this.msg = message;
            this.isAsyContinue = isAsyContinue;
        }
    }
}
