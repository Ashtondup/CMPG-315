﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Client_Build
{
    public partial class Form1 : Form
    {
        private readonly string IP_ADDRESS = "127.0.0.1"; // Change this to the IP address of the server
        private readonly int PORT = 4444; // Change this to the port of the server
        private TcpClient client;
        private NetworkStream stream;
        private string username;
        private Thread receiveThread;
        private bool receiving;

        public Form1()
        {
            InitializeComponent();
            receiving = false;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e) 
        {
            CloseConnection();
        }
        private void btn_send_message_Click(object sender, EventArgs e)
        {
            byte[] message = Encoding.UTF8.GetBytes(txt_message.Text);
            stream.Write(message, 0, message.Length);
            ControlInvoke(rch_txt_messages, () => rch_txt_messages.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + ((IPEndPoint)client.Client.LocalEndPoint).ToString() + " (To Server): " + txt_message.Text + Environment.NewLine));
            txt_message.Clear();
        }

        private void btn_connect_to_server_Click(object sender, EventArgs e)
        {
            client = new TcpClient();
            client.Connect(IP_ADDRESS, PORT);
            stream = client.GetStream();
            AddMessage("Connected to server.");

            // Send client IP address and port number to the server
            string clientInfo = String.Format("ClientInfo:{0}:{1}", ((IPEndPoint)client.Client.LocalEndPoint).Address.ToString(), ((IPEndPoint)client.Client.LocalEndPoint).Port.ToString());
            Send(clientInfo);

            // Start the receive thread
            receiving = true;
            receiveThread = new Thread(new ThreadStart(Receive));
            receiveThread.Start();
        }
        private void Send(string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            stream.Write(buffer, 0, buffer.Length);
        }
        private void Receive()
        {
            while (receiving)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        ControlInvoke(rch_txt_messages, () => rch_txt_messages.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Server (To " + ((IPEndPoint)client.Client.LocalEndPoint).ToString() + "): " + Encoding.UTF8.GetString(buffer, 0, bytesRead) + Environment.NewLine));
                    }
                }
                catch (Exception ex)
                {
                    ControlInvoke(rch_txt_messages, () => rch_txt_messages.AppendText("Error receiving data: " + ex.Message + Environment.NewLine));
                }
            }
        }
        private void AddMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => AddMessage(message)));
            }
            else
            {
                rch_txt_messages.AppendText(message + Environment.NewLine);
            }
        }
        delegate void UniversalVoidDelegate();

        public static void ControlInvoke(Control control, Action function)
        {

            if (control.IsDisposed || control.Disposing)
            {
                return;
            }
            if (control.InvokeRequired)
            {
                control.Invoke(new UniversalVoidDelegate(() => ControlInvoke(control, function)));
                return;
            }
            function();
        }
        private void CloseConnection()
        {
            if (client != null && client.Connected)
            {
                receiving = false;
                receiveThread.Join();
                stream.Close();
                client.Close();
            }
        }
    }
}
