using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ET.TA.IAA.Models;

/// <summary>
/// 区域进出分析类
/// </summ
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
/// </example>ary>
/// <param name="list"></param>
namespace ET.TA.IAA.Analyser
{

    public class AreaInoutAnalyser : IAnalyser<List<bi_hta_area_inout>>
    {
        private List<bi_iaa_area_inout> iaaList;

        public AreaInoutAnalyser()
        {
            iaaList = new List<bi_iaa_area_inout>();
        }

        public void parser(List<bi_hta_area_inout> list)
        {
            SetList(list);
            if (iaaList != null)
            {
                iaaList.ForEach(item =>
                {
                    item.TotalTime = (int)(item.OutDatetime - item.IntoDatetime).TotalSeconds;
                    //item.InotPosition=
                    //item.OutPosition=
                    item.Insert();
                    //SaveAlarm(item);
                });
            }
        }

        /// <summary>
        /// 返回iaa列表
        /// </summary>
        /// <param name="list"></param>
        private void SetList(List<bi_hta_area_inout> list)
        {
            //1:进入 
            foreach (var item in list.Where(o => o.ActionState == 1))
            {
                iaaList.Add(new bi_iaa_area_inout()
                {
                    AnalyseConditions = item.AnalyseConditions,
                    ConditionsUserId = item.ConditionsUserId,
                    CoverageID = item.CoverageID,
                    CreateDatetime = DateTime.Now,
                    DriverICCode = item.DriverICCode,
                    DriverName = item.DriverName,
                    IntoDatetime = item.ActionDatetime,
                    IntoLat = item.ActionLat,
                    IntoLon = item.ActionLon,
                    IntoMileage = item.ActionMileage,
                    IntoOil = item.ActionOil,
                    MotorcadeID = item.MotorcadeID,
                    MotorcadeName = item.MotorcadeName,
                    PlateColor = item.PlateColor,
                    PlateNumber = item.PlateNumber,
                    VehicleID = item.VehicleID
                });
            }

            //2：离开
            foreach (var item in list.Where(o => o.ActionState == 2))
            {
                var temp = iaaList.FirstOrDefault(o => o.VehicleID == item.VehicleID);
                if (temp != null)
                {
                    temp.OutDatetime = item.ActionDatetime;
                    temp.OutLat = item.ActionLat;
                    temp.OutLon = item.ActionLon;
                    temp.OutMileage = item.ActionMileage;
                    temp.OutOil = item.ActionOil;
                }
            }
        }
    }
}
