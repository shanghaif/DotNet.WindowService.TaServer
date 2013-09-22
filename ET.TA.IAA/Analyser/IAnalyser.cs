using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ET.TA.IAA.Analyser
{

    /// <summary>
    /// 分析器接口
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
    /// <typeparam name="T"></typeparam>
    public interface IAnalyser<T>
    {
         void parser(T list);
    }
}
