using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ET.WinService.Core.MSMQ
{
    public class MSSendedEventArgs
    {
        private object msgContent;

        public object MsgContent
        {
            get
            {
                return this.msgContent;
            }
        }

        public MSSendedEventArgs(object msgContent)
        {
            this.msgContent = msgContent;
        }
    }
}
