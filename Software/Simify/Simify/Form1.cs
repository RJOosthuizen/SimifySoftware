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



namespace Simify
{
    public partial class Form1 : Form
    {
        bool isConnected = false;
        String[] ports;
        SerialPort port;
        public Form1()
        {
            InitializeComponent();
            disableControls();
            getAvailableComPorts();

            foreach (string port in ports)
            {
                cboxPorts.Items.Add(port);
                Console.WriteLine(port);
                if (ports[0] != null)
                {
                    cboxPorts.SelectedItem = ports[0];
                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                connectToArduino();
            }
            else
            {
                disconnectFromArduino();
            }
        }


        void getAvailableComPorts()
        {
            ports = SerialPort.GetPortNames();
        }

        private void connectToArduino()
        {
            isConnected = true;
            string selectedPort = cboxPorts.GetItemText(cboxPorts.SelectedItem);
            port = new SerialPort(selectedPort, 9600, Parity.None, 8, StopBits.One);
            port.Open();
            port.Write("#STAR\n");
            btnConnect.Text = "Disconnect";
            enableControls();
        }

        private void disconnectFromArduino()
        {
            isConnected = false;
            port.Write("#STOP\n");
            port.Close();
            btnConnect.Text = "Connect";
            disableControls();
            //resetDefaults();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                port.Write("#TEXTON#\n");
            }
        }

        private void enableControls()
        {
            btnOn.Enabled = true;
            btnOff.Enabled = true;
        }

        private void disableControls()
        {
            btnOn.Enabled = false;
            btnOff.Enabled = false;
        }

        private void btnOff_Click(object sender, EventArgs e)
        {
            if (isConnected)
            {
                port.Write("#TEXTOFF#\n");
            }
        }
    }
}
