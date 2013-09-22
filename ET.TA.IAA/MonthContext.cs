using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using ET.TA.IAA.MonthService;
namespace ET.TA.IAA
{
    public class MonthContext
    {
         protected ILog log = LogManager.GetLogger(typeof(TaIAAContext));
         IMonthSubject subject;

        public MonthContext()
        {
            subject = new MonthSubject();            //通知者
            subject.Attach(new SpeedMonthService());     //超速月度报表
            subject.Attach(new TirdMonthService());       //疲劳月度报表
         
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
