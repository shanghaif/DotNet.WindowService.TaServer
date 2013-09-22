using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ET.TA.IAA.AddressService
{
    public interface IAddressService
    {
        /// <summary>
        /// 根据经纬度获取地址的方法，具体重写此接口
        /// </summary>
        void StartUpdateAddress();
    }
}
