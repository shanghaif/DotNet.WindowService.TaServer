using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ET.TA.IAA.AddressService
{
    public class AddressSubject : IAddressSubject
    {
        private List<IAddressService> observers = new List<IAddressService>();
        /// <summary>
        /// 增加观察者
        /// </summary>
        /// <param name="service"></param>
        public void Attach(IAddressService service)
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
                    item.StartUpdateAddress();
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
