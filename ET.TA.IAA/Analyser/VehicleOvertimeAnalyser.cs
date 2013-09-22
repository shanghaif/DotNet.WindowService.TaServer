using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ET.TA.IAA.Models;

namespace ET.TA.IAA.Analyser
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
    public class VehicleOvertimeAnalyser:IAnalyser<List<bi_iaa_vehicle_overtime>>
    {
        public void parser(List<bi_iaa_vehicle_overtime> list)
        {
            if (list != null)
            {
                list.ForEach(item =>
                {
                    item.TotalTime = (int)(item.EndDatetime - item.StartDatetime).TotalSeconds;
                    item.Insert();
                    //SaveAlarm(item);
                });
            }
        }
    }
}
