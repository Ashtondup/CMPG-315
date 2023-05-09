using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.VisualBasic.ApplicationServices;

namespace SignalRWinformClient
{
    public partial class Form1 : Form
    {
        HubConnection hubConnection;
        public Form1()
        {
            InitializeComponent();
            hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7262/ChatHub")
                .Build();
            hubConnection.Closed += HubConnection_Closed;
        }

        private async Task HubConnection_Closed(Exception? arg)
        {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await hubConnection.StartAsync();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                var newMessage = $"{user}:{message}";
                this.Invoke(new MethodInvoker(delegate ()
                { listBox1.Items.Add(newMessage); }));
                
            });
            try
            {
                await hubConnection.StartAsync();
                listBox1.Items.Add("Connection started");
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                await hubConnection.InvokeAsync("SendMessage", textBox1.Text, textBox2.Text);
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
        }
    }
}