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
    /// 超速分析
    /// </summary>
    /// <remarks>
    /// ------------------------------------------------------------------------------
    /// Copyright:Copyright (c) 2013,广州亿程交通信息有限公司 All rights reserved.
    /// 描  述：超速分析
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
    public class OverSpeedService : IService
    {
        protected ILog log = LogManager.GetLogger(typeof(OverSpeedService));
        AnalyserDAO analyserDAO;
        IAnalyser<List<bi_iaa_vehicle_speed>> overSpeedAnalyser;

        public OverSpeedService()
        {
            analyserDAO = new AnalyserDAO();
            overSpeedAnalyser = new OverSpeedDrivingAnalyser();
        }
        #region IService 成员

        public void StartService()
        {
            log.Info("开始超速分析");
            List<bi_iaa_vehicle_speed> speedList = analyserDAO.GetANA_AnalyserSpeedMap();
            if (speedList != null)
            {
                overSpeedAnalyser.parser(speedList);
            }
            else
            {
                log.Info("超速数据为空");
            }
            log.Info("结束超速分析");
        }

        #endregion
    }
}
