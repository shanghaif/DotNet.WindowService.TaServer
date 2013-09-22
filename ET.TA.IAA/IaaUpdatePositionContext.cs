using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using ET.TA.IAA.AddressService;
namespace ET.TA.IAA
{
    
    public class IaaUpdatePositionContext
    {
        protected ILog log = LogManager.GetLogger(typeof(IaaUpdatePositionContext));
        IAddressSubject subject;
        public IaaUpdatePositionContext()
        {
            subject = new AddressSubject();            //通知者
            subject.Attach(new SpeedAddressService());     //超速地理位置更新
            subject.Attach(new TiredAddressService());       //疲劳地理位置更新
        }
        public void Run()
        {
            try
            {
                subject.Notify();
            }
            catch (Exception ex)
            {

                log.Error(ex);
            }
        }
    }
}
