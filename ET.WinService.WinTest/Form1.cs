using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ET.TA.IAA.AddressService;
using ET.TA.IAA.Service;
using ET.TA.IAA.MonthService;
namespace ET.WinService.WinTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SpeedMonthService aa = new SpeedMonthService();
            //aa.StartCalculate();
            //FatigueService service = new FatigueService();
            //service.StartService();
            TirdMonthService tiredMonthService = new TirdMonthService();
            tiredMonthService.StartCalculate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TirdMonthService tiredMonthService = new TirdMonthService();
            tiredMonthService.StartCalculate();
        }
    }
}
