using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TCP_IP_Build_Client_App
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
        string Local = "127.0.0.1";
        int port = 4445;
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null && client.Connected)
            {
                byte[] message = Encoding.UTF8.GetBytes("Client is disconnecting...");
                stream.Write(message, 0, message.Length);
                stream.Close();
                client.Close();
            }

            receiving = false;
            receiveThread.Join();
        }
        public Form1()
        {
            InitializeComponent();
            receiving = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            client = new TcpClient();
            client.Connect(IP_ADDRESS, PORT);
            stream = client.GetStream();
            AddMessage("Connected to server.");

            // Send client IP address and port number to the server
            string clientInfo = String.Format("ClientInfo:{0}:{1}", ((IPEndPoint)client.Client.LocalEndPoint).Address.ToString(), ((IPEndPoint)client.Client.LocalEndPoint).Port.ToString());
            Send(clientInfo);

            username = "User " + new Random().Next(1000, 9999);
            Send(username);

            // Start the receive thread
            receiving = true;
            receiveThread = new Thread(new ThreadStart(Receive));
            receiveThread.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] message = Encoding.UTF8.GetBytes(txt_Msg.Text);
            stream.Write(message, 0, message.Length);
            ControlInvoke(txt_MESSAGES, () => txt_MESSAGES.AppendText("<<: " + txt_Msg.Text + Environment.NewLine));
            txt_Msg.Clear();
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
                        ControlInvoke(txt_MESSAGES, () => txt_MESSAGES.AppendText(">>: " + Encoding.UTF8.GetString(buffer, 0, bytesRead) + Environment.NewLine));
                    }
                }
                catch (Exception ex)
                {
                    ControlInvoke(txt_MESSAGES, () => txt_MESSAGES.AppendText("Error receiving data: " + ex.Message + Environment.NewLine));
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
                txt_MESSAGES.AppendText(message + Environment.NewLine);
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


    }
}