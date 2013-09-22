using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ET.TA.IAA.Service
{
    /// <summary>
    /// 分析service接口
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// 分析方法，具体分析器重写这个方法
        /// </summary>
        void StartService();

    }
}
