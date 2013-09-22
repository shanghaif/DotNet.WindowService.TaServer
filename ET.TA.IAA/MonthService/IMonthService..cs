using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ET.TA.IAA.MonthService
{
    public  interface IMonthService
    {
         /// <summary>
        /// 进行月度报表计算，具体重写此接口
        /// </summary>
        void StartCalculate();
    }
}
