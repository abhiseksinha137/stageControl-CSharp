using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace stageControl_CSharp
{
    public partial class Form1 : Form
    {
        private SerialPort com;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void sendSerial(string command)
        {
            try {
                com.WriteLine(command);
                writeLog(command);
            }
            catch (Exception ex)
            {
                writeLog(ex.Message);
            }
        }
        private void writeLog(string log)
        {
            txtbxLog.Text = txtbxLog.Text + log + Environment.NewLine;
        }

        private void connectCOM() {
            try
            {
                com = new SerialPort(comboBox1.Text, 9600, Parity.None, 8, StopBits.One);
                btnDiscon.Enabled = true;
                writeLog("Stage Connected!");
            }
            catch (Exception ex)
            {
                writeLog("Stage not found!");
            }
        }

        private void closeCOM()
        {
            try
            {
                com.Close();
                btnDiscon.Enabled = false;
                writeLog("Stage Disconnected!");
            }
            catch (Exception ex)
            {
                writeLog(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            connectCOM();
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            comboBox1.Items.Clear();
            foreach (string port in ports)
            {
                comboBox1.Items.Add(port);
            }
        }

        private void btnDiscon_Click(object sender, EventArgs e)
        {
            closeCOM();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeCOM();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string tag = timer1.Tag.ToString();
            sendSerial(tag);
            //writeLog(tag);
        }

        private void btnFwd_Click(object sender, EventArgs e)
        {
            sendSerial("F");
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            sendSerial("R");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            sendSerial("B");
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            sendSerial("L");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (AboutBox1 box = new AboutBox1())
            {
                box.ShowDialog(this);
            }
        }

        private void btnFwd_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Tag = "F";
            timer1.Enabled = true;
        }

        private void btnFwd_MouseUp(object sender, MouseEventArgs e)
        {
            timer1.Enabled = false;
        }

        

        private void btnRight_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Enabled = true;
            timer1.Tag = "R";
        }

        private void btnRight_MouseUp(object sender, MouseEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void btnBack_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Enabled=true;
            timer1.Tag = "B";
        }

        private void btnBack_MouseUp(object sender, MouseEventArgs e)
        {
            timer1.Enabled = false;
        }
        private void btnLeft_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Enabled = true;
            timer1.Tag = "L";
        }

        private void btnLeft_MouseUp(object sender, MouseEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtbxDelay.Text = "500";
        }

        private void txtbxDelay_TextChanged(object sender, EventArgs e)
        {
            int delay=500;
            int.TryParse(txtbxDelay.Text, out delay);
            try
            {
                timer1.Interval = delay;
                writeLog("Delay set " + txtbxDelay.Text+ "ms");
            }
            catch(Exception ex)
            {
                
            }

        }

        private void txtbxLog_TextChanged(object sender, EventArgs e)
        {
            txtbxLog.SelectionStart = txtbxLog.Text.Length;
            txtbxLog.ScrollToCaret();
        }
    }
}
