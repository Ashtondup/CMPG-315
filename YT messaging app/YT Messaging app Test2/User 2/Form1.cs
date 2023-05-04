using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;

namespace User_2
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
        int remotePort = 55554;
        int port = 55555;

        private void Connect_Click(object sender, EventArgs e)
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

        private void btn_send_Click(object sender, EventArgs e)
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
    }
}