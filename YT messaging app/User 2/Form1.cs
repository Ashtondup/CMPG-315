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
        UdpClient Client;
        IPEndPoint remoteIP;
        int remotePort = 55554;
        int port = 55555;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void onReceive(IAsyncResult AR)
        {
            byte[] buffer = Client.EndReceive(AR, ref remoteIP);
            Client.BeginReceive(new AsyncCallback(onReceive), Client);
            ControlInvoke(txtInMSG, () => txtInMSG.AppendText(":>> " + Encoding.ASCII.GetString(buffer) + Environment.NewLine));

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
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Client.Connect(remoteIP);
            Client.Send(Encoding.ASCII.GetBytes(txtMSG.Text), txtMSG.Text.Length);
            txtMSG.Clear();
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            Client = new UdpClient(port);
            //remoteIP = new IPEndPoint(IPAddress.Parse(txt_RemoteIP.Text), remotePort);
            remoteIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), remotePort);
            Client.BeginReceive(new AsyncCallback(onReceive), Client);
            _logger.LogInformation("Connection established with remote IP address: {0}", remoteIP);
        }
        
    }
}