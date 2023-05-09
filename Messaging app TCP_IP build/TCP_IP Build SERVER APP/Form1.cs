using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace TCP_IP_Build_SERVER_APP
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            Server = new TcpListener(IPAddress.Any, port);
            Server.Start();

            ControlInvoke(txtInMSG, () => txtInMSG.AppendText("Waiting for a client to connect..." + Environment.NewLine));

            while (true)
            {
                try
                {
                    Client = await Server.AcceptTcpClientAsync();
                    Stream = Client.GetStream();
                    byte[] buffer = new byte[1024];
                    Stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(onReceive), buffer);
                    ControlInvoke(txtInMSG, () => txtInMSG.AppendText("Connected to client" + Environment.NewLine));

                    // Display a message box to indicate that a client has connected
                    MessageBox.Show("A client has connected to the server.");

                    // Listen for incoming data from the client
                    while (true)
                    {
                        try
                        {
                            buffer = new byte[1024];
                            int bytesRead = await Stream.ReadAsync(buffer, 0, buffer.Length);
                            ControlInvoke(txtInMSG, () => txtInMSG.AppendText(">>: " + Encoding.UTF8.GetString(buffer, 0, bytesRead) + Environment.NewLine));
                        }
                        catch (Exception ex)
                        {
                            ControlInvoke(txtInMSG, () => txtInMSG.AppendText("Error receiving data: " + ex.Message + Environment.NewLine));
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ControlInvoke(txtInMSG, () => txtInMSG.AppendText("Error accepting client: " + ex.Message + Environment.NewLine));
                }
            }
        }
        private void onConnect(IAsyncResult AR)
        {
            Client = Server.EndAcceptTcpClient(AR);
            Stream = Client.GetStream();
            byte[] buffer = new byte[1024];
            Stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(onReceive), buffer);
            ControlInvoke(txtInMSG, () => txtInMSG.AppendText("Connected to client" + Environment.NewLine));

            // Display a message box to indicate that a client has connected
            MessageBox.Show("A client has connected to the server.");

            // Listen for incoming data from the client
            while (true)
            {
                try
                {
                    buffer = new byte[1024];
                    int bytesRead = Stream.Read(buffer, 0, buffer.Length);
                    ControlInvoke(txtInMSG, () => txtInMSG.AppendText(">>: " + Encoding.UTF8.GetString(buffer, 0, bytesRead) + Environment.NewLine));
                }
                catch (Exception ex)
                {
                    ControlInvoke(txtInMSG, () => txtInMSG.AppendText("Error receiving data: " + ex.Message + Environment.NewLine));
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
                    ControlInvoke(txtInMSG, () => txtInMSG.AppendText(":>> " + Encoding.UTF8.GetString(buffer, 0, bytesRead) + Environment.NewLine));
                    Stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(onReceive), buffer);
                }
            }
            catch (Exception ex)
            {
                ControlInvoke(txtInMSG, () => txtInMSG.AppendText("Error receiving data: " + ex.Message + Environment.NewLine));
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Client = new TcpClient();
            Client.Connect("127.0.0.1", 4445); // set client IP and port
            Stream = Client.GetStream();
            byte[] message = Encoding.UTF8.GetBytes(txt_Msg.Text);
            Stream.Write(message, 0, message.Length);
            ControlInvoke(txtInMSG, () => txtInMSG.AppendText("<<: " + txt_Msg.Text + Environment.NewLine));
            txt_Msg.Clear();
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}