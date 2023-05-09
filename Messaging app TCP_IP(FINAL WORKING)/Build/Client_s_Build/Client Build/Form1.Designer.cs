namespace Client_Build
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
            this.rch_txt_messages = new System.Windows.Forms.RichTextBox();
            this.btn_connect_to_server = new System.Windows.Forms.Button();
            this.btn_send_message = new System.Windows.Forms.Button();
            this.txt_message = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(135, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(385, 76);
            this.label1.TabIndex = 0;
            this.label1.Text = "Client_Build";
            // 
            // rch_txt_messages
            // 
            this.rch_txt_messages.Location = new System.Drawing.Point(156, 109);
            this.rch_txt_messages.Name = "rch_txt_messages";
            this.rch_txt_messages.Size = new System.Drawing.Size(364, 334);
            this.rch_txt_messages.TabIndex = 1;
            this.rch_txt_messages.Text = "";
            // 
            // btn_connect_to_server
            // 
            this.btn_connect_to_server.Location = new System.Drawing.Point(611, 130);
            this.btn_connect_to_server.Name = "btn_connect_to_server";
            this.btn_connect_to_server.Size = new System.Drawing.Size(109, 23);
            this.btn_connect_to_server.TabIndex = 2;
            this.btn_connect_to_server.Text = "Connect to Server";
            this.btn_connect_to_server.UseVisualStyleBackColor = true;
            this.btn_connect_to_server.Click += new System.EventHandler(this.btn_connect_to_server_Click);
            // 
            // btn_send_message
            // 
            this.btn_send_message.Location = new System.Drawing.Point(611, 473);
            this.btn_send_message.Name = "btn_send_message";
            this.btn_send_message.Size = new System.Drawing.Size(89, 23);
            this.btn_send_message.TabIndex = 3;
            this.btn_send_message.Text = "Send Message";
            this.btn_send_message.UseVisualStyleBackColor = true;
            this.btn_send_message.Click += new System.EventHandler(this.btn_send_message_Click);
            // 
            // txt_message
            // 
            this.txt_message.Location = new System.Drawing.Point(156, 475);
            this.txt_message.Name = "txt_message";
            this.txt_message.Size = new System.Drawing.Size(276, 20);
            this.txt_message.TabIndex = 4;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(611, 179);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 605);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.txt_message);
            this.Controls.Add(this.btn_send_message);
            this.Controls.Add(this.btn_connect_to_server);
            this.Controls.Add(this.rch_txt_messages);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rch_txt_messages;
        private System.Windows.Forms.Button btn_connect_to_server;
        private System.Windows.Forms.Button btn_send_message;
        private System.Windows.Forms.TextBox txt_message;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

