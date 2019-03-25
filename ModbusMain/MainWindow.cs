using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using Modbus;

namespace ModbusMain
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private int number = 0;

        public void ModbusTest()
        {
            Random ran = new Random();
            int val = ran.Next(3);
            byte[] res = ModbusHelper.SendTrainCyclostyle(number, ModbusHelper.MB_WRITE_MULTIPLE_REGS, 0, 1, new byte[] { (byte)(val >> 8), (byte)(val & 0xFF) });
            string str = string.Empty;
            for (int i = 0; i < res.Length; i++)
            {
                str += res[i].ToString("X2") + " ";
            }
            txt_send.Text = string.Format("Tx:{0}-{1}", number.ToString("D3"), str);
            number++;
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            ModbusTest();
        }
    }
}
