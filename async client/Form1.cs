using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace async_client
{
    public partial class Form1 : Form
    {
        TcpClient client = new TcpClient();
        int port = 12345;
        public Form1()
        {
            InitializeComponent();
            client.NoDelay = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!client.Connected) StartConnection();
        }

        public async void StartConnection()
        {
            try
            {
                IPAddress ipadress = IPAddress.Parse(textBox1.Text);
                await client.ConnectAsync(ipadress, port);
            }
            catch(Exception error) 
            {
                MessageBox.Show(error.Message, Text);
                return;
            }

            button2.Enabled = true;
            button1.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            StartStream(textBox2.Text);
        }

        public async void StartStream( string message )
        {
            byte[] outdata = Encoding.Unicode.GetBytes( textBox2.Text );
            try
            {
                await client.GetStream().WriteAsync(outdata, 0, outdata.Length);
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message, Text);
                return;
            }
        }
    }
}
