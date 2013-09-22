using System;
using System.Collections.Generic;
using System.Web;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Text;
using log4net;
namespace ET.WinService.Core.Utility
{
    public class TCPClientSocket
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TCPClientSocket));
        /// <summary>
        /// 向服务器连接的套接字实体
        /// </summary>
        private TcpClient ConnAgent;

        /// <summary>
        /// GPS定位数据接收
        /// </summary>
        /// <param name="gpsdata">GPS定位数据结构体</param>
        public delegate void HandleGPSData(string realTimeDataRecordTrack);

        /// <summary>
        /// TCP发送流
        /// </summary>
        private NetworkStream Ns;

        /// <summary>
        /// 接收GPS数据事件
        /// </summary>
        public event HandleGPSData ReciveHandelEvent;

        /// <summary>
        /// 接收线程
        /// </summary>
        private Thread Receive = null;

        private bool isAlive;
        /// <summary>
        /// 是否已经连接
        /// </summary>
        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IP;
        /// <summary>
        /// 端口号
        /// </summary>
        public int PORT;
        public void Connect()
        {
            if (!IsAlive)
            {

                try
                {
                    Disconnect();
                    log.Info("正在建立与WEB直连数据的连接！");
                    ConnAgent = new TcpClient();
                    IPAddress address = IPAddress.Parse(IP);
                    ConnAgent.Connect(address, PORT);
                    Ns = ConnAgent.GetStream();
                    log.Info("与服务器的连接建立成功！");
                }
                catch (Exception ex)
                {
                    IsAlive = false;
                    log.Info("不能建立与服务器" + IP.Trim() + ":" + PORT .ToString()+ "的连接！产生异常:\r\n" + ex.ToString());
                    Disconnect();
                }

            }
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        public void ReceiveChatter()
        {
            Receive = new Thread(new ThreadStart(ReceiveThread));
            Receive.IsBackground = true;
            Receive.Name = "ReceiveCTS";
            IsAlive = true;
            Receive.Start();
          
        }
        /// <summary>
        /// 服务端数据接处理，作为单独地一个线程执行，接收服务器的下发的数据
        /// </summary>
        private void ReceiveThread()
        {
            string RecvStr = "";
            //完整数据包
            string DataBody;
            string Chatter = "";
            int Bytes = 0;
            Byte[] buffer = new Byte[1024];

            while (IsAlive)
            {
                try
                {
                    if (Ns != null)
                        //断开连接时 读取缓冲区数据会报错
                        Bytes = Ns.Read(buffer, 0, buffer.Length);
                }
                catch (Exception ex)
                {
                    buffer = null;

                    IsAlive = false;
                    log.Info(string.Format("与WEB直连代理的连接发生故障！ReceiveChatter()\r\n产生异常为： {0}",
                                                 ex.ToString()));

                    Disconnect();

                    break;
                }

                try
                {
                    if (Bytes == 0)
                    {
                        IsAlive = false;
                        buffer = null;
                        log.Info("(B)与WEB直连代理的连接发生故障！ReceiveChatter()\r\n");
                        Disconnect();
                        break;
                    }
                    else
                    {
                        //提取缓冲区数据
                        Chatter = RecvStr + Encoding.Default.GetString(buffer, 0, Bytes);

                        //若数据为空则继续重新读取缓冲区数据
                        if (Chatter == "")
                        {
                            continue;
                        }

                        DataBody = Chatter;
                        //非空数据才处理
                        if (!DataBody.Equals(""))
                        {
                            AnalyseChartter(DataBody);
                        }

                        //}
                    }
                }
                catch (System.Exception ex)
                {
                    log.Info(string.Format("ReceiveChatter()--- 产生异常为：{0}\r\n", ex.ToString()));
                }
            }
        }
        /// <summary>
        /// 分析处理数据体，激活事件
        /// </summary>
        /// <param name="sDataBody">完整数据包</param>
        private void AnalyseChartter(string sDataBody)
        {
            string basePackData;

            try
            {

                basePackData = sDataBody;

                if (null != ReciveHandelEvent)
                {
                    ReciveHandelEvent(basePackData);
                };
            }
            catch (System.Exception ex)
            {
                //MessageEvent(Format "GPS数据接收分析错误:{0}", 0);
            };
        }
        public void Disconnect()
        {
            try
            {
                if (Ns != null)
                {
                    Ns.Close();
                    Ns = null;
                }
                if (ConnAgent != null)
                {
                    ConnAgent.Close();
                    ConnAgent = null;
                }

                if (Receive != null)
                    Receive = null;

                isAlive = false;

                try
                {
                    //销毁读取数据线程
                    Receive.Abort();
                }
                catch (ThreadAbortException ex)
                {
                    Receive.Abort();
                }

            }
            catch
            {
            }
        }
        /// <summary>
        /// 发送指令
        /// </summary>
        /// <param name="SendData"></param>
        /// <returns>发送成功返回 true 发送失败返回 false </returns>
        public bool NetSendData(string SendData)
        {
            try
            {
                //注意：SendData 没有包括包头## 包尾 \r\n
                byte[] OUT = Encoding.Default.GetBytes(SendData);

                //只给一次连接机会，后交给连接管理器
                if (Ns != null)
                    Ns.Write(OUT, 0, OUT.Length);
                else
                {
                    Connect();
                    Ns.Write(OUT, 0, OUT.Length);
                }
                return true;
            }
            catch (System.Exception ex)
            {
                log.Info(string.Format("发送指令产生异常，如下：{0}", ex.ToString()));
                return false;
            }
        }
    }
}