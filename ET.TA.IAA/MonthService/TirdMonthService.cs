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
    /// 执行车辆疲劳的月度报表统计，一个月执行一次
    /// add by chenjiongjiong 2013-05-02
    /// </summary>
    public class TirdMonthService : IMonthService
    {
        private ILog log = LogManager.GetLogger(typeof(TirdMonthService));
        MonthDao monthDao;
        public TirdMonthService()
        {
            monthDao = new MonthDao();
        }
        public void StartCalculate()
        {
            //运算当月的疲劳报表汇总数据
            DateTime NowTime = DateTime.Now;
            int NowYear = int.Parse(NowTime.ToString("yyyy"));
            int NowMonth = int.Parse(NowTime.ToString("MM"));
            log.Info("开始获取当月的月度车辆疲劳汇总数据");
            DataTable SpeedDt = monthDao.GetMonthTird(NowYear, NowMonth);
            log.Info("完成获取当月的月度车辆疲劳汇总数据");
            //删除当月的统计数据，目得是防止重复统计
            log.Info("开始删除当月现有的月度车辆疲劳汇总数据");
            monthDao.DeleteMonthTird(NowYear, NowMonth);
            log.Info("完成删除当月现有的月度车辆疲劳汇总数据");
            log.Info("开始插入最新的当月现有的月度车辆疲劳汇总数据");
            monthDao.UpdateMonthTird(SpeedDt);
            log.Info("结束插入最新的当月现有的月度车辆疲劳汇总数据");
        }
    }
}
