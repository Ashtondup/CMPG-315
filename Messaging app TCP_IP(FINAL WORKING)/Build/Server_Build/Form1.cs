using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server_Build
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        TcpClient Client;
        TcpListener Server;
        NetworkStream Stream;
        string host = "127.0.0.1";
        int port = 4444;
        bool ServerStatus = false;
        private bool isServerRunning = false;
        public class ClientHandler
        {
            public TcpClient Client { get; private set; }
            public NetworkStream Stream { get; private set; }

            public ClientHandler(TcpClient client)
            {
                Client = client;
                Stream = client.GetStream();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (ServerStatus == false)
            {
                lbl_server_status.Text = "Server Status:  Offline";
            }
            else 
            {
                lbl_server_status.Text = "Server Status:  Online";
            }
            lbl_clients.Text += "Clients : 0";
            lbl_server_ip.Text += host;
            lbl_server_port.Text += port.ToString();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseConnection();
        }

        private async void btn_Start_Server_Click(object sender, EventArgs e)
        {
            ServerStatus = true;
            Server = new TcpListener(IPAddress.Any, port);
            Server.Start();

            ControlInvoke(rch_text_log, () => rch_text_log.AppendText("Waiting for a client to connect..." + Environment.NewLine));

            isServerRunning = true;

            if (ServerStatus == false)
            {
                lbl_server_status.Text = "Server Status:  Offline";
            }
            else
            {
                lbl_server_status.Text = "Server Status:  Online";
            }

            while (isServerRunning)
            {
                try
                {
                    TcpClient newClient = await Server.AcceptTcpClientAsync();
                    ClientHandler clientHandler = new ClientHandler(newClient);
                    Task.Run(() => HandleClient(clientHandler));
                    string clientIp = ((IPEndPoint)newClient.Client.RemoteEndPoint).Address.ToString();
                    int clientPort = ((IPEndPoint)newClient.Client.RemoteEndPoint).Port;
                    ControlInvoke(rch_text_log, () => rch_text_log.AppendText("Connected to client" + Environment.NewLine));
                    ControlInvoke(comboBoxClients, () => comboBoxClients.Items.Add(clientIp + ":" + clientPort.ToString()));

                    // Display a message box to indicate that a client has connected
                    MessageBox.Show("A client has connected to the server.");
                }
                catch (Exception ex)
                {
                    if (isServerRunning)
                    {
                        ControlInvoke(rch_text_log, () => rch_text_log.AppendText("Error accepting client: " + ex.Message + Environment.NewLine));
                    }
                }
            }
        }
        private void onConnect(IAsyncResult AR)
        {
            Client = Server.EndAcceptTcpClient(AR);
            Stream = Client.GetStream();
            byte[] buffer = new byte[1024];
            Stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(onReceive), buffer);
            ControlInvoke(rch_text_log, () => rch_text_log.AppendText("Connected to client" + Environment.NewLine));

            // Display a message box to indicate that a client has connected
            MessageBox.Show("A client has connected to the server.");

            // Listen for incoming data from the client
            while (true)
            {
                try
                {
                    buffer = new byte[1024];
                    int bytesRead = Stream.Read(buffer, 0, buffer.Length);
                    ControlInvoke(rch_text_log, () => rch_text_log.AppendText(">>: " + Encoding.UTF8.GetString(buffer, 0, bytesRead) + Environment.NewLine));
                }
                catch (Exception ex)
                {
                    ControlInvoke(rch_text_log, () => rch_text_log.AppendText("Error receiving data: " + ex.Message + Environment.NewLine));
                    break;
                }
            }
        }
        private void onReceive(IAsyncResult AR)
        {
            try
            {
                int bytesRead = Stream.EndRead(AR);
                if (bytesRead > 0)
                {
                    byte[] buffer = (byte[])AR.AsyncState;
                    ControlInvoke(rch_text_log, () => rch_text_log.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + ((IPEndPoint)Client.Client.RemoteEndPoint).ToString() + " (To Server): " + Encoding.UTF8.GetString(buffer, 0, bytesRead) + Environment.NewLine));
                    Stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(onReceive), buffer);
                }
            }
            catch (Exception ex)
            {
                ControlInvoke(rch_text_log, () => rch_text_log.AppendText("Error receiving data: " + ex.Message + Environment.NewLine));
            }

        }

        private async void btn_server_sendmessage_Click(object sender, EventArgs e)
        {
            if (Client == null || !Client.Connected)
            {
                ControlInvoke(rch_text_log, () => rch_text_log.AppendText("No client connected" + Environment.NewLine));
                return;
            }

            try
            {
                byte[] message = Encoding.UTF8.GetBytes(txt_message.Text);
                await Stream.WriteAsync(message, 0, message.Length);
                ControlInvoke(rch_text_log, () => rch_text_log.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Server (To " + ((IPEndPoint)Client.Client.RemoteEndPoint).ToString() + "): " + txt_message.Text + Environment.NewLine));

                txt_message.Clear();
            }
            catch (Exception ex)
            {
                ControlInvoke(rch_text_log, () => rch_text_log.AppendText("Error sending data: " + ex.Message + Environment.NewLine));
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
            if (Client != null && Client.Connected)
            {
                Stream.Close();
                Client.Close();
            }

            if (Server != null)
            {
                Server.Stop();
            }
        }

        private void btn_stop_server_Click(object sender, EventArgs e)
        {
            ServerStatus = false;
            try
            {
                isServerRunning = false;

                if (Server != null)
                {
                    Server.Stop();
                    ControlInvoke(rch_text_log, () => rch_text_log.AppendText("Server stopped." + Environment.NewLine));
                    if (ServerStatus == false)
                    {
                        lbl_clients.Text = "Clients : 0";
                        lbl_server_status.Text = "Server Status:  Offline";
                    }
                }

                if (Stream != null)
                {
                    Stream.Close();
                }

                if (Client != null)
                {
                    Client.Close();
                }
            }
            catch (Exception ex)
            {
                ControlInvoke(rch_text_log, () => rch_text_log.AppendText("Error stopping server: " + ex.Message + Environment.NewLine));
            }
        }
        private async void HandleClient(ClientHandler clientHandler)
        {
            try
            {
                NetworkStream streamHandler = clientHandler.Stream;
                byte[] buffer = new byte[1024];

                while (true)
                {
                    int bytesRead = await streamHandler.ReadAsync(buffer, 0, buffer.Length);
                    string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    // Process the received message
                }
            }
            catch (Exception ex)
            {
                // Log the error or display a message
            }
        }
    }
}