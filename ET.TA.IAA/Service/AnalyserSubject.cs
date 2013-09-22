using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.TA.IAA.Service
{

    /// <summary>
    /// 分析器通知者
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
    public class AnalyserSubject : ISubject
    {
        private List<IService> observers = new List<IService>();
        #region ISubject 成员

        /// <summary>
        /// 增加观察者
        /// </summary>
        /// <param name="service"></param>
        public void Attach(IService service)
        {
            observers.Add(service);
        }

        /// <summary>
        /// 删除通知者
        /// </summary>
        /// <param name="service"></param>
        public void Detach(IService service)
        {
            observers.Remove(service);
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
                    item.StartService();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                //});
            }
        }

        #endregion
    }
}
