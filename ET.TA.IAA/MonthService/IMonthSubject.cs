using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ET.TA.IAA.MonthService
{
    interface IMonthSubject
    {
        /// <summary>
        /// 增加观察者
        /// </summary>
        /// <param name="service"></param>
        void Attach(IMonthService service);

        /// <summary>
        /// 通知观察者
        /// </summary>
        void Notify();
    }
}
