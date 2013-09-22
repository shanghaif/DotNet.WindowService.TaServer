using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ET.TA.IAA.DAL;
using ET.TA.IAA.Models;
using ET.TA.IAA.Analyser;

namespace ET.TA.IAA.Service
{

    /// <summary>
    /// 地点超时
    /// </summary>
    /// <remarks>
    /// ------------------------------------------------------------------------------
    /// Copyright:Copyright (c) 2013,广州亿程交通信息有限公司 All rights reserved.
    /// 描  述：
    /// 版本号：1.0.0.1
    /// 作  者：黄冠群 (hgq@e-trans.com.cn)
    /// 日  期：2013年4月17日
    /// 修  改：
    /// 原  因：
    /// ------------------------------------------------------------------------------
    /// </remarks>
    /// <example>
    /// [示例代码在这里写入]
    /// </example>
    public class VehicleOvertimeService:IService
    {
        AnalyserDAO analyserDAO;
        IAnalyser<List<bi_iaa_vehicle_overtime>> vehicleOvertimeAnalyser;


        public VehicleOvertimeService()
        {
            analyserDAO = new AnalyserDAO();
            vehicleOvertimeAnalyser = new VehicleOvertimeAnalyser();
        }

        #region IService 成员

        /// <summary>
        /// 启动分析器
        /// </summary>
        public void StartService()
        {
            List<bi_iaa_vehicle_overtime> list = analyserDAO.GetVehicleOvertime();
            if (list != null)
            {
                vehicleOvertimeAnalyser.parser(list);
            }
        }

        #endregion
    }
}
