using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Data;
using ET.TA.IAA.DAL;
using ET.WinService.Core.Utility;
using System.Configuration;
namespace ET.TA.IAA.MonthService
{
    /// <summary>
    /// 执行车辆车度的月度报表统计，一个月执行一次
    /// add by chenjiongjiong 2013-04-27
    /// </summary>
    public class SpeedMonthService : IMonthService
    {
        MonthDao monthDao;
        public SpeedMonthService()
        {
            monthDao = new MonthDao();
        }
        private ILog log = LogManager.GetLogger(typeof(SpeedMonthService));
        public void StartCalculate()
        { 
           //运算当月的超速报表汇总数据
            DateTime NowTime = DateTime.Now;
            int NowYear = int.Parse(NowTime.ToString("yyyy"));
            int NowMonth = int.Parse(NowTime.ToString("MM"));
            log.Info("开始获取当月的月度车辆超速汇总数据");
            DataTable SpeedDt=monthDao.GetMonthSpeed(NowYear, NowMonth);
            log.Info("完成获取当月的月度车辆超速汇总数据");
            //删除当月的统计数据，目得是防止重复统计
            log.Info("开始删除当月现有的月度车辆超速汇总数据");
            monthDao.DeleteMonthSpeed(NowYear, NowMonth);
            log.Info("完成删除当月现有的月度车辆超速汇总数据");
            log.Info("开始插入最新的当月现有的月度车辆超速汇总数据");
            monthDao.UpdateMonthSpeed(SpeedDt);
            log.Info("结束插入最新的当月现有的月度车辆超速汇总数据");
        }
    }
}
