using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

using ET.TA.IAA.Service;

namespace ET.TA.IAA
{

    /// <summary>
    /// IAA上下文
    /// </summary>
    /// <remarks>
    /// ------------------------------------------------------------------------------
    /// Copyright:Copyright (c) 2013,广州亿程交通信息有限公司 All rights reserved.
    /// 描  述：
    /// 版本号：1.0.0.1
    /// 作  者：黄冠群 (hgq@e-trans.com.cn)
    /// 日  期：2013年3月6日
    /// 修  改：
    /// 原  因：
    /// ------------------------------------------------------------------------------
    /// </remarks>
    /// <example>
    /// [示例代码在这里写入]
    /// </example>
    public class TaIAAContext
    {
        protected ILog log = LogManager.GetLogger(typeof(TaIAAContext));
        ISubject subject;

        public TaIAAContext()
        {
            subject = new AnalyserSubject();            //通知者
            subject.Attach(new OverSpeedService());     //超速分析
            subject.Attach(new FatigueService());       //疲劳驾驶
           // subject.Attach(new AreaInoutService());     //区域进出
            //subject.Attach(new LocaleInoutService());   //地点进出
           // subject.Attach(new VehicleOvertimeService()); //地点超时
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
