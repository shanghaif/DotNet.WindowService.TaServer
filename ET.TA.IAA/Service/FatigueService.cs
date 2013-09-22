using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ET.TA.IAA.DAL;
using ET.TA.IAA.Models;
using ET.TA.IAA.Analyser;
using log4net;

namespace ET.TA.IAA.Service
{

    /// <summary>
    /// 每天疲劳分析service
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
    public class FatigueService : IService
    {
        protected ILog log = LogManager.GetLogger(typeof(FatigueService));
        AnalyserDAO analyserDAO;
        IAnalyser<List<bi_iaa_vehicle_tired>> fatigueAnalyser;


        public FatigueService()
        {
            analyserDAO = new AnalyserDAO();
            fatigueAnalyser = new FatigueDrivingAnalyser();
        }

        #region IService 成员

        /// <summary>
        /// 启动分析器
        /// </summary>
        public void StartService()
        {
            log.Info("开始疲劳分析");
            List<bi_iaa_vehicle_tired> fatigueList = analyserDAO.GetANA_AnalyserTiredMap();
            if (fatigueList != null)
            {
                fatigueAnalyser.parser(fatigueList);
            }
            else
            {
                log.Info("疲劳数据为空");
            }
            log.Info("结束疲劳分析");
        }

        #endregion
    }
}
