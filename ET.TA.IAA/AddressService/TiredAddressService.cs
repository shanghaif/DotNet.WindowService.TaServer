using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Data;
using ET.TA.IAA.DAL;
using ET.WinService.Core.Utility;
using System.Configuration;

namespace ET.TA.IAA.AddressService
{
    /// <summary>
    /// 批量更新车辆疲劳的位置信息
    /// </summary>
    public class TiredAddressService : IAddressService
    {
        private ILog log = LogManager.GetLogger(typeof(SpeedAddressService));
        IaaDao iaaDao;
        string TcpIpAddress = ConfigurationManager.AppSettings["TcpIpAddress"].ToString();
        string TcpPort = ConfigurationManager.AppSettings["TcpPort"].ToString();
        DataTable DtTird;
        public TiredAddressService()
        {
            iaaDao = new IaaDao();
        }
     
        public void StartUpdateAddress()
        {
            log.Info("开始更新车辆疲劳的位置信息");
            //先获取前一天的超速iaa数据

            this.DtTird = iaaDao.GetIaaTired();

            //tcp
            TCPClientSocket tcpClient;
            tcpClient = new TCPClientSocket();
            tcpClient.IP = this.TcpIpAddress;
            tcpClient.PORT = int.Parse(this.TcpPort);
            tcpClient.ReciveHandelEvent += tcpClient_HandleTired;
            tcpClient.Connect();

            //遍历每一辆车，发送经纬度数据获取对应的地理位置
            string startLon = string.Empty;
            string startLat = string.Empty;
            string endLon = string.Empty;
            string endLat = string.Empty;
            string ID = string.Empty;
            //接收返回的数据
            tcpClient.ReceiveChatter();
            foreach (DataRow drTird in this.DtTird.Rows)
            {
                //发送指令
                startLon = drTird["StartLon"].ToString();
                startLat = drTird["StartLat"].ToString();
                endLon = drTird["EndLon"].ToString();
                endLat = drTird["EndLat"].ToString();
                ID = drTird["ID"].ToString();

                if (!string.IsNullOrEmpty(startLon) && !string.IsNullOrEmpty(startLat))
                {
                    tcpClient.NetSendData(string.Format("##{0},{1},{2}\r\n", ID, startLon, startLat));
                    //主线程休息200毫秒
                    System.Threading.Thread.Sleep(200);
                }

                if (!string.IsNullOrEmpty(endLon) && !string.IsNullOrEmpty(endLat))
                {
                    tcpClient.NetSendData(string.Format("##-{0},{1},{2}\r\n", ID, endLon, endLat));
                    //主线程休息200毫秒
                    System.Threading.Thread.Sleep(200);
                }
            }

            iaaDao.UpdateIaaTired(this.DtTird);
            log.Info("结束更新车辆疲劳的位置信息");
        }

        //根据经纬度订阅实际位置
        private void tcpClient_HandleTired(string basePackData)
        {
            string[] Arrayaddress = basePackData.Split('|');
            string Address = Arrayaddress[0].ToString();
            string ID = Arrayaddress[1].ToString();
            if (ID.StartsWith("-"))
            {
                ID = ID.Substring(1);
                DataRow[] Enddrs = this.DtTird.Select(string.Format("ID={0}", ID.Trim()));
                if (Enddrs.Length > 0)
                {
                    Enddrs[0]["EndPosition"] = Address;
                }
            }
            else
            {
                DataRow[] Startdrs = this.DtTird.Select(string.Format("ID={0}", ID.Trim()));
                if (Startdrs.Length > 0)
                {
                    Startdrs[0]["StartPosition"] = Address;
                }
            }

        }
    }
}
