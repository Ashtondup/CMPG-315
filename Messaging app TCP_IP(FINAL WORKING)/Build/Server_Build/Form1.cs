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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server_Build
{
    public partial class Form1 : Form
    {
        private Dictionary<string, TcpClient> connectedClients = new Dictionary<string, TcpClient>();
        private CancellationTokenSource cancellationTokenSource;
        public Form1()
        {
            InitializeComponent();
        }
        public List<TcpClient> ActiveClients { get; private set; } = new List<TcpClient>();

        private Dictionary<string, ClientHandler> clients = new Dictionary<string, ClientHandler>();

        TcpClient Client;
        TcpListener Server;
        NetworkStream Stream;
        string host = "127.0.0.1";
        int port = 4444;
        bool ServerStatus = false;
        private bool isServerRunning = false;
        private int clientCount = 0; // Add this at the class level

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

            cancellationTokenSource = new CancellationTokenSource();

            Task.Run(async () =>
            {
                while (isServerRunning)
                {
                    try
                    {
                        TcpClient newClient = await Server.AcceptTcpClientAsync();
                        ClientHandler clientHandler = new ClientHandler(newClient);
                        HandleClient(clientHandler);
                    }
                    catch (Exception ex)
                    {
                        if (isServerRunning)
                        {
                            ControlInvoke(rch_text_log, () => rch_text_log.AppendText("Error accepting client: " + ex.Message + Environment.NewLine));
                        }
                    }
                }
            }, cancellationTokenSource.Token);
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
            if (comboBoxClients.SelectedItem == null)
            {
                ControlInvoke(rch_text_log, () => rch_text_log.AppendText("No client selected" + Environment.NewLine));
                return;
            }

            string selectedClientKey = comboBoxClients.SelectedItem.ToString();
            if (!clients.ContainsKey(selectedClientKey))
            {
                ControlInvoke(rch_text_log, () => rch_text_log.AppendText("Selected client not found" + Environment.NewLine));
                return;
            }

            ClientHandler selectedClient = clients[selectedClientKey];
            NetworkStream selectedClientStream = selectedClient.Stream;

            try
            {
                byte[] message = Encoding.UTF8.GetBytes(txt_message.Text);
                await selectedClientStream.WriteAsync(message, 0, message.Length);
                ControlInvoke(rch_text_log, () => rch_text_log.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Server (To " + selectedClientKey + "): " + txt_message.Text + Environment.NewLine));

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
        private void BroadcastConnectedClients()
        {
            string clientListMessage = "ConnectedClients:";
            foreach (string clientEndpoint in connectedClients.Keys)
            {
                clientListMessage += clientEndpoint + ",";
            }

            byte[] messageBytes = Encoding.UTF8.GetBytes(clientListMessage);
            foreach (TcpClient client in connectedClients.Values)
            {
                try
                {
                    NetworkStream stream = client.GetStream();
                    stream.Write(messageBytes, 0, messageBytes.Length);
                }
                catch (Exception ex)
                {
                    // Handle error if necessary
                    // This could happen if a client has disconnected unexpectedly
                    ControlInvoke(rch_text_log, () => rch_text_log.AppendText("Error sending message to client: " + ex.Message + Environment.NewLine));
                }
            }
        }
        private void btn_stop_server_Click(object sender, EventArgs e)
        {
            cancellationTokenSource.Cancel(); // This line cancels the ongoing AcceptTcpClientAsync() operation
            Server.Stop();
            btn_Start_Server.Enabled = true;
            btn_stop_server.Enabled = false;

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
            string clientEndpoint = clientHandler.Client.Client.RemoteEndPoint.ToString();

            // Add the client to the connectedClients dictionary and the ComboBox
            lock (connectedClients)
            {
                connectedClients.Add(clientEndpoint, clientHandler.Client);
                clients.Add(clientEndpoint, clientHandler);
                clientCount++; // Increment the counter
            }
            ControlInvoke(comboBoxClients, () => comboBoxClients.Items.Add(clientEndpoint));
            ControlInvoke(lbl_clients, () => lbl_clients.Text = $"Clients: {clientCount}"); // Update the label

            // Broadcast the updated list of clients
            BroadcastConnectedClients();
            try
            {
                NetworkStream streamHandler = clientHandler.Stream;
                byte[] buffer = new byte[1024];

                while (true)
                {
                    int bytesRead = await streamHandler.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        // Client disconnected
                        break;
                    }
                    string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    // Parse the received message
                    var messageParts = receivedMessage.Split(new[] { "To:", "Message:" }, StringSplitOptions.RemoveEmptyEntries);
                    if (messageParts.Length == 2)
                    {
                        string recipient = messageParts[0].Trim();
                        string message = messageParts[1].Trim();

                        if (clients.ContainsKey(recipient))
                        {
                            // Forward the message to the recipient
                            byte[] messageToForward = Encoding.UTF8.GetBytes(message);
                            await clients[recipient].Stream.WriteAsync(messageToForward, 0, messageToForward.Length);
                        }
                    }

                    ControlInvoke(rch_text_log, () => rch_text_log.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + clientEndpoint + " (To Server): " + receivedMessage + Environment.NewLine));
                }
            }
            catch (Exception ex)
            {
                // Log the error or display a message
                ControlInvoke(rch_text_log, () => rch_text_log.AppendText("Error: " + ex.Message + Environment.NewLine));
            }
            finally
            {
                // Remove the client from the comboBoxClients and close the connection
                ControlInvoke(comboBoxClients, () => comboBoxClients.Items.Remove(clientEndpoint));
                clientHandler.Client.Close();
                lock (connectedClients)
                {
                    connectedClients.Remove(clientEndpoint);
                    clients.Remove(clientEndpoint);
                    clientCount--; // Decrement the counter
                }
                ControlInvoke(lbl_clients, () => lbl_clients.Text = $"Clients: {clientCount}"); // Update the label
                BroadcastConnectedClients();
            }
        }




    }
    public static class TaskExtensions
    {
        public static async Task WithCancellation(this Task task, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();
            using (cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).TrySetResult(true), tcs))
                if (task != await Task.WhenAny(task, tcs.Task))
                    throw new OperationCanceledException(cancellationToken);
        }
    }
}