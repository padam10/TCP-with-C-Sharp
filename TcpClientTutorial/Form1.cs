using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;


namespace TcpClientTutorial
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        // some comment
        private void bConnect_Click(object sender, EventArgs e)
        {
            Thread mThread = new Thread(new ThreadStart(ConnectAsClient));
            mThread.Start();
        }

        private void ConnectAsClient()
        {
            TcpClient client = new TcpClient();
            //Change the following incorrect ip to your correct ip
            client.Connect(IPAddress.Parse("199.167.33.201"),5004);
            updateUI("Connected");
            NetworkStream stream = client.GetStream();
            string s = "Hello from client";
            byte[] message = Encoding.ASCII.GetBytes(s);
            stream.Write(message,0,message.Length);
            updateUI("Message sent");
            stream.Close();
            client.Close();

        }

        private void updateUI(string s)
        {
            Func<int> del = delegate()
                {
                    textBox1.AppendText(s + System.Environment.NewLine);
                    return 0;
                };
        }
    }
}
