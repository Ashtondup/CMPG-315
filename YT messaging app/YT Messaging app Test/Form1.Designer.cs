namespace YT_Messaging_app_Test
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Connect = new Button();
            txt_RemoteIP = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtInMSG = new RichTextBox();
            btn_send = new Button();
            txtMSG = new TextBox();
            SuspendLayout();
            // 
            // Connect
            // 
            Connect.Location = new Point(509, 137);
            Connect.Name = "Connect";
            Connect.Size = new Size(226, 59);
            Connect.TabIndex = 0;
            Connect.Text = "Connect";
            Connect.UseVisualStyleBackColor = true;
            Connect.Click += button1_Click;
            // 
            // txt_RemoteIP
            // 
            txt_RemoteIP.Location = new Point(255, 137);
            txt_RemoteIP.Multiline = true;
            txt_RemoteIP.Name = "txt_RemoteIP";
            txt_RemoteIP.Size = new Size(226, 59);
            txt_RemoteIP.TabIndex = 1;
            txt_RemoteIP.Text = "127.0.0.1";
            txt_RemoteIP.TextChanged += txt_RemoteIP_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 45F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(164, 115);
            label1.Name = "label1";
            label1.Size = new Size(85, 81);
            label1.TabIndex = 2;
            label1.Text = "IP";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 70F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(311, -3);
            label2.Name = "label2";
            label2.Size = new Size(349, 125);
            label2.TabIndex = 3;
            label2.Text = "USER 1";
            label2.Click += label2_Click;
            // 
            // txtInMSG
            // 
            txtInMSG.Location = new Point(146, 226);
            txtInMSG.Name = "txtInMSG";
            txtInMSG.Size = new Size(589, 225);
            txtInMSG.TabIndex = 4;
            txtInMSG.Text = "";
            txtInMSG.TextChanged += txtInMSG_TextChanged;
            // 
            // btn_send
            // 
            btn_send.Location = new Point(656, 492);
            btn_send.Name = "btn_send";
            btn_send.Size = new Size(66, 59);
            btn_send.TabIndex = 5;
            btn_send.Text = "button2";
            btn_send.UseVisualStyleBackColor = true;
            btn_send.Click += button2_Click;
            // 
            // txtMSG
            // 
            txtMSG.Location = new Point(229, 492);
            txtMSG.Multiline = true;
            txtMSG.Name = "txtMSG";
            txtMSG.Size = new Size(415, 59);
            txtMSG.TabIndex = 6;
            txtMSG.TextChanged += txtMSG_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(939, 627);
            Controls.Add(txtMSG);
            Controls.Add(btn_send);
            Controls.Add(txtInMSG);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txt_RemoteIP);
            Controls.Add(Connect);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Connect;
        private TextBox txt_RemoteIP;
        private Label label1;
        private Label label2;
        private RichTextBox txtInMSG;
        private Button btn_send;
        private TextBox txtMSG;
    }
}