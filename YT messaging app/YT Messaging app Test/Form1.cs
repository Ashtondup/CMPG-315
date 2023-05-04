using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;

namespace YT_Messaging_app_Test
{
    public partial class Form1 : Form
    {
        private readonly ILogger<Form1> _logger;
        public Form1()
        {
            InitializeComponent();
            _logger = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            }).CreateLogger<Form1>();
        }
        UdpClient server;
        IPEndPoint remoteIP;
        int remotePort = 55555;
        int port = 55554;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            server = new UdpClient(port);
            remoteIP = new IPEndPoint(IPAddress.Parse(txt_RemoteIP.Text), remotePort);
            //remoteIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), remotePort);
            server.BeginReceive(new AsyncCallback(onReceive), server);
            _logger.LogInformation("Connection established with remote IP address: {0}", remoteIP);
            server.Connect(remoteIP);
        }
        private void onReceive(IAsyncResult AR)
        {
            byte[] buffer = server.EndReceive(AR, ref remoteIP);
            server.BeginReceive(new AsyncCallback(onReceive), server);
            ControlInvoke(txtInMSG, () => txtInMSG.AppendText(":>> " + Encoding.ASCII.GetString(buffer) + Environment.NewLine));

        }

        private void button2_Click(object sender, EventArgs e)
        {
            server.Send(Encoding.ASCII.GetBytes(txtMSG.Text), txtMSG.Text.Length);
            txtMSG.Clear();
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

        private void txtMSG_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtInMSG_TextChanged(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void txt_RemoteIP_TextChanged(object sender, EventArgs e)
        {
        }
    }
}