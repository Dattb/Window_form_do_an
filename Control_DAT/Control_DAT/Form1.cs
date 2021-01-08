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

namespace Control_DAT
{
    public partial class DatUTC : Form
    {   
        public DatUTC()
        {
            InitializeComponent();
        }
        int intlen = 0;
        byte[] Mesh_message = { 0xe8, 0xff, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xff, 0xff, 0x82, 0x4c, 0xff, 0xff, 0x00 };

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private Timer _scrollingTimer = null;
       

        private void btn_connect_Click(object sender, EventArgs e)
        {
            if (label2.Text == "Disconnect")
            {
                serialPort1.PortName = cbb_COM.Text;
                serialPort1.Open();
                serialPort1.BaudRate = 115200;
                serialPort1.Parity = 0 ;
                serialPort1.StopBits = 0;
                btn_connect.Text = "Disconnect";
                label2.Text = "Connect";
                cbb_COM.Enabled = false;
            }
            else
            {
                serialPort1.Close();
                btn_connect.Text = "Connect";
                label2.Text = "Disconnect";
               
                cbb_COM.Enabled = true;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            if (intlen != ports.Length)
            {
                intlen = ports.Length;
                cbb_COM.Items.Clear();
                for (int j = 0; j < intlen; j++)
                {
                    cbb_COM.Items.Add(ports[j]);
                }
            }
            
        }

        private void btn_1_Click(object sender, EventArgs e)
        {
            int Lux_data = Convert.ToInt32((label1.Text));

            if(serialPort1.IsOpen == true)
            {  
                Mesh_message[8] = 0xa1;
                Mesh_message[9] = 0x00;
                Mesh_message[12] = (byte)Lux_data;
                Mesh_message[13] = (byte)(Lux_data >> 8);
                serialPort1.Write(Mesh_message, 0, 15);
                
            }

        }

            private void btn_2_Click(object sender, EventArgs e)
        {
            int Lux_data = Convert.ToInt32((label1.Text));

            if (serialPort1.IsOpen == true)
            {

                Mesh_message[8] = 0xa4;
                Mesh_message[9] = 0x00;
                Mesh_message[12] = (byte)Lux_data;
                Mesh_message[13] = (byte)(Lux_data >> 8);
                serialPort1.Write(Mesh_message, 0, 15);

            }
        }

        private void btn_3_Click(object sender, EventArgs e)
        {
            int Lux_data = Convert.ToInt32((label1.Text));

            if (serialPort1.IsOpen == true)
            {
                Mesh_message[8] = 0xc0;
                Mesh_message[9] = 0x01;
                Mesh_message[12] = (byte)Lux_data;
                Mesh_message[13] = (byte)(Lux_data >> 8);
                serialPort1.Write(Mesh_message, 0, 15);


            }
        }

        private void trb_data_Scroll(object sender, EventArgs e)
        {
            if (_scrollingTimer == null)
            {

                _scrollingTimer = new Timer()
                {
                    Enabled = false,
                    Interval = 500,
                    Tag = (sender as TrackBar).Value
                };
                _scrollingTimer.Tick += (s, ea) =>
                {

                    if (trb_data.Value == (int)_scrollingTimer.Tag)
                    {

                        _scrollingTimer.Stop();

                        _scrollingTimer.Dispose();
                        _scrollingTimer = null;
                    }
                    else
                    {

                        _scrollingTimer.Tag = trb_data.Value;
                        label1.Text = trb_data.Value.ToString();
                    }
                };
                _scrollingTimer.Start();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}

