using System;
using System.Collections.Generic;
using System.Text;
using ET.TA.IAA.Models;
using System.Linq;

using ET.WinService.Core.ActiveMQ;
using System.Configuration;
using log4net;

namespace ET.TA.IAA.Analyser
{

    /// <summary>
    /// 疲劳驾驶分析类
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
    public class FatigueDrivingAnalyser : IAnalyser<List<bi_iaa_vehicle_tired>>
    {
        protected ILog log = LogManager.GetLogger(typeof(FatigueDrivingAnalyser));
        #region IAnalyser<List<bi_iaa_vehicle_tired>> 成员

        public void parser(List<bi_iaa_vehicle_tired> list)
        {
            log.Info("开始分析疲劳驾驶：" + list.Count());
            if (list != null)
            {
                list.ForEach(item =>
                {
                    try
                    {
                        item.TotalMileage = item.EndMileage - item.StartMileage;
                        item.TotalOil = item.EndOil - item.StartOil;
                        item.TotalTime = (int)(item.EndDatetime - item.StartDatetime).TotalSeconds;
                        item.CreateDatetime = DateTime.Now;
                        item.Insert();
                        //SaveAlarm(item);
                    }
                    catch (Exception ex)
                    {

                        log.Error(ex);
                    }
                });
            }
        }

        /// <summary>
        /// 报警入库
        /// </summary>
        /// <param name="entity"></param>
        private void SaveAlarm(bi_iaa_vehicle_tired entity)
        {
            StringBuilder recordBuffer = new StringBuilder();

            recordBuffer.Append(entity.VehicleID)
                        .Append(",").Append(entity.PlateNumber)
                        .Append(",").Append(entity.PlateColor)
                        .Append(",").Append(entity.MotorcadeID)
                        .Append(",").Append(entity.MotorcadeName)
                        .Append(",").Append(entity.DriverICCode)
                        .Append(",").Append(entity.DriverName)
                        .Append(",").Append(entity.StartDatetime)
                        .Append(",").Append(entity.EndDatetime)
                        .Append(",").Append(entity.TotalTime)
                        .Append(",").Append(entity.StartLon)
                        .Append(",").Append(entity.StartLat)
                        .Append(",").Append(entity.StartPosition)
                        .Append(",").Append(entity.EndLon)
                        .Append(",").Append(entity.EndLat)
                        .Append(",").Append(entity.EndPosition)
                        .Append(",").Append(entity.TotalMileage)
                        .Append(",").Append(entity.StartOil)
                        .Append(",").Append(entity.EndOil)
                        .Append(",").Append(entity.TotalOil)
                        .Append(",").Append(entity.AlarmNum)
                        .Append(",").Append(entity.AnalyseConditions)
                        .Append(",").Append(entity.AnalyseObject)
                        .Append(",").Append(entity.ConditionsUserId)
                        .Append(",").Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //发送到MQ
            ActiveMQ mq = new ActiveMQ();
            mq.Start();
            mq.CreateProducer(true, ConfigurationManager.AppSettings["VehicleTiredSubject"]);
            mq.SendMQMessage(recordBuffer.ToString());
        }

        #endregion
    }
}
