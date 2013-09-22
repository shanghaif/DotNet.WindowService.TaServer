using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ET.TA.IAA.MonthService
{
    public class MonthSubject:IMonthSubject
    {
        private List<IMonthService> observers = new List<IMonthService>();
        /// <summary>
        /// 增加观察者
        /// </summary>
        /// <param name="service"></param>
        public void Attach(IMonthService service)
        {
            observers.Add(service);
        }



        /// <summary>
        /// 通知观察者
        /// </summary>
        public void Notify()
        {
            foreach (var item in observers)
            {
                //开启一个任务
                // Task.Factory.StartNew(() =>
                // {
                try
                {
                    item.StartCalculate();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                //});
            }
        }
    }
}
