using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;


namespace TCPServerDemo
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

        // After the button click it waits to listen at the following port number
        private void bStartServer_Click(object sender, EventArgs e)
        {
            Thread tcpServerRunThread = new Thread(new ThreadStart(this.TcpServerRun));
            tcpServerRunThread.Start();

        }

        private void TcpServerRun()
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Any, 5004);
            tcpListener.Start();
            updateUI("Listening");
            
            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                updateUI("Connected");
                Thread tcpHandlerThread = new Thread(new ParameterizedThreadStart(this.tcpHandler));
                tcpHandlerThread.Start(client);

            }
        }

        private void tcpHandler(object client)
        {
            TcpClient mClient = (TcpClient)client;
            NetworkStream stream = mClient.GetStream();
            byte[] message = new byte[1024];
            stream.Read(message, 0, message.Length);
            updateUI("New Message = " + Encoding.ASCII.GetString(message));
            stream.Close();
            mClient.Close();
        }

        private void updateUI(string s)
        {
            Func<int> del = delegate ()
            {
                textBox1.AppendText(s + System.Environment.NewLine);
                return 0;
            };
            Invoke(del);
        }
    }
}
