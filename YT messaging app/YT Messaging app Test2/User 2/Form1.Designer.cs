namespace User_2
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
            txtMSG = new TextBox();
            btn_send = new Button();
            txtInMSG = new RichTextBox();
            label2 = new Label();
            label1 = new Label();
            txt_RemoteIP = new TextBox();
            Connect = new Button();
            SuspendLayout();
            // 
            // txtMSG
            // 
            txtMSG.Location = new Point(290, 501);
            txtMSG.Multiline = true;
            txtMSG.Name = "txtMSG";
            txtMSG.Size = new Size(415, 59);
            txtMSG.TabIndex = 13;
            // 
            // btn_send
            // 
            btn_send.Location = new Point(717, 501);
            btn_send.Name = "btn_send";
            btn_send.Size = new Size(66, 59);
            btn_send.TabIndex = 12;
            btn_send.Text = "button2";
            btn_send.UseVisualStyleBackColor = true;
            btn_send.Click += btn_send_Click;
            // 
            // txtInMSG
            // 
            txtInMSG.Location = new Point(207, 235);
            txtInMSG.Name = "txtInMSG";
            txtInMSG.Size = new Size(589, 225);
            txtInMSG.TabIndex = 11;
            txtInMSG.Text = "";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 70F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(356, 18);
            label2.Name = "label2";
            label2.Size = new Size(349, 125);
            label2.TabIndex = 10;
            label2.Text = "USER 3";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 45F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(225, 124);
            label1.Name = "label1";
            label1.Size = new Size(85, 81);
            label1.TabIndex = 9;
            label1.Text = "IP";
            // 
            // txt_RemoteIP
            // 
            txt_RemoteIP.Location = new Point(316, 146);
            txt_RemoteIP.Multiline = true;
            txt_RemoteIP.Name = "txt_RemoteIP";
            txt_RemoteIP.Size = new Size(226, 59);
            txt_RemoteIP.TabIndex = 8;
            txt_RemoteIP.Text = "127.0.0.1";
            // 
            // Connect
            // 
            Connect.Location = new Point(570, 146);
            Connect.Name = "Connect";
            Connect.Size = new Size(226, 59);
            Connect.TabIndex = 7;
            Connect.Text = "Connect";
            Connect.UseVisualStyleBackColor = true;
            Connect.Click += Connect_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1039, 605);
            Controls.Add(txtMSG);
            Controls.Add(btn_send);
            Controls.Add(txtInMSG);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txt_RemoteIP);
            Controls.Add(Connect);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtMSG;
        private Button btn_send;
        private RichTextBox txtInMSG;
        private Label label2;
        private Label label1;
        private TextBox txt_RemoteIP;
        private Button Connect;
    }
}