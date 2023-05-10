namespace Server_Build
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Start_Server = new System.Windows.Forms.Button();
            this.btn_stop_server = new System.Windows.Forms.Button();
            this.rch_text_log = new System.Windows.Forms.RichTextBox();
            this.comboBoxClients = new System.Windows.Forms.ComboBox();
            this.btn_settings = new System.Windows.Forms.Button();
            this.lbl_server_status = new System.Windows.Forms.Label();
            this.lbl_clients = new System.Windows.Forms.Label();
            this.lbl_server_ip = new System.Windows.Forms.Label();
            this.lbl_server_port = new System.Windows.Forms.Label();
            this.txt_message = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_server_sendmessage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(39, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(707, 76);
            this.label1.TabIndex = 0;
            this.label1.Text = "Messaging App Server";
            // 
            // btn_Start_Server
            // 
            this.btn_Start_Server.Location = new System.Drawing.Point(220, 174);
            this.btn_Start_Server.Name = "btn_Start_Server";
            this.btn_Start_Server.Size = new System.Drawing.Size(75, 23);
            this.btn_Start_Server.TabIndex = 1;
            this.btn_Start_Server.Text = "Start Server";
            this.btn_Start_Server.UseVisualStyleBackColor = true;
            this.btn_Start_Server.Click += new System.EventHandler(this.btn_Start_Server_Click);
            // 
            // btn_stop_server
            // 
            this.btn_stop_server.Location = new System.Drawing.Point(343, 174);
            this.btn_stop_server.Name = "btn_stop_server";
            this.btn_stop_server.Size = new System.Drawing.Size(75, 23);
            this.btn_stop_server.TabIndex = 2;
            this.btn_stop_server.Text = "Stop Server";
            this.btn_stop_server.UseVisualStyleBackColor = true;
            this.btn_stop_server.Click += new System.EventHandler(this.btn_stop_server_Click);
            // 
            // rch_text_log
            // 
            this.rch_text_log.Location = new System.Drawing.Point(220, 222);
            this.rch_text_log.Name = "rch_text_log";
            this.rch_text_log.Size = new System.Drawing.Size(320, 222);
            this.rch_text_log.TabIndex = 3;
            this.rch_text_log.Text = "";
            // 
            // comboBoxClients
            // 
            this.comboBoxClients.FormattingEnabled = true;
            this.comboBoxClients.Location = new System.Drawing.Point(382, 479);
            this.comboBoxClients.Name = "comboBoxClients";
            this.comboBoxClients.Size = new System.Drawing.Size(121, 21);
            this.comboBoxClients.TabIndex = 4;
            // 
            // btn_settings
            // 
            this.btn_settings.Location = new System.Drawing.Point(465, 174);
            this.btn_settings.Name = "btn_settings";
            this.btn_settings.Size = new System.Drawing.Size(75, 23);
            this.btn_settings.TabIndex = 5;
            this.btn_settings.Text = "Settings";
            this.btn_settings.UseVisualStyleBackColor = true;
            // 
            // lbl_server_status
            // 
            this.lbl_server_status.AutoSize = true;
            this.lbl_server_status.Location = new System.Drawing.Point(185, 109);
            this.lbl_server_status.Name = "lbl_server_status";
            this.lbl_server_status.Size = new System.Drawing.Size(77, 13);
            this.lbl_server_status.TabIndex = 6;
            this.lbl_server_status.Text = "Server Status: ";
            // 
            // lbl_clients
            // 
            this.lbl_clients.AutoSize = true;
            this.lbl_clients.Location = new System.Drawing.Point(440, 109);
            this.lbl_clients.Name = "lbl_clients";
            this.lbl_clients.Size = new System.Drawing.Size(44, 13);
            this.lbl_clients.TabIndex = 7;
            this.lbl_clients.Text = "Clients: ";
            // 
            // lbl_server_ip
            // 
            this.lbl_server_ip.AutoSize = true;
            this.lbl_server_ip.Location = new System.Drawing.Point(185, 139);
            this.lbl_server_ip.Name = "lbl_server_ip";
            this.lbl_server_ip.Size = new System.Drawing.Size(54, 13);
            this.lbl_server_ip.TabIndex = 8;
            this.lbl_server_ip.Text = "Server IP:";
            // 
            // lbl_server_port
            // 
            this.lbl_server_port.AutoSize = true;
            this.lbl_server_port.Location = new System.Drawing.Point(440, 139);
            this.lbl_server_port.Name = "lbl_server_port";
            this.lbl_server_port.Size = new System.Drawing.Size(63, 13);
            this.lbl_server_port.TabIndex = 9;
            this.lbl_server_port.Text = "Server Port:";
            // 
            // txt_message
            // 
            this.txt_message.Location = new System.Drawing.Point(220, 480);
            this.txt_message.Name = "txt_message";
            this.txt_message.Size = new System.Drawing.Size(100, 20);
            this.txt_message.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(217, 447);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Send message";
            // 
            // btn_server_sendmessage
            // 
            this.btn_server_sendmessage.Location = new System.Drawing.Point(220, 526);
            this.btn_server_sendmessage.Name = "btn_server_sendmessage";
            this.btn_server_sendmessage.Size = new System.Drawing.Size(100, 23);
            this.btn_server_sendmessage.TabIndex = 12;
            this.btn_server_sendmessage.Text = "Send Message";
            this.btn_server_sendmessage.UseVisualStyleBackColor = true;
            this.btn_server_sendmessage.Click += new System.EventHandler(this.btn_server_sendmessage_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 656);
            this.Controls.Add(this.btn_server_sendmessage);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_message);
            this.Controls.Add(this.lbl_server_port);
            this.Controls.Add(this.lbl_server_ip);
            this.Controls.Add(this.lbl_clients);
            this.Controls.Add(this.lbl_server_status);
            this.Controls.Add(this.btn_settings);
            this.Controls.Add(this.comboBoxClients);
            this.Controls.Add(this.rch_text_log);
            this.Controls.Add(this.btn_stop_server);
            this.Controls.Add(this.btn_Start_Server);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Start_Server;
        private System.Windows.Forms.Button btn_stop_server;
        private System.Windows.Forms.RichTextBox rch_text_log;
        private System.Windows.Forms.ComboBox comboBoxClients;
        private System.Windows.Forms.Button btn_settings;
        private System.Windows.Forms.Label lbl_server_status;
        private System.Windows.Forms.Label lbl_clients;
        private System.Windows.Forms.Label lbl_server_ip;
        private System.Windows.Forms.Label lbl_server_port;
        private System.Windows.Forms.TextBox txt_message;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_server_sendmessage;
    }
}

