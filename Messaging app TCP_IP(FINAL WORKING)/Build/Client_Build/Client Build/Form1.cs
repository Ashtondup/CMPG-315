using System;
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
        private async void btn_send_message_Click(object sender, EventArgs e)
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("You are not connected to the server.");
                return;
            }
            if (string.IsNullOrEmpty(txt_message.Text))
            {
                MessageBox.Show("Please enter a message.");
                return;
            }
            if (comboBoxClientsOnline.SelectedItem == null)
            {
                MessageBox.Show("Please select a recipient.");
                return;
            }
            try
            {
                string messageToSend = "To:" + comboBoxClientsOnline.SelectedItem.ToString() + " Message:" + txt_message.Text;
                byte[] buffer = Encoding.UTF8.GetBytes(messageToSend);
                await stream.WriteAsync(buffer, 0, buffer.Length); // Here, use the instance of NetworkStream
                txt_message.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending message: " + ex.Message);
            }
        }

        private void btn_connect_to_server_Click(object sender, EventArgs e)
        {
            try
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
                btn_connect_to_server.Enabled = false;
                btn_disconnect.Enabled = true;
            }
            catch (SocketException)
            {
                MessageBox.Show("Could not connect to server. Please make sure the server is online.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                        if (message.StartsWith("ConnectedClients:"))
                        {
                            string[] clients = message.Substring("ConnectedClients:".Length).Split(',');
                            UpdateConnectedClientsComboBox(clients);
                        }
                        else
                        {
                            ControlInvoke(rch_txt_messages, () => rch_txt_messages.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Server (To " + ((IPEndPoint)client.Client.LocalEndPoint).ToString() + "): " + message + Environment.NewLine));
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Add the message box here to notify that the connection was lost
                    if (!client.Connected)
                    {
                        MessageBox.Show("Connection lost.");
                        receiving = false;
                    }
                    else
                    {
                        ControlInvoke(rch_txt_messages, () => rch_txt_messages.AppendText("Error receiving data: " + ex.Message + Environment.NewLine));
                    }
                }
            }
        }
        private void UpdateConnectedClientsComboBox(string[] clients)
        {
            ControlInvoke(comboBoxClientsOnline, () =>
            {
                comboBoxClientsOnline.Items.Clear();
                foreach (string client in clients)
                {
                    if (!string.IsNullOrEmpty(client))
                    {
                        comboBoxClientsOnline.Items.Add(client);
                    }
                }
            });
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

        private void Form1_Load(object sender, EventArgs e)
        {
            btn_connect_to_server.Enabled = true;
            btn_disconnect.Enabled = false;
        }

        private async void btn_disconnect_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    // Send a disconnect message
                    string disconnectMessage = "Disconnecting";
                    byte[] messageBytes = Encoding.UTF8.GetBytes(disconnectMessage);
                    await stream.WriteAsync(messageBytes, 0, messageBytes.Length);

                    // Close the connection
                    client.Close();
                }
                catch (Exception ex)
                {
                    // Handle any errors
                    MessageBox.Show("Error disconnecting: " + ex.Message);
                }
            }
        }
    }
}
